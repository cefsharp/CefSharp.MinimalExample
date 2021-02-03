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

For `.Net Core 3.1/Net 5` the following packages are now on Nuget.org
- https://www.nuget.org/packages/CefSharp.WinForms.NETCore
- https://www.nuget.org/packages/CefSharp.Wpf.NETCore
- https://www.nuget.org/packages/CefSharp.OffScreen.NETCore
- Publish Example
  - x86: `dotnet publish -f netcoreapp3.1 -r win-x86
  - x64: `dotnet publish -f netcoreapp3.1 -r win-x64

It is possible to publish the application as single EXE file by adding `-p:PublishSingleFile=true`.

Any problems please report them on https://github.com/cefsharp/CefSharp/issues/3197

## .NET 5 Support

The same packages listed above in the .Net Core section should be used for .Net 5.0

