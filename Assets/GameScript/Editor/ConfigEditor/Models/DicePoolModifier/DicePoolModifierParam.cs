
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

namespace GameEditor.ConfigEditor.Model.DicePoolModifier
{

public abstract class DicePoolModifierParam :  Luban.EditorBeanBase 
{
    public DicePoolModifierParam()
    {
    }
    public abstract string GetTypeStr();

    private int _typeIndex = -1;
    private int TypeIndex => _typeIndex;
    private static string[] Types = new string[]
    {
        "CommonModifier",
    };
    private static string[] TypeAlias = new string[]
    {
        "一般修正",
    };

    public static DicePoolModifierParam Create(string type)
    {
        switch (type)
        {
            case "DicePoolModifier.CommonModifier":   
            case "CommonModifier":
            {
                var obj = new DicePoolModifier.CommonModifier();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            default: return null;
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderDicePoolModifierParam(ref DicePoolModifierParam obj)
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
    public static DicePoolModifierParam LoadJsonDicePoolModifierParam(SimpleJSON.JSONNode _json)
    {
        string type = _json["$type"];
        DicePoolModifierParam obj;
        switch (type)
        {
            case "DicePoolModifier.CommonModifier":   
            case "CommonModifier":
            {
                obj = new DicePoolModifier.CommonModifier(); 
                obj._typeIndex = Array.IndexOf(Types, "CommonModifier");
                break;
            }
            default: throw new SerializationException();
        }
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonDicePoolModifierParam(DicePoolModifierParam _obj, SimpleJSON.JSONNode _json)
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

