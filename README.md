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
| [CefSharp.MinimalExample.netcore.sln](https://github.com/cefsharp/CefSharp.MinimalExample/blob/master/CefSharp.MinimalExample.netcore.sln) | .Net Core 3.1/5.0/6.0/7.0 | Newer SDK Style projects that target .Net Core 3.1, .Net 5.0, .Net 6.0 and .Net 7.0 using PackageReference | 
 
For a more complete example of each project see the main `CefSharp` repository.

## .NET Core support

For **.Net Core 3.1/Net 5/6/7** follow the [Quick Start](https://github.com/cefsharp/CefSharp/wiki/Quick-Start-For-MS-.Net-5.0-or-greater).

**Publish Example**
```
# Publish x86
dotnet publish -f netcoreapp3.1 -r win-x86
# Publish x64
dotnet publish -f netcoreapp3.1 -r win-x64
# Publish using current runtime identifier https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish
dotnet publish -f netcoreapp3.1 --use-current-runtime
```

For publishing as single file you need to [Self Host the BrowserSubProcess](https://github.com/cefsharp/CefSharp/wiki/SelfHost-BrowserSubProcess), more details in https://github.com/cefsharp/CefSharp/issues/3407#issuecomment-787008626

Any problems please report them on https://github.com/cefsharp/CefSharp/discussions

## .NET 5/6/7 Support

For **.Net 5/6/7** follow the [Quick Start](https://github.com/cefsharp/CefSharp/wiki/Quick-Start-For-MS-.Net-5.0-or-greater).

