
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

public sealed class AssignCounter :  Result.DataResult 
{
    public AssignCounter()
    {
            counter = GameEditor.ConfigEditor.Model.Counter.CounterName.Counter1;
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "AssignCounter";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["counter"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { counter = (GameEditor.ConfigEditor.Model.Counter.CounterName)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.Counter.CounterName), _fieldJson); } else if(_fieldJson.IsNumber) { counter = (GameEditor.ConfigEditor.Model.Counter.CounterName)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                counter = GameEditor.ConfigEditor.Model.Counter.CounterName.Counter1;
            }
        }
        
        { 
            var _fieldJson = _json["value"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  value = _fieldJson;
            }
            else
            {
                value = 0;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["counter"] = new JSONNumber((int)counter);
        }
        {
            _json["value"] = new JSONNumber(value);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderAssignCounter(AssignCounter obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("计数器", "counter"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("counter", "计数器"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.counter;
        var __alias1 = (Counter.CounterName_Alias)this.counter;
        __alias1 = (Counter.CounterName_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = Counter.CounterName_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.counter = (GameEditor.ConfigEditor.Model.Counter.CounterName)__item1.Value;
    }
    else
    {
        this.counter = (GameEditor.ConfigEditor.Model.Counter.CounterName)UnityEditor.EditorGUILayout.EnumPopup(this.counter, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("值", "value"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("value", "值"), GUILayout.Width(100));
}
this.value = UnityEditor.EditorGUILayout.IntField(this.value, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static AssignCounter LoadJsonAssignCounter(SimpleJSON.JSONNode _json)
    {
        AssignCounter obj = new Result.AssignCounter();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonAssignCounter(AssignCounter _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 计数器
    /// </summary>
    public GameEditor.ConfigEditor.Model.Counter.CounterName counter;
    /// <summary>
    /// 值
    /// </summary>
    public int value;

    public override string ToString()
    {
        return "{ "
        + "counter:" + counter + ","
        + "value:" + value + ","
        + "}";
    }
}

}
