// Copyright © 2022 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;

namespace CefSharp.MinimalExample.WinForms
{
    public class LoadingProcessChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Access to the underlying <see cref="IBrowser"/> object
        /// </summary>
        public IBrowser Browser { get; private set; }

        /// <summary>
        /// Loaidng progress
        /// </summary>
        public double Progress { get; private set; }

        /// <summary>
        /// Creates a new AddressChangedEventArgs event argument.
        /// </summary>
        /// <param name="browser">the browser object</param>
        /// <param name="progress">the address</param>
        public LoadingProcessChangedEventArgs(IBrowser browser, double progress)
        {
            Browser = browser;
            Progress = progress;
        }
    }
}
