using DWSIM.Interfaces;
using DWSIM.Simulate365.Models;
using DWSIM.Simulate365.Services;
using DWSIM.UI.Web;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace DWSIM.Simulate365.FormFactories
{
    public class S365FilePickerForm : IFilePicker
    {
        private WebUIForm _webUIForm;
        private readonly FilePickerService _filePickerService;

        public string SuggestedDirectory { get; set; }
        public string SuggestedFilename { get; set; }

        private readonly UserService _userService;

        private bool CollaborationEnabled;


        #region Public events

        public static event EventHandler FileOpenedFromDashboard;

        public static event EventHandler<BeforeShowDialogEventArgs> BeforeShowSaveDialog;

        public static event EventHandler<BeforeShowDialogEventArgs> BeforeShowOpenDialog;
        public event EventHandler AfterUserLoggedIn;

        #endregion

        public S365FilePickerForm()
        {

            _filePickerService = new FilePickerService();
            _filePickerService.S3365DashboardFileOpenStarted += FilePickerService_S3365DashboardFileOpenStarted;
            _filePickerService.S365DashboardSaveFileClicked += FilePickerService_S365DashboardSaveFileClicked;
            _filePickerService.S365DashboardFolderCreated += _filePickerService_S365DashboardFolderCreated;
            _userService = UserService.GetInstance();
            _userService.OnUserLoggedIn += OnUserLoggedInEvent;

            string configValue = ConfigurationManager.AppSettings["EnableCollaboration"];
            if (!string.IsNullOrEmpty(configValue))
            {
                bool.TryParse(configValue, out CollaborationEnabled);
            }
        }


        private void OnUserLoggedInEvent(object sender, EventArgs e)
        {
            if (AfterUserLoggedIn != null)
            {
                AfterUserLoggedIn?.Invoke(this, new EventArgs());
            }
            else
            {
                _webUIForm?.RealoadPage();
            }
            //_webUIForm?.Navigate(_webUIForm?.InitialUrl);
        }

        private void _filePickerService_S365DashboardFolderCreated(object sender, EventArgs e)
        {
            _webUIForm?.RealoadPage();
        }

        private void FilePickerService_S365DashboardSaveFileClicked(object sender, S365DashboardSaveFile e)
        {
            UsubscribeFromEvents();

            // Close window
            _webUIForm?.Close();
            _webUIForm?.Dispose();
        }
        private void UsubscribeFromEvents()
        {
            _userService.OnUserLoggedIn -= OnUserLoggedInEvent;
            _filePickerService.S3365DashboardFileOpenStarted -= FilePickerService_S3365DashboardFileOpenStarted;
            _filePickerService.S365DashboardSaveFileClicked -= FilePickerService_S365DashboardSaveFileClicked;
            _filePickerService.S365DashboardFolderCreated -= _filePickerService_S365DashboardFolderCreated;
        }

        private void FilePickerService_S3365DashboardFileOpenStarted(object sender, EventArgs e)
        {
            UsubscribeFromEvents();
            // Close window
            _webUIForm?.Close();
            _webUIForm?.Dispose();
        }

        public void Close()
        {
            UsubscribeFromEvents();
            _webUIForm?.Close();
            _webUIForm?.Dispose();
        }

        public S365File ShowSaveDialog(List<string> fileFormats = null, bool isSaveAs = false, bool isLeavingCollaborationFile = false)
        {
            // Invoke event handlers
            var eventArgs = new BeforeShowDialogEventArgs();
            BeforeShowSaveDialog?.Invoke(null, eventArgs);
            if (eventArgs.Cancel)
                return null;

            var navigationPath = "filepicker/save";
            var queryParams = new Dictionary<string, string>();
            if (fileFormats != null && fileFormats.Count > 0)
            {
                queryParams.Add("extensions", string.Join("_", fileFormats));
            }
            if (!string.IsNullOrWhiteSpace(SuggestedDirectory))
            {

                // If user has opened collaboration file, and tries to save that file he will get wrong SuggestedDirectory.
                // We could compare OwnerId of opened file with currentUserId, but getting opened file data from here is issue.
                // For now we will just disable setting suggestedDirectory inside save form if collaboration is enabled.
                if (!CollaborationEnabled)
                {
                    queryParams.Add("directory", HttpUtility.UrlEncode(SuggestedDirectory));
                }

            }

            if (isSaveAs)
            {
                queryParams.Add("saveAs", "true");
            }

            if (isLeavingCollaborationFile)
            {
                queryParams.Add("leavingCollaborationFile", "true");
            }

            if (!string.IsNullOrWhiteSpace(SuggestedFilename))
            {
                queryParams.Add("filename", HttpUtility.UrlEncode(SuggestedFilename));
            }

            var initialUrl = $"{navigationPath}";
            if (queryParams.Any())
            {
                initialUrl = initialUrl + string.Join("", queryParams.Select(x =>
                {
                    var param = $"{x.Key}={x.Value}";
                    return queryParams.First().Key == x.Key ? $"?{param}" : $"&{param}";

                }).ToList());
            }

            string title = $"Save {(isSaveAs ? "As" : "")} file to Simulate 365 Dashboard";
            _webUIForm = new WebUIForm(initialUrl, title, true)
            {
                Width = (int)(1300 * DWSIM.GlobalSettings.Settings.DpiScale),
                Height = (int)(800 * DWSIM.GlobalSettings.Settings.DpiScale)
            };

            _webUIForm.SubscribeToInitializationCompleted(Browser_CoreWebView2InitializationCompleted);

            _webUIForm.ShowDialog();

            return _filePickerService.SelectedSaveFile != null ?
                new S365File(null)
                {
                    FileUniqueIdentifier = null,
                    Filename = _filePickerService.SelectedSaveFile.Filename,
                    ParentUniqueIdentifier = _filePickerService.SelectedSaveFile.ParentUniqueIdentifier,
                    FullPath = _filePickerService.SelectedSaveFile.SimulatePath,
                    ConflictAction = _filePickerService.SelectedSaveFile.ConflictAction,
                    OwnerId = UserService.GetInstance().CurrentUser?.Id,
                    IsSharedForCollaboration = _filePickerService.SelectedSaveFile.IsSharedForCollaboration
                } : null;
        }


        public S365File ShowOpenDialog(List<string> fileFormats = null)
        {
            // Invoke event handlers
            var eventArgs = new BeforeShowDialogEventArgs();
            BeforeShowOpenDialog?.Invoke(null, eventArgs);
            if (eventArgs.Cancel)
                return null;

            var navigationPath = "filepicker/open";
            var queryParams = new Dictionary<string, string>();
            if (fileFormats != null && fileFormats.Count > 0)
            {
                queryParams.Add("extensions", string.Join("_", fileFormats));
            }
            if (!string.IsNullOrWhiteSpace(SuggestedDirectory))
            {
                queryParams.Add("directory", HttpUtility.UrlEncode(SuggestedDirectory));
            }

            bool enableCollaboration = false;

            string configValue = ConfigurationManager.AppSettings["EnableCollaboration"];
            if (!string.IsNullOrEmpty(configValue))
            {
                bool.TryParse(configValue, out enableCollaboration);
            }
            if (enableCollaboration)
            {
                queryParams.Add("collaboration", "true");
            }

            var initialUrl = $"{navigationPath}";
            if (queryParams.Any())
            {
                initialUrl = initialUrl + string.Join("", queryParams.Select(x =>
                {
                    var param = $"{x.Key}={x.Value}";
                    return queryParams.First().Key == x.Key ? $"?{param}" : $"&{param}";

                }).ToList());
            }
            string title = "Open file from Simulate 365 Dashboard";
            _webUIForm = new WebUIForm(initialUrl, title, true)
            {
                Width = (int)(1300 * DWSIM.GlobalSettings.Settings.DpiScale),
                Height = (int)(800 * DWSIM.GlobalSettings.Settings.DpiScale)
            };

            _webUIForm.SubscribeToInitializationCompleted(Browser_CoreWebView2InitializationCompleted);

            _webUIForm.ShowDialog();

            return _filePickerService.SelectedOpenFile;
        }


        private void Browser_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            try
            {
                var webView = sender as WebView2;
                if (webView.CoreWebView2 != null)
                {
                    webView.CoreWebView2.AddHostObjectToScript("authService", new AuthService());
                    webView.CoreWebView2.AddHostObjectToScript("filePickerService", _filePickerService);
                }
            }
            catch (Exception ex)
            {

                //  throw;
            }
        }

        #region FilePickerService

        public IVirtualFile ShowOpenDialog(IEnumerable<IFilePickerAllowedType> allowedTypes)
        {
            List<string> fileFormats = null;
            if (allowedTypes != null && allowedTypes.Count() > 0)
            {
                fileFormats = allowedTypes.SelectMany(t => t.AllowedExtensions.Select(e => ReplateLeadingStarDot(e))).Distinct().ToList();
            }

            var file = ShowOpenDialog(fileFormats);

            FileOpenedFromDashboard?.Invoke(this, new EventArgs());
            return file;
        }

        public IVirtualFile ShowSaveDialog(IEnumerable<IFilePickerAllowedType> allowedTypes, bool isSaveAs = false, bool isLeavingCollaborationFile = false)
        {
            List<string> fileFormats = null;
            if (allowedTypes != null && allowedTypes.Count() > 0)
            {
                fileFormats = allowedTypes.SelectMany(t => t.AllowedExtensions.Select(e => ReplateLeadingStarDot(e))).Distinct().ToList();
            }

            var file = ShowSaveDialog(fileFormats, isSaveAs, isLeavingCollaborationFile);
            return file;
        }

        private string ReplateLeadingStarDot(string input)
        {
            return Regex.Replace(input, @"^\*{0,1}\.", "");
        }


        #endregion
    }

    public class BeforeShowDialogEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }
}
