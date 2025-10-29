using DWSIM.Logging;
using DWSIM.UI.Web.Services;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DWSIM.UI.Web
{
    public partial class WebUIForm : Form
    {
        public static string USER_DATA_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DWSIM", "S365BrowserData");

        public static string LOCAL_WEB_UI_DOMAIN = "https://dwsim.webui";
        public static string LOCAL_WEB_UI_URL = $"{LOCAL_WEB_UI_DOMAIN}/index.html#";

        public string InitialUrl { get; private set; }
        public string Title { get; private set; }
        public bool UseLocalUI { get; private set; }
        public Dictionary<string, object> HostedObjects { get; set; } = new Dictionary<string, object>();

        private bool _isDisposing = false;
        private CancellationTokenSource _initializationCts;

        public WebUIForm(string initialUrl, string title = null, bool userLocalUI = false)
        {
            // If userLocalUI == false, then real URL must be provided
            if (!userLocalUI && (String.IsNullOrWhiteSpace(initialUrl) || !Regex.IsMatch(initialUrl, "https*://")))
                throw new Exception("When not using local UI, real URL must be provided.");

            this.UseLocalUI = userLocalUI;

            // If using local UI, prepand virtual domain
            if (userLocalUI)
                this.InitialUrl = $"{LOCAL_WEB_UI_URL}/{initialUrl}";
            else
                this.InitialUrl = initialUrl;

            _initializationCts = new CancellationTokenSource();

            // After all variables are set, then initialize form components
            InitializeComponent();




            // Title
            // Must be called after initialize components
            if (!String.IsNullOrWhiteSpace(title))
                this.Text = title;
            else
                this.Text = "Web UI";

            // Preconfigure WebView2
            webView.CreationProperties = new CoreWebView2CreationProperties()
            {
                UserDataFolder = USER_DATA_FOLDER
            };


            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;


            this.Shown += async (_, __) =>
            {
                try
                {
                    await InitializeAsync(_initializationCts.Token);
                }
                catch (TaskCanceledException)
                {
                    Logger.LogInfo("WebView2 initialization canceled (form closing or disposed).");
                }
                catch (ObjectDisposedException)
                {
                    Logger.LogInfo("WebView2 initialization aborted — form already disposed.");
                }
                catch (Exception ex)
                {
                    Logger.LogError("Unhandled error during WebView2 initialization.", ex);
                }
            };
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            try
            {
                if (_isDisposing || this.IsDisposed || webView.IsDisposed)
                    return;



                if (webView.CoreWebView2 != null)
                {
                    // Add hosted objects
                    foreach (var kv in HostedObjects)
                    {
                        try
                        {
                            webView.CoreWebView2.AddHostObjectToScript(kv.Key, kv.Value);
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError($"Failed to add host object {kv.Key}", ex);
                        }
                    }

                    // Add system service if not already added
                    if (!HostedObjects.ContainsKey("systemService"))
                    {
                        try
                        {
                            webView.CoreWebView2.AddHostObjectToScript("systemService", new SystemService());
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError("Failed to add systemService host object", ex);
                        }
                    }

                    webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;




                    webView.CoreWebView2.NavigationCompleted += (s, args) =>
                    {
                        if (!args.IsSuccess)
                        {
                            Logger.LogError($"Navigation failed: {args.WebErrorStatus}", null);
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occurred while initializing WebView2.", ex);
            }
        }

        private async Task InitializeAsync(CancellationToken token)
        {
            int retries = 3;

            for (int i = 0; i < retries; i++)
            {
                try
                {
                    Logger.LogInfo($"Attempting to initialize WebView2 (attempt {i + 1}/{retries})");






                    if (token.IsCancellationRequested || _isDisposing || this.IsDisposed)
                        return;



                    if (webView == null || webView.IsDisposed)
                    {
                        Logger.LogError("webView is null or disposed before initialization.", null);
                        return;
                    }

                    if (!webView.IsHandleCreated)
                    {
                        webView.CreateControl();
                        if (!webView.IsHandleCreated)
                        {

                            Logger.LogError("Failed to create handle for webView.", null);
                            await Task.Delay(200 * (i + 1), token);
                            continue;
                        }
                    }

                    var environment = await WaitAsync(
                                                 CoreWebView2Environment.CreateAsync(null, USER_DATA_FOLDER, null),
                                                 TimeSpan.FromSeconds(5),
                                                 token);

                    if (token.IsCancellationRequested || _isDisposing || this.IsDisposed || webView.IsDisposed)
                        return;

                    await webView.EnsureCoreWebView2Async(environment);

                    if (token.IsCancellationRequested || _isDisposing || this.IsDisposed || webView.IsDisposed)
                        return;

                    if (webView.CoreWebView2 == null)
                    {
                        Logger.LogError("CoreWebView2 is null after EnsureCoreWebView2Async.", null);
                        await Task.Delay(200 * (i + 1), token);
                        continue;
                    }

                    if (UseLocalUI)
                    {
                        var assemblyDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location ?? Assembly.GetExecutingAssembly().Location);
                        var webUiDir = Path.Combine(assemblyDir, "dwsim-web-ui");
                        if (!Directory.Exists(webUiDir))
                            throw new Exception($"Directory {webUiDir} doesn't exist.");

                        webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                            "dwsim.webui", webUiDir, CoreWebView2HostResourceAccessKind.Allow);
                    }

                    webView.Source = new Uri(InitialUrl);
                    return; // success
                }
                catch (TaskCanceledException)
                {
                    Logger.LogInfo("WebView2 initialization cancelled.");
                    return;
                }
                catch (ObjectDisposedException)
                {
                    Logger.LogInfo("WebView2 disposed during initialization, aborting.");
                    return;
                }
                catch (InvalidOperationException ex) when (ex.Message.Contains("disposed"))
                {
                    Logger.LogInfo("Initialization aborted due to control disposal.");
                    return;
                }
                catch (COMException ex) when ((uint)ex.HResult == 0x80004004) // E_ABORT
                {
                    await Task.Delay(200 * (i + 1), token);
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Failed to initialize WebView2 (attempt {i + 1}/{retries})", ex);
                    if (i == retries - 1 && !_isDisposing && !this.IsDisposed)
                    {
                        try
                        {
                            MessageBox.Show(this,
                                "Failed to initialize WebView2 after retries. Please restart the application.",
                                "Initialization Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        catch { }
                    }
                    await Task.Delay(200 * (i + 1), token);
                }
            }
        }

        public void AddHostObjectToScript(string name, object rawObject)
        {
            if (_isDisposing) return;
            HostedObjects[name] = rawObject;

            try
            {
                if (!_isDisposing && !this.IsDisposed && !webView.IsDisposed && webView.CoreWebView2 != null)
                {
                    webView.CoreWebView2.AddHostObjectToScript(name, rawObject);
                }
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("disposed")) { }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to add host object {name}", ex);
            }
        }

        public void RemoveHostObject(string name)
        {
            try
            {
                if (!_isDisposing && !this.IsDisposed && !webView.IsDisposed && webView.CoreWebView2 != null)
                {
                    webView.CoreWebView2.RemoveHostObjectFromScript(name);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to remove host object {name}", ex);
            }
            finally
            {
                HostedObjects.Remove(name);
            }
        }

        public void RealoadPage()
        {
            try
            {
                if (!_isDisposing && !this.IsDisposed && !webView.IsDisposed && webView.CoreWebView2 != null)
                {
                    webView.CoreWebView2.Reload();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to reload page", ex);
            }
        }

        public void Navigate(string url)
        {
            SafeNavigate(url);
        }

        public void SafeNavigate(string url)
        {
            try
            {
                if (!_isDisposing && !this.IsDisposed && !webView.IsDisposed)
                {
                    webView.Source = new Uri(url);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to navigate to {url}", ex);
            }
        }

        public bool IsWebView2Ready()
        {
            return !_isDisposing &&
                   !this.IsDisposed &&
                   !webView.IsDisposed &&
                   webView.CoreWebView2 != null;
        }

        public void SafeClose()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(SafeClose));
                return;
            }

            try
            {
                if (!this.IsDisposed)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error during safe close", ex);
            }
        }

        public void SubscribeToNavigationStarting(EventHandler<CoreWebView2NavigationStartingEventArgs> callback)
        {
            try
            {
                if (!_isDisposing && !this.IsDisposed && !webView.IsDisposed)
                {
                    webView.NavigationStarting += callback;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to subscribe to NavigationStarting", ex);
            }
        }

        public void SubscribeToInitializationCompleted(EventHandler<CoreWebView2InitializationCompletedEventArgs> callback)
        {
            try
            {
                if (!_isDisposing && !this.IsDisposed && !webView.IsDisposed)
                {
                    webView.CoreWebView2InitializationCompleted += callback;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to subscribe to InitializationCompleted", ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _isDisposing = true;

            // Cancel any ongoing initialization
            _initializationCts?.Cancel();
            _initializationCts?.Dispose();
            _initializationCts = null;

            try
            {
                if (webView != null && !webView.IsDisposed)
                {
                    try
                    {
                        webView.Stop();

                        if (webView.CoreWebView2 != null)
                        {
                            webView.CoreWebView2.Stop();

                            foreach (var key in HostedObjects.Keys.ToList())
                            {
                                try
                                {
                                    webView.CoreWebView2.RemoveHostObjectFromScript(key);
                                }
                                catch (Exception ex)
                                {
                                    Logger.LogError($"Error removing host object {key}", ex);
                                }
                            }

                            HostedObjects.Clear();
                        }
                    }
                    catch (ObjectDisposedException) { }
                    catch (InvalidOperationException ex) when (ex.Message.Contains("disposed")) { }

                    webView.CoreWebView2InitializationCompleted -= WebView_CoreWebView2InitializationCompleted;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error during WebView2 disposal", ex);
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private static async Task<T> WaitAsync<T>(Task<T> task, TimeSpan timeout, CancellationToken token)
        {
            var timeoutCts = new CancellationTokenSource(timeout);
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, timeoutCts.Token);

            var tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);

            using (linkedCts.Token.Register(() => tcs.TrySetCanceled(linkedCts.Token)))
            {
                var completed = await Task.WhenAny(task, tcs.Task);
                if (completed == tcs.Task)
                    throw new TaskCanceledException();

                return await task; // unwrap result
            }
        }
    }
}