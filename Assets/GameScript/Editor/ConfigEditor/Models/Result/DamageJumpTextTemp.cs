
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

public sealed class DamageJumpTextTemp :  Result.VisualResult 
{
    public DamageJumpTextTemp()
    {
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "DamageJumpTextTemp";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["duration"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  duration = _fieldJson;
            }
            else
            {
                duration = 0;
            }
        }
        
        { 
            var _fieldJson = _json["preview_delay"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  previewDelay = _fieldJson;
            }
            else
            {
                previewDelay = 0;
            }
        }
        
        { 
            var _fieldJson = _json["post_delay"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  postDelay = _fieldJson;
            }
            else
            {
                postDelay = 0;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["duration"] = new JSONNumber(duration);
        }
        {
            _json["preview_delay"] = new JSONNumber(previewDelay);
        }
        {
            _json["post_delay"] = new JSONNumber(postDelay);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderDamageJumpTextTemp(DamageJumpTextTemp obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("渐隐时间", "duration"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("duration", "渐隐时间"), GUILayout.Width(100));
}
this.duration = UnityEditor.EditorGUILayout.DoubleField(this.duration, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("前间隔", "preview_delay"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("preview_delay", "前间隔"), GUILayout.Width(100));
}
this.previewDelay = UnityEditor.EditorGUILayout.DoubleField(this.previewDelay, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("后间隔", "post_delay"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("post_delay", "后间隔"), GUILayout.Width(100));
}
this.postDelay = UnityEditor.EditorGUILayout.DoubleField(this.postDelay, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static DamageJumpTextTemp LoadJsonDamageJumpTextTemp(SimpleJSON.JSONNode _json)
    {
        DamageJumpTextTemp obj = new Result.DamageJumpTextTemp();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonDamageJumpTextTemp(DamageJumpTextTemp _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 渐隐时间
    /// </summary>
    public double duration;
    /// <summary>
    /// 前间隔
    /// </summary>
    public double previewDelay;
    /// <summary>
    /// 后间隔
    /// </summary>
    public double postDelay;

    public override string ToString()
    {
        return "{ "
        + "duration:" + duration + ","
        + "previewDelay:" + previewDelay + ","
        + "postDelay:" + postDelay + ","
        + "}";
    }
}

}
