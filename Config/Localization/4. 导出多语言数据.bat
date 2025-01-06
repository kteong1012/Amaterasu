@set PROC=..\..\Tools\Localiztion\LocalizationTool\bin\Debug\net8.0\LocalizationTool.dll

@dotnet %PROC% Export -translateFolderPath Translate -outputDir Json -dataType Json -languages zh_CN,en_US,ja_JP
@dotnet %PROC% Export -translateFolderPath Translate -outputDir ../../Assets/GameRes/Raw/Localization/Bytes -dataType Bytes -languages zh_CN,en_US,ja_JP

@pause