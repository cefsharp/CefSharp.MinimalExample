// Copyright © 2022 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms.Host;
using System;

namespace CefSharp.MinimalExample.WinForms
{
    
    public delegate void OnLoadingProgressChangeDelegate(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress);

    public class ChromiumHostControlEx : ChromiumHostControl, IChromiumWebBrowserEx
    {
        /// <summary>
        /// Called when Loading progress changed
        /// </summary>
        public event EventHandler<LoadingProcessChangedEventArgs> LoadingProcessChanged;

        public void OnLoadingProcessChanged(IBrowser browser, double progress)
        {
            var args = new LoadingProcessChangedEventArgs(browser, progress);

            LoadingProcessChanged?.Invoke(this, args);
        }
    }
}
