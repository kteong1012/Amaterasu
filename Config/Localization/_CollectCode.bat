@chcp 65001
@set PROC=..\..\Tools\Localiztion\CSharpTranslatePicker\bin\Debug\net8.0\CSharpTranslatePicker.dll
@set UNITY_SOLUTION_PATH=..\..\Unity.sln

@dotnet %PROC% %UNITY_SOLUTION_PATH% .\Source\code.txt

@pause