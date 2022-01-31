// Copyright © 2022 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public interface IChromiumWebBrowserEx : IChromiumWebBrowserBase, IWin32Window, IComponent, ISynchronizeInvoke
    {
        /// <summary>
        /// Called when Loading progress changed
        /// </summary>
        event EventHandler<LoadingProcessChangedEventArgs> LoadingProcessChanged;

        void OnLoadingProcessChanged(IBrowser browser, double progress);
    }
}
