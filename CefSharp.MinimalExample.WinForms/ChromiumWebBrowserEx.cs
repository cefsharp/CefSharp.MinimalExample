// Copyright © 2022 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;

namespace CefSharp.MinimalExample.WinForms
{
    public class ChromiumWebBrowserEx : ChromiumWebBrowser, IChromiumWebBrowserEx
    {
        /// <summary>
        /// Called when Loading progress changed
        /// </summary>
        public event EventHandler<LoadingProcessChangedEventArgs> LoadingProcessChanged;

        public ChromiumWebBrowserEx(string address) : base(address)
        {

        }

        public void OnLoadingProcessChanged(IBrowser browser, double progress)
        {
            var args = new LoadingProcessChangedEventArgs(browser, progress);

            LoadingProcessChanged?.Invoke(this, args);
        }
    }
}
