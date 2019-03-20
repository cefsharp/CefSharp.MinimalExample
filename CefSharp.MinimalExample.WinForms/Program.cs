// Copyright © 2010-2019 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using CefSharp.Internals;
using System.Threading.Tasks;
using CefSharp.RenderProcess;
using CefSharp.BrowserSubprocess;
using CefSharp.MinimalExample.WinForms.RenderProcess;

namespace CefSharp.MinimalExample.WinForms
{
    /// <summary>
    /// Demos integrating the browser subprocess into your application executable
    /// </summary>
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            //For Windows 7 and above, best to include relevant app.manifest entries as well
            Cef.EnableHighDPISupport();

            var type = args.GetArgumentValue(CefSharpArguments.SubProcessTypeArgument);

            if(type == null)
            {
                var settings = new CefSettings()
                {
                    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                    CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
                    //Using your own executable as the subprocess requires
                    BrowserSubprocessPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "CefSharp.MinimalExample.WinForms.exe")
                };

                //Example of setting a command line argument
                //Enables WebRTC
                settings.CefCommandLineArgs.Add("enable-media-stream", "1");

                //Enable this to debug the BrowserProcess
                //A dialog will popup allowing you to attach the debugger to the process
                //settings.CefCommandLineArgs.Add("renderer-startup-dialog", "1");

                //Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

                var browser = new BrowserForm();
                Application.Run(browser);

                return 0;
            }

            return CustomBrowserProcessCode(type, args);
        }

        /// <summary>
        /// Custom BrowserSubProcess handling code goes here
        /// </summary>
        /// <returns>error code</returns>
        private static int CustomBrowserProcessCode(string type, string[] args)
        {
            int result;

            Debug.WriteLine("BrowserSubprocess starting up with command line: " + string.Join("\n", args));

            var parentProcessId = -1;

            // The Crashpad Handler doesn't have any HostProcessIdArgument, so we must not try to
            // parse it lest we want an ArgumentNullException.
            if (type != "crashpad-handler")
            {
                parentProcessId = int.Parse(args.GetArgumentValue(CefSharpArguments.HostProcessIdArgument));
                if (args.HasArgument(CefSharpArguments.ExitIfParentProcessClosed))
                {
                    Task.Factory.StartNew(() => AwaitParentProcessExit(parentProcessId), TaskCreationOptions.LongRunning);
                }
            }

            // Use our custom subProcess provides features like EvaluateJavascript
            if (type == "renderer")
            {
                //Add your own custom implementation of IRenderProcessHandler here
                IRenderProcessHandler handler = new RenderProcessHandler();
                var wcfEnabled = args.HasArgument(CefSharpArguments.WcfEnabledArgument);
                var subProcess = wcfEnabled ? new WcfEnabledSubProcess(parentProcessId, handler, args) : new SubProcess(handler, args);

                using (subProcess)
                {
                    result = subProcess.Run();
                }
            }
            else
            {
                result = SubProcess.ExecuteProcess();
            }

            Debug.WriteLine("BrowserSubprocess shutting down.");

            return result;
        }

        private static async void AwaitParentProcessExit(int parentProcessId)
        {
            try
            {
                var parentProcess = Process.GetProcessById(parentProcessId);
                parentProcess.WaitForExit();
            }
            catch (Exception e)
            {
                //main process probably died already
                Debug.WriteLine(e);
            }

            await Task.Delay(1000); //wait a bit before exiting

            Debug.WriteLine("BrowserSubprocess shutting down forcibly.");

            Environment.Exit(0);
        }
    }
}
