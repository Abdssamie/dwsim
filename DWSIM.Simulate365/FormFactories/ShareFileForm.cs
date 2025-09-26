using DWSIM.Interfaces;
using DWSIM.Simulate365.Services;
using DWSIM.UI.Web;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWSIM.Simulate365.FormFactories
{
    public class ShareFileForm
    {
        private WebUIForm _webUIForm;
        private readonly FilePickerService _filePickerService;
        private readonly UserService _userService;
        public ShareFileForm()
        {
            _filePickerService = new FilePickerService();
            _userService = UserService.GetInstance();
            _userService.OnUserLoggedIn += OnUserLoggedInEvent;
        }

        public void ShowFileShareDialog(string fileUniqueId)
        {
            var initialUrl = $"share/file/{fileUniqueId}";
            string title = "Share File";
            _webUIForm = new WebUIForm(initialUrl, title, true)
            {
                Width = (int)(1300 * DWSIM.GlobalSettings.Settings.DpiScale),
                Height = (int)(800 * DWSIM.GlobalSettings.Settings.DpiScale)
            };
            _webUIForm.SubscribeToInitializationCompleted(Browser_CoreWebView2InitializationCompleted);

            _webUIForm.ShowDialog();
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

        private void OnUserLoggedInEvent(object sender, EventArgs e)
        {
            // We do not have file unique id here, so just close the form
            if(_webUIForm!=null && _webUIForm.Visible)
            {
                _webUIForm?.Close();              
            }
           
        }
    }
}
