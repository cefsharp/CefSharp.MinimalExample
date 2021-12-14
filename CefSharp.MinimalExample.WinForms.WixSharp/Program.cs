using System;
using WixSharp;
using Microsoft.Deployment.WindowsInstaller;
using System.IO;

namespace CefSharp.MinimalExample.WinForms.WixSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
#if DEBUG
            const string Build = "Debug";
#else
            const string Build = "Release";
#endif
            var buildPath = Path.GetFullPath(@"..\CefSharp.MinimalExample.WinForms\bin.net472\" + bitness + @"\" + Build + @"\net472");
            var project = new Project("CefSharp.MinimalExample.WinForms",
                          new Dir(@"%ProgramFiles%\CefSharp.MinimalExample.WinForms",
                              new DirFiles($"{buildPath}\\*.*"),
                              new Dir("locales", new DirFiles($"{buildPath}\\locales\\*.*")),
                              new Dir("swiftshader", new DirFiles($"{buildPath}\\swiftshader\\*.*"))),
                          new CustomActionRef("WixCloseApplications", When.Before, Step.CostFinalize, new Condition("VersionNT > 400")),
                          new CloseApplication(new Id("CefSharp.MinimalExample.WinForms"), "CefSharp.MinimalExample.WinForms.net472.exe")
                          {
                              Timeout = 15,
                              EndSessionMessage = true,
                              RebootPrompt = false,
                          },
                          new Property("MsiLogging", "vocewarmup"));
            
            project.GUID = new Guid("9007D203-B084-482F-BC23-5F4BF82EE679");
            project.Platform = Platform.x64;

            Compiler.BuildMsi(project);
        }
    }
}
