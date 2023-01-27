// Copyright © 2021 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.Wpf;
using System;
using System.IO;

namespace CefSharp.MinimalExample.Wpf
{
    /// <summary>
    /// For .Net 5.0/6.0/7.0 Publishing Single File exe requires using your own applications executable to
    /// act as the BrowserSubProcess. See https://github.com/cefsharp/CefSharp/issues/3407
    /// for further details. <see cref="Program.Main(string[])"/> for the default main application entry point
    /// </summary>
    public class ProgramPublishSingleFile
    {
        [STAThread]
        public static int Main(string[] args)
        {
            // To support High DPI this must be before CefSharp.BrowserSubprocess.SelfHost.Main so the BrowserSubprocess is DPI Aware
            // Alternatively set DPI Awareness in your app.manifest
            Cef.EnableHighDPISupport();

            var exitCode = CefSharp.BrowserSubprocess.SelfHost.Main(args);

            if (exitCode >= 0)
            {
                return exitCode;
            }

            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
                BrowserSubprocessPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
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

            //Don't perform a dependency check
            //By default this example calls Cef.Initialzie in the CefSharp.MinimalExample.Wpf.App
            //constructor for purposes of providing a self contained single file example we call it here.
            //You could remove this code and use the CefSharp.MinimalExample.Wpf.App example if you 
            //set BrowserSubprocessPath to an absolute path to your main application exe.
            Cef.Initialize(settings, performDependencyCheck: false);

            var app = new App();
            app.InitializeComponent();
            return app.Run();
        }
    }
}
