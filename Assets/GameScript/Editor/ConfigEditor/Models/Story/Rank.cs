
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

namespace GameEditor.ConfigEditor.Model.Story
{

public sealed class Rank :  Luban.EditorBeanBase 
{
    public Rank()
    {
            buffList = System.Array.Empty<int>();
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["story_rank"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  storyRank = _fieldJson;
            }
            else
            {
                storyRank = 0;
            }
        }
        
        { 
            var _fieldJson = _json["buff_list"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int __n0 = _fieldJson.Count; buffList = new int[__n0]; int __i0=0; foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { int __v0;  if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0;  buffList[__i0++] = __v0; }  
            }
            else
            {
                buffList = System.Array.Empty<int>();
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["story_rank"] = new JSONNumber(storyRank);
        }

        if (buffList != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in buffList) { __cjson0["null"] = new JSONNumber(__e0); } __cjson0.Inline = __cjson0.Count == 0; _json["buff_list"] = __cjson0; }
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderRank(Rank obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("story_rank", "story_rank"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("story_rank", ""), GUILayout.Width(100));
}
this.storyRank = UnityEditor.EditorGUILayout.IntField(this.storyRank, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("buff_list", "buff_list"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("buff_list", ""), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.buffList.Length;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            var __list1 = new System.Collections.Generic.List<int>(this.buffList);
            __list1.RemoveAt(__i1);
            this.buffList = __list1.ToArray();
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        int __e1 = this.buffList[__i1];
        __e1 = UnityEditor.EditorGUILayout.IntField(__e1, GUILayout.Width(150));;
        this.buffList[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        var __list1 = new System.Collections.Generic.List<int>(this.buffList);
        int __e1;
        __e1 = 0;;
        __list1.Add(__e1);
        this.buffList = __list1.ToArray();
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            int __importElement1;
            if(!__importJson1.IsNumber) { throw new SerializationException(); }  __importElement1 = __importJson1;
            var __list1 = new System.Collections.Generic.List<int>(this.buffList);
            __list1.Add(__importElement1);
            this.buffList = __list1.ToArray();
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static Rank LoadJsonRank(SimpleJSON.JSONNode _json)
    {
        Rank obj = new Story.Rank();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonRank(Rank _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public int storyRank;
    public int[] buffList;

    public override string ToString()
    {
        return "{ "
        + "storyRank:" + storyRank + ","
        + "buffList:" + Luban.StringUtil.CollectionToString(buffList) + ","
        + "}";
    }
}

}
