# CefSharp.MinimalExample

Minimal example of how the CefSharp library can be used using the official `Nuget` packages.

This project includes examples for
- WinForms
- WPF
- OffScreen

| Solution | .Net Version | Description |
|--------|--------|--------|
| [CefSharp.MinimalExample.sln](https://github.com/cefsharp/CefSharp.MinimalExample/blob/master/CefSharp.MinimalExample.sln)         | .Net 4.5.2 | Older Non-SDK Style projects that target .Net 4.5.2 and use packages.config |
| [CefSharp.MinimalExample.net472.sln](https://github.com/cefsharp/CefSharp.MinimalExample/blob/master/CefSharp.MinimalExample.net472.sln)  | .Net 4.7.2 | Newer SDK Style projects that target .Net 4.7.2 and use PackageReference |
| [CefSharp.MinimalExample.netcore.sln](https://github.com/cefsharp/CefSharp.MinimalExample/blob/master/CefSharp.MinimalExample.netcore.sln) | .Net Core 3.1/5.0/6.0/7.0 | Newer SDK Stlye projects that target .Net Core 3.1, .Net 5.0, .Net 6.0 and .Net 7.0 using PackageReference | 
 
For a more complete example of each project see the main `CefSharp` repository.

## .NET Core support

For `.Net Core 3.1/Net 5/6/7` use the following packages:
- https://www.nuget.org/packages/CefSharp.WinForms.NETCore
- https://www.nuget.org/packages/CefSharp.Wpf.NETCore
- https://www.nuget.org/packages/CefSharp.OffScreen.NETCore
- Publish Example
  - x86: `dotnet publish -f netcoreapp3.1 -r win-x86
  - x64: `dotnet publish -f netcoreapp3.1 -r win-x64

It is possible to publish the application as single EXE file by adding `-p:PublishSingleFile=true`.

Any problems please report them on https://github.com/cefsharp/CefSharp/issues/3197

## .NET 5/6/7 Support

The same packages listed above in the .Net Core section should be used for .Net 5.0/6.0/7.0

