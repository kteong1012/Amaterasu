
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

public sealed class SelfClone :  Result.DataResult 
{
    public SelfClone()
    {
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "SelfClone";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["clone_count"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  cloneCount = _fieldJson;
            }
            else
            {
                cloneCount = 0;
            }
        }
        
        { 
            var _fieldJson = _json["need_add_state_tag"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsBoolean) { throw new SerializationException(); }  needAddStateTag = _fieldJson;
            }
            else
            {
                needAddStateTag = false;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["clone_count"] = new JSONNumber(cloneCount);
        }
        {
            _json["need_add_state_tag"] = new JSONBool(needAddStateTag);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderSelfClone(SelfClone obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("克隆数量", "clone_count"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("clone_count", "克隆数量"), GUILayout.Width(100));
}
this.cloneCount = UnityEditor.EditorGUILayout.IntField(this.cloneCount, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("是否需要添加状态标签", "need_add_state_tag"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("need_add_state_tag", "是否需要添加状态标签"), GUILayout.Width(100));
}
this.needAddStateTag = UnityEditor.EditorGUILayout.Toggle(this.needAddStateTag, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static SelfClone LoadJsonSelfClone(SimpleJSON.JSONNode _json)
    {
        SelfClone obj = new Result.SelfClone();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonSelfClone(SelfClone _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 克隆数量
    /// </summary>
    public int cloneCount;
    /// <summary>
    /// 是否需要添加状态标签
    /// </summary>
    public bool needAddStateTag;

    public override string ToString()
    {
        return "{ "
        + "cloneCount:" + cloneCount + ","
        + "needAddStateTag:" + needAddStateTag + ","
        + "}";
    }
}

}
