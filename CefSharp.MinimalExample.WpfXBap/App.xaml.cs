using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using CefSharp;

namespace CefSharp.MinimalExample.WpfXBap
{ 
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            IBrowserProcessHandler browserProcessHandler;

            AppDomain.CurrentDomain.AssemblyResolve += Resolver;

            browserProcessHandler = null;

            //Any CefSharp references have to be in another method with NonInlining
            // attribute so the assembly rolver has time to do it's thing.
            InitializeCefSharp(browserProcessHandler);
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            Cef.Shutdown();

            base.OnExit(e);
        }
        #endregion

        #region Private Methods
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void InitializeCefSharp(IBrowserProcessHandler browserProcessHandler)
        {
            CefSettings settings;

            try
            {
                settings = new CefSettings();

                // Set BrowserSubProcessPath based on app bitness at runtime
                settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Environment.Is64BitProcess ? "x64" : "x86", "CefSharp.BrowserSubprocess.exe");

                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: browserProcessHandler);
            }
            catch (FileNotFoundException exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        /// <summary>
        /// Will attempt to load missing assembly from either x86 or x64 subdir
        /// Required by CefSharp to load the unmanaged dependencies when running using AnyCPU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static Assembly Resolver(object sender, ResolveEventArgs e)
        {
            Assembly resolvedAssembly;
            string assemblyName;
            string archSpecificPath;

            resolvedAssembly = null;

            if (e.Name.StartsWith("CefSharp") == true)
            {
                //https://github.com/cefsharp/CefSharp/issues/1714
                assemblyName = e.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Environment.Is64BitProcess ? "x64" : "x86", assemblyName);

                //LoadFile does not load files into the load-from context, and does not resolve dependencies using the load path
                resolvedAssembly = File.Exists(archSpecificPath) ? Assembly.LoadFile(archSpecificPath) : null;
            }

            return resolvedAssembly;
        }
        #endregion
    }
}
