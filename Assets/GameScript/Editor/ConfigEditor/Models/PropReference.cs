
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

namespace GameEditor.ConfigEditor.Model
{

public sealed class PropReference :  Luban.EditorBeanBase 
{
    public PropReference()
    {
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["PropId"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  PropId = _fieldJson;
            }
            else
            {
                PropId = 0;
            }
        }
        
        { 
            var _fieldJson = _json["ChangeNum"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  ChangeNum = _fieldJson;
            }
            else
            {
                ChangeNum = 0;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["PropId"] = new JSONNumber(PropId);
        }
        {
            _json["ChangeNum"] = new JSONNumber(ChangeNum);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderPropReference(PropReference obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("道具Id", "PropId"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("PropId", "道具Id"), GUILayout.Width(100));
}
this.PropId = UnityEditor.EditorGUILayout.IntField(this.PropId, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("减少数量", "ChangeNum"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("ChangeNum", "减少数量"), GUILayout.Width(100));
}
this.ChangeNum = UnityEditor.EditorGUILayout.IntField(this.ChangeNum, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static PropReference LoadJsonPropReference(SimpleJSON.JSONNode _json)
    {
        PropReference obj = new PropReference();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonPropReference(PropReference _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 道具Id
    /// </summary>
    public int PropId;
    /// <summary>
    /// 减少数量
    /// </summary>
    public int ChangeNum;

    public override string ToString()
    {
        return "{ "
        + "PropId:" + PropId + ","
        + "ChangeNum:" + ChangeNum + ","
        + "}";
    }
}

}
