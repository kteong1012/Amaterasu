@echo off
set PROJECT_ROOT=..
set CONFIG_ROOT=%PROJECT_ROOT%\Config
set LUBAN_DLL=%CONFIG_ROOT%\Luban\src\Luban\bin\Debug\net8.0\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    --conf %CONF_ROOT%\luban.conf ^
    -d json ^
    -t client ^
    -x json.outputDataDir=Output\Json

pause