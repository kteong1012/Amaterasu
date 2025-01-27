
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using SimpleJSON;
using Luban;
using UnityEngine;
using System.Linq;
using System;

namespace GameEditor.ConfigEditor.Model.Avg
{

public abstract class DialogueBehaviour :  Luban.EditorBeanBase 
{
    public DialogueBehaviour()
    {
    }
    public abstract string GetTypeStr();

    private int _typeIndex = -1;
    private int TypeIndex => _typeIndex;
    private static string[] Types = new string[]
    {
        "NormalBehaviour",
        "OptionBehaviour",
        "DetermineBehaviour",
    };
    private static string[] TypeAlias = new string[]
    {
        "对话",
        "选项",
        "判定",
    };

    public static DialogueBehaviour Create(string type)
    {
        switch (type)
        {
            case "Avg.NormalBehaviour":   
            case "NormalBehaviour":
            {
                var obj = new Avg.NormalBehaviour();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Avg.OptionBehaviour":   
            case "OptionBehaviour":
            {
                var obj = new Avg.OptionBehaviour();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Avg.DetermineBehaviour":   
            case "DetermineBehaviour":
            {
                var obj = new Avg.DetermineBehaviour();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            default: return null;
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderDialogueBehaviour(ref DialogueBehaviour obj)
    {
        UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
        var array = ConfigEditorSettings.showComment ? TypeAlias : Types;
        UnityEditor.EditorGUILayout.BeginHorizontal();
        UnityEditor.EditorGUILayout.LabelField("类型", GUILayout.Width(100));
        var index = UnityEditor.EditorGUILayout.Popup(obj.TypeIndex, array, GUILayout.Width(200));
        if (obj.TypeIndex != index)
        {
            obj = Create(Types[index]);
        }
        UnityEditor.EditorGUILayout.EndHorizontal();
        obj?.Render();
        UnityEditor.EditorGUILayout.EndVertical();
    }

    public override void Render()
    {
    }
    public static DialogueBehaviour LoadJsonDialogueBehaviour(SimpleJSON.JSONNode _json)
    {
        string type = _json["$type"];
        DialogueBehaviour obj;
        switch (type)
        {
            case "Avg.NormalBehaviour":   
            case "NormalBehaviour":
            {
                obj = new Avg.NormalBehaviour(); 
                obj._typeIndex = Array.IndexOf(Types, "NormalBehaviour");
                break;
            }
            case "Avg.OptionBehaviour":   
            case "OptionBehaviour":
            {
                obj = new Avg.OptionBehaviour(); 
                obj._typeIndex = Array.IndexOf(Types, "OptionBehaviour");
                break;
            }
            case "Avg.DetermineBehaviour":   
            case "DetermineBehaviour":
            {
                obj = new Avg.DetermineBehaviour(); 
                obj._typeIndex = Array.IndexOf(Types, "DetermineBehaviour");
                break;
            }
            default: throw new SerializationException();
        }
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonDialogueBehaviour(DialogueBehaviour _obj, SimpleJSON.JSONNode _json)
    {
        _json["$type"] = _obj.GetTypeStr();
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }


    public override string ToString()
    {
        return "{ "
        + "}";
    }
}

}

