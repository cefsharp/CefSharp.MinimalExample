// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
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
            //For Windows 7 and above, app.manifest entries will take precedences of this call
            Cef.EnableHighDPISupport();

            //We are using our current exe as the BrowserSubProcess
            //Multiple instances will be spawned to handle all the 
            //Chromium proceses, render, gpu, network, plugin, etc.
            var subProcessExe = new CefSharp.BrowserSubprocess.BrowserSubprocessExecutable();
            var result = subProcessExe.Main(args);
            if (result > 0)
            {
                return result;
            }

            //We use our current exe as the BrowserSubProcess
            var exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
                BrowserSubprocessPath = exePath
            };

            //Example of setting a command line argument
            //Enables WebRTC
            settings.CefCommandLineArgs.Add("enable-media-stream");

            // For .NET Core, don't perform a dependency check, to allow publishing single-file
            // executables.
            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

            var browser = new BrowserForm();
            Application.Run(browser);

            return 0;
        }
    }
}
