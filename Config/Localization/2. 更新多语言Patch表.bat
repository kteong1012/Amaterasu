@set PROC=..\..\Tools\Localiztion\LocalizationTool\bin\Debug\net8.0\LocalizationTool.dll

@dotnet %PROC% Update -mainExcelPath Main.xlsx -translateFolderPath Translate -exportPatchFolderPath Patch/Tasks -languages zh_CN,en_US,ja_JP

@pause