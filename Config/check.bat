@echo off
set PROJECT_ROOT=..
set GAME_CLIENT_ROOT=%PROJECT_ROOT%\GameClient
set GAME_SERVER_ROOT=%PROJECT_ROOT%\GameServer
set CONFIG_ROOT=%PROJECT_ROOT%\Config
set LUBAN_DLL=%CONFIG_ROOT%\Luban\src\Luban\bin\Debug\net7.0\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    --conf %CONF_ROOT%\luban.conf ^
    -t client ^
    -x forceLoadDatas=1

echo.
echo.
echo.
echo.
echo.

if %ERRORLEVEL% NEQ 0 (
    REM 颜色红色
    color 0c
    echo 检查不通过
    echo.
    echo.
    echo.
    echo.
    echo.
    pause
    exit /b %ERRORLEVEL%
)

REM 颜色绿色
color 0a
echo 检查通过

echo.
echo.
echo.
echo.
echo.

pause