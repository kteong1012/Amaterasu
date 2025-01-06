@set PROC=..\..\Tools\Localiztion\LocalizationTool\bin\Debug\net8.0\LocalizationTool.dll

@dotnet %PROC% Collect -mainExcelPath Main.xlsx -codeTextPath Source/code.txt -prefabTextPath Source/prefab.txt -configTextPath Source/config.txt

@pause