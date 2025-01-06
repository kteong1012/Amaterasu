setlocal enabledelayedexpansion
@echo off
set PROJECT_ROOT=..
set GAME_CLIENT_ROOT=%PROJECT_ROOT%
set CONFIG_ROOT=%PROJECT_ROOT%\Config
set LUBAN_DLL=%CONFIG_ROOT%\Luban\src\Luban\bin\Debug\net8.0\Luban.dll
set CONF_ROOT=.

@echo =================== read config ===================


set EDITOR_TABLES=
REM 按行读取 input.txt 文件
for /f "delims=" %%i in (EditorTables.txt) do (
    set line=%%i
    REM EDITOR_TABLES += "-o " + line + " "
    set EDITOR_TABLES=!EDITOR_TABLES! -o !line!
)

echo read editor tables: !EDITOR_TABLES!


@echo =================== gen editor ===================
dotnet %LUBAN_DLL% ^
    --conf %CONF_ROOT%\luban.conf ^
    --customTemplateDir %CONFIG_ROOT%\Luban\CustomTemplate ^
    -t editor ^
    -c cs-unity-gui-json ^
    -d json3 ^
    -o !EDITOR_TABLES! ^
    -x jsonStrict=false ^
    -x outputCodeDir=%GAME_CLIENT_ROOT%\Assets\GameScript\Editor\ConfigEditor\Models ^
    -x outputDataDir=%CONF_ROOT%\Datas\DataJson


pause
endlocal