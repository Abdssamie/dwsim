using DWSIM.Simulate365.Enums;
using DWSIM.Simulate365.Models;
using DWSIM.UI.Web.Settings;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWSIM.Simulate365.Services
{
    public static class FileUploaderService
    {
        public static event EventHandler<BeforeUploadEventArgs> BeforeUpload;
        public static event EventHandler UploadStarted;
        public static event EventHandler UploadCompleted;
        public static event EventHandler<Exception> UploadFailed;

        /// <summary>
        /// Uploads file to Simulate 365 dashboard
        /// </summary>
        /// <param name="fileUniqueIdentifier">File uniqueId (GUID)</param>
        /// <param name="parentUniqueIdentifier">Parent folder uniqueId (GUID)</param>
        /// <param name="filePath">Pyhsical file location</param>
        /// <param name="filename">Name of file</param>
        /// <param name="simulatePath">Simulate 365 file path</param>
        /// <param name="ownerId">File owner id (GUID), used for uploading changes in collaboration files,S365File-> OwnerId </param>
        /// <param name="conflictAction">Conflict action when file exists</param>
        /// <param name="currentVersion">Version number of file in Simulate 365</param>
        /// <returns>S365File</returns>
        public static S365File UploadFile(string fileUniqueIdentifier, string parentUniqueIdentifier, string filePath, string filename, string simulatePath, string ownerId, UploadConflictAction? conflictAction, long? fileVersion = null)
        {
            using (var fileStream = System.IO.File.OpenRead(filePath))
                return UploadFile(fileUniqueIdentifier, parentUniqueIdentifier, fileStream, filename, simulatePath, ownerId, conflictAction, fileVersion);
        }

        /// <summary>
        /// Uploads file to Simulate 365 dashboard
        /// </summary>
        /// <param name="fileUniqueIdentifier">File uniqueId (GUID)</param>
        /// <param name="parentUniqueIdentifier">Parent folder uniqueId (GUID)</param>
        /// <param name="fileStream">File loaded into Stream</param>
        /// <param name="filename">Name of file</param>
        /// <param name="simulatePath">Simulate 365 file path</param>
        /// <param name="ownerId">File owner id (GUID), used for uploading changes in collaboration files</param>
        /// <param name="conflictAction">Conflict action when file exists</param>
        /// <param name="currentVersion">Version number of file in Simulate 365</param>
        /// <returns>S365File</returns>       
        public static S365File UploadFile(string fileUniqueIdentifier, string parentUniqueIdentifier, Stream fileStream, string filename, string simulatePath, string ownerId, UploadConflictAction? conflictAction, long? fileVersion = null)
        {
            try
            {
                UploadStarted?.Invoke(null, EventArgs.Empty);
                // Invoke event handlers
                var eventArgs = new BeforeUploadEventArgs();
                BeforeUpload?.Invoke(null, eventArgs);
                if (eventArgs.Cancel)
                    throw new Exception("Upload operation was canceled.");

                fileStream.Seek(0, SeekOrigin.Begin);

                var token = UserService.GetInstance().GetUserToken();
                var client = GetDashboardClient(token);

                var file = Task.Run(async () => await UploadDocumentAsync(parentUniqueIdentifier, filename, fileStream, ownerId, conflictAction, fileVersion)).Result;

                UploadCompleted?.Invoke(null, EventArgs.Empty);
                return new S365File(filename)
                {
                    FileUniqueIdentifier = file.FileUniqueIdentifier.ToString(),
                    ParentUniqueIdentifier = parentUniqueIdentifier,
                    Filename = file.Filename,
                    FileVersion = file.FileVersion,
                    FullPath = file.SimulatePath,
                    OwnerId = file.OwnerId.ToString(),
                    IsSharedForCollaboration = file.IsSharedForCollaboration
                };
            }
            catch (Exception ex)
            {
                UploadFailed?.Invoke(null, ex);
                throw new Exception("An error occurred while saving file to Simulate 365 Dashboard.", ex);
            }
        }

        public static S365File UploadFileByFilePath(string simulatePath, Stream fileStream, string ownerId, UploadConflictAction? conflictAction, long? fileVersion = null)
        {
            try
            {
                UploadStarted?.Invoke(null, EventArgs.Empty);
                // Invoke event handlers
                var eventArgs = new BeforeUploadEventArgs();
                BeforeUpload?.Invoke(null, eventArgs);
                if (eventArgs.Cancel)
                    throw new Exception("Upload operation was canceled.");

                if (simulatePath.StartsWith("//Simulate 365 Dashboard/"))
                    simulatePath = simulatePath.Substring(24);

                var fileWithBreadCrumbs = GetFileByPath(simulatePath);
                if (fileWithBreadCrumbs == null || fileWithBreadCrumbs.File == null)
                    throw new Exception($"File on simulate path '{simulatePath}' not found.");
                var file = fileWithBreadCrumbs.File;

                fileStream.Seek(0, SeekOrigin.Begin);

                var token = UserService.GetInstance().GetUserToken();
                var client = GetDashboardClient(token);
                var parentUniqueIdentifier = fileWithBreadCrumbs.BreadcrumbItems?.LastOrDefault()?.UniqueIdentifier.ToString();

                var filename = Path.GetFileName(simulatePath) ?? string.Empty;

                var fileResp = Task.Run(async () => await UploadDocumentAsync(parentUniqueIdentifier, filename, fileStream, ownerId, conflictAction, fileVersion)).Result;

                UploadCompleted?.Invoke(null, EventArgs.Empty);
                return new S365File(filename)
                {
                    FileUniqueIdentifier = fileResp.FileUniqueIdentifier.ToString(),
                    FileVersion = fileResp.FileVersion,
                    ParentUniqueIdentifier = parentUniqueIdentifier,
                    Filename = fileResp.Filename,
                    FullPath = fileResp.SimulatePath,
                    OwnerId = fileResp.OwnerId.ToString(),
                    IsSharedForCollaboration = fileResp.IsSharedForCollaboration
                };
            }
            catch (Exception ex)
            {
                UploadFailed?.Invoke(null, ex);
                throw new Exception("An error occurred while saving file to Simulate 365 Dashboard.", ex);
            }
        }

        private static async Task<UploadFileResponseModel> UploadDocumentAsync(string parentUniqueIdentifier, string filename, Stream fileStream, string ownerId, UploadConflictAction? conflictAction, long? fileVersion)
        {
            try
            {
                var token = UserService.GetInstance().GetUserToken();
                var client = GetDashboardClient(token);

                using (var content = new MultipartFormDataContent())
                {
                    // 0= Overwrite file if exists, 1= Keep both
                    if (conflictAction.HasValue)
                        content.Add(new StringContent(conflictAction.ToString()), "ConflictAction");
                    if (!string.IsNullOrWhiteSpace(parentUniqueIdentifier))
                        content.Add(new StringContent(parentUniqueIdentifier), "ParentDirectoryUniqueId");

                    if (!string.IsNullOrWhiteSpace(ownerId))
                        content.Add(new StringContent(ownerId), "OwnerId");

                    if (fileVersion.HasValue)
                    {
                        content.Add(new StringContent(fileVersion.ToString()), "FileVersion");
                    }

                    content.Add(new StreamContent(fileStream), "files", filename);

                    // Send request
                    var response = await client.PostAsync("/api/files/upload", content);

                    // Handle response
                    var responseContent = await response.Content.ReadAsStringAsync();



                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        throw new Exception($"An error occurred while uploading file. Status code: {response.StatusCode}. Error:{errorMessage}");
                    }

                    var responseModel = JsonConvert.DeserializeObject<List<UploadFileResponseModel>>(responseContent);

                    if (responseModel == null || responseModel.Count == 0)
                        throw new Exception("An error occurred while uploading file. Response is empty.");

                    return responseModel.First();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to upload document.", ex);
            }
        }

        private static FilesWithBreadcrumbsResponseModel GetFileByPath(string simulatePath, AccessType? accessType = AccessType.ReadOnly)
        {
            var token = UserService.GetInstance().GetUserToken();
            var client = GetDashboardClient(token);
            var result = Task.Run(async () => await client.GetAsync($"/api/files/by-path?filePath={simulatePath}&includeBreadcrumbs=true&accessType={accessType}")).Result;
            var resultContent = Task.Run(async () => await result.Content.ReadAsStringAsync()).Result;
            var itemWithBreadcrumbs = JsonConvert.DeserializeObject<FilesWithBreadcrumbsResponseModel>(resultContent);
            return itemWithBreadcrumbs;
        }

        public static FilesWithBreadcrumbsResponseModel GetFileByUniqueIdentifier(string fileUniqueIdentifier, AccessType? accessType = AccessType.ReadOnly)
        {
            var token = UserService.GetInstance().GetUserToken();
            var client = GetDashboardClient(token);
            var result = Task.Run(async () => await client.GetAsync($"/api/files/{fileUniqueIdentifier}/single?includeBreadcrumbs=true&accessType={accessType}")).Result;
            var resultContent = Task.Run(async () => await result.Content.ReadAsStringAsync()).Result;
            var itemWithBreadcrumbs = JsonConvert.DeserializeObject<FilesWithBreadcrumbsResponseModel>(resultContent);
            return itemWithBreadcrumbs;
        }

        private static HttpClient GetDashboardClient(string token)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(DashboardSettings.DashboardServiceUrl);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            return client;
        }
    }

    public class BeforeUploadEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }
}
