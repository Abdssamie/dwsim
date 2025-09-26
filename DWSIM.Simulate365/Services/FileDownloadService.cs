using DWSIM.Simulate365.Enums;
using DWSIM.Simulate365.Models;
using DWSIM.UI.Web.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.Simulate365.Services
{
    public class FileDownloadService
    {
        public static Stream GetFileBySimulatePath(string simulatePath)
        {
            if (simulatePath.StartsWith("//Simulate 365 Dashboard/"))
                simulatePath = simulatePath.Substring(24);
            var token = UserService.GetInstance().GetUserToken();
            var client = GetDashboardClient(token);
            // Get drive item
            var stream = Task.Run(async () => await client.GetStreamAsync($"/api/files/download-by-path?filePath={simulatePath}")).Result;
            return stream;
        }
        public static Stream GetFileByUniqueIdentifier(string fileUniqueIdentifier, AccessType accessType = AccessType.ReadOnly)
        {
            var token = UserService.GetInstance().GetUserToken();
            var client = GetDashboardClient(token);
            // Get drive item
            var stream = Task.Run(async () => await client.GetStreamAsync($"/api/files/{fileUniqueIdentifier}/download?accessType={accessType}")).Result;
            return stream;
        }

        public static bool FileExistsByPath(string simulatePath)
        {
            if (simulatePath.StartsWith("//Simulate 365 Dashboard/"))
                simulatePath = simulatePath.Substring(24);

            var token = UserService.GetInstance().GetUserToken();
            var client = GetDashboardClient(token);
            var contentJson = JsonConvert.SerializeObject(new FileExistsByPathPostModel { FilePath = simulatePath });
            var content = new StringContent(contentJson, Encoding.UTF8, "application/json");

            try
            {
                var result = Task.Run(async () => await client.PostAsync("/api/files/file-exists-by-path", content)).Result;

                if (!result.IsSuccessStatusCode)
                {
                    var errorMessage = Task.Run(async () => await result.Content.ReadAsStringAsync()).Result;
                    throw new Exception($"An error occurred while checking if file exists by file path. Response status:{result.StatusCode}. Error message: {errorMessage}");
                }

                var resultJson = Task.Run(async () => await result.Content.ReadAsStringAsync()).Result;
                if (string.IsNullOrEmpty(resultJson))
                    throw new Exception("An error occurred while checking if file exists by file path. File response message was empty.");

                var responseModel = JsonConvert.DeserializeObject<FileExistsResponseModel>(resultJson);
                return responseModel?.Exists ?? false;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during the file existence check", ex);
            }
        }



        private static HttpClient GetDashboardClient(string token)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(DashboardSettings.DashboardServiceUrl);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            return client;

        }
    }
}
