@set LUBAN_DLL=..\Luban\src\Luban\bin\Debug\net8.0\Luban.dll

dotnet %LUBAN_DLL% ^
-t client ^
-d text-list ^
--conf ..\luban.conf ^
--validationFailAsError ^
-x outputDataDir=Source ^
-x outputSaver.text-list.cleanUpOutputDir=0 ^
-x l10n.textListFile=config.txt

@pause