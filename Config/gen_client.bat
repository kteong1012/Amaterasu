@echo off
set PROJECT_ROOT=..
set GAME_CLIENT_ROOT=%PROJECT_ROOT%
set CONFIG_ROOT=%PROJECT_ROOT%\Config
set LUBAN_DLL=%CONFIG_ROOT%\Luban\src\Luban\bin\Debug\net8.0\Luban.dll
set CONF_ROOT=.

@echo =================== gen client ===================
dotnet %LUBAN_DLL% ^
    --conf %CONF_ROOT%\luban.conf ^
    --customTemplateDir %CONFIG_ROOT%\Luban\CustomTemplate ^
    -t client ^
    -d bin ^
    -d json ^
    -c cs-bin ^
    -x bin.outputDataDir=%GAME_CLIENT_ROOT%\Assets\GameRes\Raw\Config\Bytes ^
    -x json.outputDataDir=%GAME_CLIENT_ROOT%\Assets\GameRes\Raw\Config\Json ^
    -x outputCodeDir=%GAME_CLIENT_ROOT%\Assets\GameScript\HotUpdate\Config\Generate

pause