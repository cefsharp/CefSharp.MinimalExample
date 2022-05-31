SET cefsharpversion=102.0.90

..\nuget restore CefSharp.MinimalExample.sln

..\nuget update CefSharp.MinimalExample.OffScreen\CefSharp.MinimalExample.OffScreen.csproj -Id CefSharp.OffScreen -Version %cefsharpversion%
..\nuget update CefSharp.MinimalExample.WinForms\CefSharp.MinimalExample.WinForms.csproj -Id CefSharp.WinForms -Version %cefsharpversion%
..\nuget update CefSharp.MinimalExample.Wpf\CefSharp.MinimalExample.Wpf.csproj -Id CefSharp.Wpf -Version %cefsharpversion%

rem #dotnet add CefSharp.MinimalExample.OffScreen\CefSharp.MinimalExample.OffScreen.net472.csproj package CefSharp.OffScreen -v %cefsharpversion%
dotnet add CefSharp.MinimalExample.WinForms\CefSharp.MinimalExample.WinForms.net472.csproj package CefSharp.WinForms -v %cefsharpversion%
dotnet add CefSharp.MinimalExample.Wpf\CefSharp.MinimalExample.Wpf.net472.csproj package CefSharp.Wpf -v %cefsharpversion%

dotnet add CefSharp.MinimalExample.OffScreen\CefSharp.MinimalExample.OffScreen.netcore.csproj package CefSharp.OffScreen.NetCore -v %cefsharpversion%
dotnet add CefSharp.MinimalExample.WinForms\CefSharp.MinimalExample.WinForms.netcore.csproj package CefSharp.WinForms.NetCore -v %cefsharpversion%
dotnet add CefSharp.MinimalExample.Wpf\CefSharp.MinimalExample.Wpf.netcore.csproj package CefSharp.Wpf.NetCore -v %cefsharpversion%

pause