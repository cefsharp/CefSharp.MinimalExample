function Generate-Installer
{
    # 1. Install Wix Toolset: https://wixtoolset.org/releases/
    # 2. Download Paraffin, copy Paraffin.exe to Wix install directory: https://github.com/Wintellect/Paraffin/releases
    # 3. Build MinimalExample.WPF.netcore in release
    $PARAFFIN = "C:\Program Files (x86)\WiX Toolset v3.11\bin\Paraffin.exe"
    $CANDLE = "C:\Program Files (x86)\WiX Toolset v3.11\bin\candle.exe"
    $LIGHT = "C:\Program Files (x86)\WiX Toolset v3.11\bin\light.exe"

    $SetupDir = "CefSharp.MinimalExample.Wpf.Setup"
    $BuildDir = "CefSharp.MinimalExample.Wpf\bin.netcore\Release\net5.0-windows"
    $TempDir = "CefSharp.MinimalExample.Wpf\bin.netcore\Release"
    $ExeName = "CefSharp.MinimalExample.Wpf.netcore.exe"

    # Temporarily move exe so it isn't duplicated in app.wxs
    Move-Item $BuildDir\$ExeName $TempDir\$ExeName
    # Generate app.wxs, contains all files in build output. Probably overkill to include everything.
    & $PARAFFIN -dir $BuildDir -gn AppComponents -dirref INSTALLFOLDER -NoRootDirectory $SetupDir\app.wxs -includeFile Variables.wxi -alias "`$(var.SOURCE)"
    # Move exe back
    Move-Item $TempDir\$ExeName $BuildDir\$ExeName

    & $CANDLE -ext WixNetFxExtension -ext WixUtilExtension.dll -out $SetupDir\ $SetupDir\Product.wxs $SetupDir\app.wxs -arch x64
    # Unfortunately I don't know what these ICE things are doing, but they were in our installer so I copied them here as well
    # ICE40: ReinstallMode
    # ICE61: AllowSameVersionUpgrades
    # ICE80: x64 registry key
    & $LIGHT -ext WixUIExtension -ext WixNetFxExtension -ext WixUtilExtension.dll -sice:ICE40 -sice:ICE80 -sice:ICE61 -out $SetupDir\Example_Installer.msi  $SetupDir\Product.wixobj  $SetupDir\app.wixobj
}

Generate-Installer