// Copyright © 2022 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using CefSharp.WinForms.Host;

namespace CefSharp.MinimalExample.WinForms
{
    public class DisplayHandlerEx : CefSharp.WinForms.Handler.DisplayHandler
    {
        protected override void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {
            if(browser.IsPopup)
            {
                var control = (ChromiumHostControlEx)ChromiumHostControl.FromBrowser(browser);

                control?.OnLoadingProcessChanged(browser, progress);
            }
            else
            {
                var control = (ChromiumWebBrowserEx)ChromiumWebBrowser.FromBrowser(browser);

                control?.OnLoadingProcessChanged(browser, progress);
            }

            base.OnLoadingProgressChange(chromiumWebBrowser, browser, progress);
        }
    }
}
