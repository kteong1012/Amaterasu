using UnityEditor;
using UnityEngine;
using UnityEditor.PackageManager;
using System.Linq;
using UnityEditor.Build.Content;

public class LogLevelCheck : Editor
{
    // ������Ҫ���ĺ궨��
    private static readonly string[] symbols = { "LOG_DEBUG", "LOG_INFO", "LOG_WARNING" };

    private void Awake()
    {
        CheckSymbolState();
    }

    [MenuItem("LogLevel/Debug")]
    private static void SetDebugSymbol()
    {
        SetSymbolState("LOG_DEBUG");
    }

    private static void CheckSymbolState()
    {
        foreach (string symbol in symbols)
        {
            bool isEnabled = IsSymbolEnabled(symbol);
            Menu.SetChecked("LogLevel/" + GetSymbolName(symbol), isEnabled);
        }
    }

    // ���ú궨���״̬
    private static void SetSymbolState(string symbol)
    {
        var target = EditorUserBuildSettings.activeBuildTarget;
        var group = BuildPipeline.GetBuildTargetGroup(target);

        var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
        var symbols = defines.Split(';').Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
        var contains = symbols.Contains(symbol);
        if (contains)
        {
            symbols.Remove(symbol);
        }
        else
        {
            symbols.Add(symbol);
        }
        defines = string.Join(";", symbols.ToArray());
        PlayerSettings.SetScriptingDefineSymbolsForGroup(group, defines);
        CheckSymbolState();
    }

    // ���궨���Ƿ�����
    private static bool IsSymbolEnabled(string symbol)
    {
        var target = EditorUserBuildSettings.activeBuildTarget;
        var group = BuildPipeline.GetBuildTargetGroup(target);
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
        return defines.Contains(symbol);
    }

    // ��ȡ�궨�������
    private static string GetSymbolName(string symbol)
    {
        var shortName = symbol.Replace("LOG_", "");
        var name = char.ToUpper(shortName[0]) + shortName.Substring(1).ToLower();
        return name;
    }
}