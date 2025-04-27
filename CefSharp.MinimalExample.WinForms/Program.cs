﻿// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {

#if ANYCPU
            CefRuntime.SubscribeAnyCpuAssemblyResolver();
#endif

            var cachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache2");

            var settings = new CefSettings()
            {
                // You must spcify a unique cache path per instance of your application
                CachePath = cachePath
            };

            //Example of setting a command line argument
            //Enables WebRTC
            // - CEF Doesn't currently support permissions on a per browser basis see https://bitbucket.org/chromiumembedded/cef/issues/2582/allow-run-time-handling-of-media-access
            // - CEF Doesn't currently support displaying a UI for media access permissions
            //
            //NOTE: WebRTC Device Id's aren't persisted as they are in Chrome see https://bitbucket.org/chromiumembedded/cef/issues/2064/persist-webrtc-deviceids-across-restart
            settings.CefCommandLineArgs.Add("enable-media-stream");
            //https://peter.sh/experiments/chromium-command-line-switches/#use-fake-ui-for-media-stream
            settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream");
            //For screen sharing add (see https://bitbucket.org/chromiumembedded/cef/issues/2582/allow-run-time-handling-of-media-access#comment-58677180)
            settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing");

            settings.CefCommandLineArgs.Add("disable-features", "OptimizationGuideOnDeviceModel");
            //--

            //Perform dependency check to make sure all relevant resources are in our output directory.
            var initialized = Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            if (!initialized)
            {
                var exitCode = Cef.GetExitCode();

                if (exitCode == Enums.ResultCode.NormalExitProcessNotified)
                {
                    MessageBox.Show($"Cef.Initialize failed with {exitCode}, another process is already using cache path {cachePath}");
                }
                else
                {
                    MessageBox.Show($"Cef.Initialize failed with {exitCode}, check the log file for more details.");
                }

                return 0;
            }

            Application.EnableVisualStyles();
            Application.Run(new BrowserForm());

            return 0;
        }
    }
}
