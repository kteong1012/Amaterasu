
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

public sealed class Or :  ConditionParam 
{
    public Or()
    {
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "Or";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["conditions_1"];
            if (_fieldJson != null)
            {
                
                if (!_fieldJson.IsObject)
                {
                    throw new SerializationException();
                }
                conditions1 = GameEditor.ConfigEditor.Model.ConditionParam.LoadJsonConditionParam(_fieldJson);
            }
            else
            {
                conditions1 = ConditionParam.Create("Always");
            }
        }
        
        { 
            var _fieldJson = _json["conditions_2"];
            if (_fieldJson != null)
            {
                
                if (!_fieldJson.IsObject)
                {
                    throw new SerializationException();
                }
                conditions2 = GameEditor.ConfigEditor.Model.ConditionParam.LoadJsonConditionParam(_fieldJson);
            }
            else
            {
                conditions2 = ConditionParam.Create("Always");
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {

        if (conditions1 != null)
        {
            { var __bjson = new JSONObject();  GameEditor.ConfigEditor.Model.ConditionParam.SaveJsonConditionParam(conditions1, __bjson); _json["conditions_1"] = __bjson; }
        }

        if (conditions2 != null)
        {
            { var __bjson = new JSONObject();  GameEditor.ConfigEditor.Model.ConditionParam.SaveJsonConditionParam(conditions2, __bjson); _json["conditions_2"] = __bjson; }
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderOr(Or obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("条件1", "conditions_1"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("conditions_1", "条件1"), GUILayout.Width(100));
}
{
    if (this.conditions1 == null)
{   
    this.conditions1 = ConditionParam.Create("Always");
}
    ConditionParam.RenderConditionParam(ref this.conditions1);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("条件2", "conditions_2"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("conditions_2", "条件2"), GUILayout.Width(100));
}
{
    if (this.conditions2 == null)
{   
    this.conditions2 = ConditionParam.Create("Always");
}
    ConditionParam.RenderConditionParam(ref this.conditions2);
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static Or LoadJsonOr(SimpleJSON.JSONNode _json)
    {
        Or obj = new Or();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonOr(Or _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 条件1
    /// </summary>
    public GameEditor.ConfigEditor.Model.ConditionParam conditions1;
    /// <summary>
    /// 条件2
    /// </summary>
    public GameEditor.ConfigEditor.Model.ConditionParam conditions2;

    public override string ToString()
    {
        return "{ "
        + "conditions1:" + conditions1 + ","
        + "conditions2:" + conditions2 + ","
        + "}";
    }
}

}
