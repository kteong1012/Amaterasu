
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

public sealed class DirectSettleSkillAction :  SkillAction 
{
    public DirectSettleSkillAction()
    {
            results = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam>();
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "DirectSettleSkillAction";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["results"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } results = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam>(); foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { GameEditor.ConfigEditor.Model.Result.ResultParam __v0;  
                if (!__e0.IsObject)
                {
                    throw new SerializationException();
                }
                __v0 = GameEditor.ConfigEditor.Model.Result.ResultParam.LoadJsonResultParam(__e0);  results.Add(__v0); }  
            }
            else
            {
                results = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam>();
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {

        if (results != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in results) { { var __bjson = new JSONObject();  GameEditor.ConfigEditor.Model.Result.ResultParam.SaveJsonResultParam(__e0, __bjson); __cjson0["null"] = __bjson; } } __cjson0.Inline = __cjson0.Count == 0; _json["results"] = __cjson0; }
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderDirectSettleSkillAction(DirectSettleSkillAction obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("结果", "results"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("results", "结果"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.results.Count;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            this.results.RemoveAt(__i1);
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        GameEditor.ConfigEditor.Model.Result.ResultParam __e1 = this.results[__i1];
        {
    if (__e1 == null)
{   
    __e1 = Result.ResultParam.Create("DifferDmg");
}
    Result.ResultParam.RenderResultParam(ref __e1);
};
        this.results[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        GameEditor.ConfigEditor.Model.Result.ResultParam __e1;
        __e1 = Result.ResultParam.Create("DifferDmg");;
        this.results.Add(__e1);
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            GameEditor.ConfigEditor.Model.Result.ResultParam __importElement1;
            
if (!__importJson1.IsObject)
{
    throw new SerializationException();
}
__importElement1 = GameEditor.ConfigEditor.Model.Result.ResultParam.LoadJsonResultParam(__importJson1);
            this.results.Add(__importElement1);
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static DirectSettleSkillAction LoadJsonDirectSettleSkillAction(SimpleJSON.JSONNode _json)
    {
        DirectSettleSkillAction obj = new DirectSettleSkillAction();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonDirectSettleSkillAction(DirectSettleSkillAction _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 结果
    /// </summary>
    public System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam> results;

    public override string ToString()
    {
        return "{ "
        + "results:" + Luban.StringUtil.CollectionToString(results) + ","
        + "}";
    }
}

}

