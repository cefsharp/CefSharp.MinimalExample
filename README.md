# CefSharp.MinimalExample

Minimal example of how the CefSharp library can be used using the official `Nuget` packages with .NET Framework
projects (`CefSharp.MnimalExample.sln`) and .NET Core projects (`CefSharp.MinimalExample.netcore.sln`). 

Includes examples for
- WinForms
- WPF
- OffScreen
 

For a more complete example of each project see the main `CefSharp` repository.

## .NET Core support
As of version `75.1.142`, the CefSharp NuGet packages can be used with .NET Core 3.0 projects (as shown by the examples). However, the current versions have some limitations that you should be aware of:
- The target machine still needs to have .NET Framework 4.5.2 or higher installed, as the `CefSharp.BrowserSubprocess.exe` is still used.
- The project file needs to update the references of `CefSharp.WinForms`/`CefSharp.WPF`/`CefSharp.OffScreen`, as well as `CefSharp.Core` and `CefSharp` to use `<Private>true</Private>`, as otherwise the CoreCLR would not load these libraries as they would not be specified in the `.deps.json` file.
- When publishing a self-contained app using a runtime identifier `win-x64` or `win-x86`, you need to set the `Platform` property to `x64` or `x86`; as otherwise it would be `AnyCPU` and the check in the `.targets` file of the NuGet package would fail.<br>
  Example:
  - x86: `dotnet publish -f netcoreapp3.0 -r win-x86 -p:Platform=x86`
  - x64: `dotnet publish -f netcoreapp3.0 -r win-x64 -p:Platform=x64`

It is possible to publish the application as single EXE file by adding `-p:PublishSingleFile=true`.