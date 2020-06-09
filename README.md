# CefSharp.MinimalExample

Minimal example of how the CefSharp library can be used using the official `Nuget` packages
- .NET Framework solution (`CefSharp.MinimalExample.sln`)
- .NET Core solution (`CefSharp.MinimalExample.netcore.sln`). 

Includes examples for
- WinForms
- WPF
- OffScreen
 
For a more complete example of each project see the main `CefSharp` repository.

## .NET Core support
As of version `75.1.142`, the CefSharp NuGet packages can be used with .NET Core 3.x projects (as shown by the examples). However, the current versions have some limitations that you should be aware of:
- Chromium [multi-process architecure](https://github.com/cefsharp/CefSharp/wiki/General-Usage#processes) launches multiple BrowserSubProcess instances for rendering, gpu acceleration, networking, plugins, etc.
  - By default `CefSharp` provides a default BrowserSubProcess (`CefSharp.BrowserSubprocess.exe`) which requires the .NET Framework 4.5.2 or higher installed.
  - It is possible to `self host` the BrowserSubProcess using your application exe this example was updated to demonstrate this in commit https://github.com/cefsharp/CefSharp.MinimalExample/commit/898eb755c6bb7f504f9b5bdc889ff9142e105848 
  - A new `.Net Core` BrowserSubProcess is in the works at https://github.com/cefsharp/CefSharp.BrowserSubProcess.NetCore
- Additional entires to your csproj/vbproj are required for the CoreCLR to load the `CefSharp.*` libraries (They would not be specified in the `.deps.json` file otherwise). See example below, the netcore.csproj files contained in this example provide a working demo.
- When publishing a self-contained app using a runtime identifier `win-x64` or `win-x86`, you need to set the `Platform` property to `x64` or `x86`; as otherwise it would be `AnyCPU` and the check in the `.targets` file of the NuGet package would fail.<br>
  Example:
  - x86: `dotnet publish -f netcoreapp3.0 -r win-x86 -p:Platform=x86`
  - x64: `dotnet publish -f netcoreapp3.0 -r win-x64 -p:Platform=x64`

It is possible to publish the application as single EXE file by adding `-p:PublishSingleFile=true`.

### .Net Core csproj/vbproj WPF
```xml
<!-- Add the following to your csproj/vbproj file -->
<ItemGroup>
	<Reference Update="CefSharp">
	  <Private>true</Private>
	</Reference>
	<Reference Update="CefSharp.Core">
	  <Private>true</Private>
	</Reference>
	<Reference Update="CefSharp.Wpf">
	  <Private>true</Private>
	</Reference>
</ItemGroup>
```

### .Net Core csproj/vbproj WinForms
```xml
<!-- Add the following to your csproj/vbproj file -->
<ItemGroup>
    <Reference Update="CefSharp">
      <Private>true</Private>
    </Reference>
    <Reference Update="CefSharp.Core">
      <Private>true</Private>
    </Reference>
    <Reference Update="CefSharp.WinForms">
      <Private>true</Private>
    </Reference>
</ItemGroup>
```

### .Net Core csproj/vbproj OffScreen
```xml
<!-- Add the following to your csproj/vbproj file -->
<ItemGroup>
    <Reference Update="CefSharp">
      <Private>true</Private>
    </Reference>
    <Reference Update="CefSharp.Core">
      <Private>true</Private>
    </Reference>
    <Reference Update="CefSharp.OffScreen">
      <Private>true</Private>
    </Reference>
</ItemGroup>
```
