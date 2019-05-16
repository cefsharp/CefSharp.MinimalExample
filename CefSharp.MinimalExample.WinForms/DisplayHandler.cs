using System.Collections.Generic;
using CefSharp.Structs;

namespace CefSharp.MinimalExample.WinForms
{
    public class DisplayHandler : IDisplayHandler
    {

        void IDisplayHandler.OnAddressChanged(IWebBrowser browserControl, AddressChangedEventArgs addressChangedArgs)
        {

        }

        bool IDisplayHandler.OnAutoResize(IWebBrowser browserControl, IBrowser browser, Size newSize)
        {
            return false;
        }

        void IDisplayHandler.OnTitleChanged(IWebBrowser browserControl, TitleChangedEventArgs titleChangedArgs)
        {

        }

        void IDisplayHandler.OnFaviconUrlChange(IWebBrowser browserControl, IBrowser browser, IList<string> urls)
        {

        }

        void IDisplayHandler.OnFullscreenModeChange(IWebBrowser browserControl, IBrowser browser, bool fullscreen)
        {

        }

        /// <summary>
        /// Called when the overall page loading progress has changed
        /// </summary>
        /// <param name="chromiumWebBrowser">The ChromiumWebBrowser control</param>
        /// <param name="browser">the browser object</param>
        /// <param name="process">ranges from 0.0 to 1.0.</param>
        public void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {
             
        }

        bool IDisplayHandler.OnTooltipChanged(IWebBrowser browserControl, ref string text)
        {
            text = "aaaaa";
            return false;
        }

        void IDisplayHandler.OnStatusMessage(IWebBrowser browserControl, StatusMessageEventArgs statusMessageArgs)
        {
           
        }

        bool IDisplayHandler.OnConsoleMessage(IWebBrowser browserControl, ConsoleMessageEventArgs consoleMessageArgs)
        {
            return false;
        }
    }
}