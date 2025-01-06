@set PROC=..\..\Tools\Localiztion\LocalizationTool\bin\Debug\net8.0\LocalizationTool.dll

@dotnet %PROC% Patch -translateFolderPath Translate -patchFolderPath Patch/Results -languages zh_CN,en_US,ja_JP

@pause