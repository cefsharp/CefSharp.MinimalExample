// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var executingFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //If libcef.dll doesn't exist in our executing folder then we'll copy the files
            //For this method the user must have write permissions to the executing folder.
            if (!File.Exists(Path.Combine(executingFolder, "libcef.dll")))
            {
                var libPath = Path.Combine(executingFolder, Environment.Is64BitProcess ? "x64" : "x86");

                CopyFilesRecursively(new DirectoryInfo(libPath), new DirectoryInfo(executingFolder));

                Directory.Delete("x86", recursive: true);
                Directory.Delete("x64", recursive: true);
            }

            LoadApp();
        }

        //https://stackoverflow.com/a/58779/4583726
        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadApp()
        {
            //Perform dependency check to make sure all relevant resources are in our output directory.
            var settings = new CefSettings();

            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

            var browser = new BrowserForm();
            Application.Run(browser);
        }
    }
}
