namespace CefSharp.MinimalExample.OffScreen
{
    public class DownloadHandler : IDownloadHandler
    {
        void IDownloadHandler.OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            var url = downloadItem.Url;

            System.Diagnostics.Debugger.Break();
        }

        void IDownloadHandler.OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {

        }
    }
}
