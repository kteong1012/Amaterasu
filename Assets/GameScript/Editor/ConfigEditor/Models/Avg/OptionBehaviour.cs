
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

public sealed class OptionBehaviour :  Avg.DialogueBehaviour 
{
    public OptionBehaviour()
    {
            options = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Avg.Option>();
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "OptionBehaviour";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["options"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } options = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Avg.Option>(); foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { GameEditor.ConfigEditor.Model.Avg.Option __v0;  if(!__e0.IsObject) { throw new SerializationException(); }  __v0 = GameEditor.ConfigEditor.Model.Avg.Option.LoadJsonOption(__e0);  options.Add(__v0); }  
            }
            else
            {
                options = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Avg.Option>();
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {

        if (options != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in options) { { var __bjson = new JSONObject();  GameEditor.ConfigEditor.Model.Avg.Option.SaveJsonOption(__e0, __bjson); __cjson0["null"] = __bjson; } } __cjson0.Inline = __cjson0.Count == 0; _json["options"] = __cjson0; }
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderOptionBehaviour(OptionBehaviour obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("选项列表", "options"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("options", "选项列表"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.options.Count;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            this.options.RemoveAt(__i1);
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        GameEditor.ConfigEditor.Model.Avg.Option __e1 = this.options[__i1];
        {
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("下句id", "next_id"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("next_id", "下句id"), GUILayout.Width(100));
}
__e1.nextId = UnityEditor.EditorGUILayout.IntField(__e1.nextId, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("可点击条件", "inner_condition"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("inner_condition", "可点击条件"), GUILayout.Width(100));
}
{
    if (__e1.innerCondition == null)
{   
    __e1.innerCondition = ConditionParam.Create("Always");
}
    ConditionParam.RenderConditionParam(ref __e1.innerCondition);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("可显示条件", "outer_condition"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("outer_condition", "可显示条件"), GUILayout.Width(100));
}
{
    if (__e1.outerCondition == null)
{   
    __e1.outerCondition = ConditionParam.Create("Always");
}
    ConditionParam.RenderConditionParam(ref __e1.outerCondition);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("不可点击文本", "additional_content"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("additional_content", "不可点击文本"), GUILayout.Width(100));
}
__e1.additionalContent = UnityEditor.EditorGUILayout.TextField(__e1.additionalContent, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(__e1.additionalContent,__x =>__e1.additionalContent = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("选项文本", "text"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("text", "选项文本"), GUILayout.Width(100));
}
__e1.text = UnityEditor.EditorGUILayout.TextField(__e1.text, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(__e1.text,__x =>__e1.text = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
};
        this.options[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        GameEditor.ConfigEditor.Model.Avg.Option __e1;
        __e1 = new Avg.Option();;
        this.options.Add(__e1);
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            GameEditor.ConfigEditor.Model.Avg.Option __importElement1;
            if(!__importJson1.IsObject) { throw new SerializationException(); }  __importElement1 = GameEditor.ConfigEditor.Model.Avg.Option.LoadJsonOption(__importJson1);
            this.options.Add(__importElement1);
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static OptionBehaviour LoadJsonOptionBehaviour(SimpleJSON.JSONNode _json)
    {
        OptionBehaviour obj = new Avg.OptionBehaviour();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonOptionBehaviour(OptionBehaviour _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 选项列表
    /// </summary>
    public System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Avg.Option> options;

    public override string ToString()
    {
        return "{ "
        + "options:" + Luban.StringUtil.CollectionToString(options) + ","
        + "}";
    }
}

}
