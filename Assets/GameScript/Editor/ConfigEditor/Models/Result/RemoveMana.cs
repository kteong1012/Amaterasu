
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

namespace GameEditor.ConfigEditor.Model.Result
{

public sealed class RemoveMana :  Result.DataResult 
{
    public RemoveMana()
    {
            targetType = GameEditor.ConfigEditor.Model.Result.TargetType.Caster;
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "RemoveMana";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["target_type"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { targetType = (GameEditor.ConfigEditor.Model.Result.TargetType)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.Result.TargetType), _fieldJson); } else if(_fieldJson.IsNumber) { targetType = (GameEditor.ConfigEditor.Model.Result.TargetType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                targetType = GameEditor.ConfigEditor.Model.Result.TargetType.Caster;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["target_type"] = new JSONNumber((int)targetType);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderRemoveMana(RemoveMana obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("目标类型", "target_type"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("target_type", "目标类型"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.targetType;
        var __alias1 = (Result.TargetType_Alias)this.targetType;
        __alias1 = (Result.TargetType_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = Result.TargetType_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.targetType = (GameEditor.ConfigEditor.Model.Result.TargetType)__item1.Value;
    }
    else
    {
        this.targetType = (GameEditor.ConfigEditor.Model.Result.TargetType)UnityEditor.EditorGUILayout.EnumPopup(this.targetType, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static RemoveMana LoadJsonRemoveMana(SimpleJSON.JSONNode _json)
    {
        RemoveMana obj = new Result.RemoveMana();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonRemoveMana(RemoveMana _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 目标类型
    /// </summary>
    public GameEditor.ConfigEditor.Model.Result.TargetType targetType;

    public override string ToString()
    {
        return "{ "
        + "targetType:" + targetType + ","
        + "}";
    }
}

}
