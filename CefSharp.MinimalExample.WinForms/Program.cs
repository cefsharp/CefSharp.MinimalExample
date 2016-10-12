// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            //For Windows 7 and above, best to include relevant app.manifest entries as well
            Cef.EnableHighDPISupport();

            var browser = new BrowserForm();

            IBrowserProcessHandler browserProcessHandler = null;
            //Pass reference to the form to access it's BeginInvoke method
            browserProcessHandler = new BrowserProcessHandler(browser);

            var settings = new CefSettings();
            settings.MultiThreadedMessageLoop = false;
            settings.ExternalMessagePump = true;

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: browserProcessHandler);
            
            Application.Run(browser);
        }
    }
}
