
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

public sealed class PropConfig :  Luban.EditorBeanBase 
{
    public PropConfig()
    {
            Name = "";
            Describe = "";
            PropType = GameEditor.ConfigEditor.Model.PropType.None;
            IconPath = "";
            PropReferenceVaule = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.PropReference>();
            ReferenceActionName = "";
            SpecialEffect = "";
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["Id"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Id = _fieldJson;
            }
            else
            {
                Id = 0;
            }
        }
        
        { 
            var _fieldJson = _json["Name"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  Name = _fieldJson;
            }
            else
            {
                Name = "";
            }
        }
        
        { 
            var _fieldJson = _json["Describe"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  Describe = _fieldJson;
            }
            else
            {
                Describe = "";
            }
        }
        
        { 
            var _fieldJson = _json["PropType"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { PropType = (GameEditor.ConfigEditor.Model.PropType)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.PropType), _fieldJson); } else if(_fieldJson.IsNumber) { PropType = (GameEditor.ConfigEditor.Model.PropType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                PropType = GameEditor.ConfigEditor.Model.PropType.None;
            }
        }
        
        { 
            var _fieldJson = _json["IconPath"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  IconPath = _fieldJson;
            }
            else
            {
                IconPath = "";
            }
        }
        
        { 
            var _fieldJson = _json["PropReferenceVaule"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } PropReferenceVaule = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.PropReference>(); foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { GameEditor.ConfigEditor.Model.PropReference __v0;  if(!__e0.IsObject) { throw new SerializationException(); }  __v0 = GameEditor.ConfigEditor.Model.PropReference.LoadJsonPropReference(__e0);  PropReferenceVaule.Add(__v0); }  
            }
            else
            {
                PropReferenceVaule = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.PropReference>();
            }
        }
        
        { 
            var _fieldJson = _json["ReferenceActionName"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  ReferenceActionName = _fieldJson;
            }
            else
            {
                ReferenceActionName = "";
            }
        }
        
        { 
            var _fieldJson = _json["SpecialEffect"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  SpecialEffect = _fieldJson;
            }
            else
            {
                SpecialEffect = "";
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["Id"] = new JSONNumber(Id);
        }

        if (Name != null)
        {
            _json["Name"] = new JSONString(Name);
        }

        if (Describe != null)
        {
            _json["Describe"] = new JSONString(Describe);
        }
        {
            _json["PropType"] = new JSONNumber((int)PropType);
        }

        if (IconPath != null)
        {
            _json["IconPath"] = new JSONString(IconPath);
        }

        if (PropReferenceVaule != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in PropReferenceVaule) { { var __bjson = new JSONObject();  GameEditor.ConfigEditor.Model.PropReference.SaveJsonPropReference(__e0, __bjson); __cjson0["null"] = __bjson; } } __cjson0.Inline = __cjson0.Count == 0; _json["PropReferenceVaule"] = __cjson0; }
        }

        if (ReferenceActionName != null)
        {
            _json["ReferenceActionName"] = new JSONString(ReferenceActionName);
        }

        if (SpecialEffect != null)
        {
            _json["SpecialEffect"] = new JSONString(SpecialEffect);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderPropConfig(PropConfig obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("Id", "Id"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("Id", "Id"), GUILayout.Width(100));
}
this.Id = UnityEditor.EditorGUILayout.IntField(this.Id, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("道具名", "Name"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("Name", "道具名"), GUILayout.Width(100));
}
this.Name = UnityEditor.EditorGUILayout.TextField(this.Name, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(this.Name,__x =>this.Name = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("描述", "Describe"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("Describe", "描述"), GUILayout.Width(100));
}
this.Describe = UnityEditor.EditorGUILayout.TextField(this.Describe, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(this.Describe,__x =>this.Describe = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("道具类型", "PropType"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("PropType", "道具类型"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.PropType;
        var __alias1 = (PropType_Alias)this.PropType;
        __alias1 = (PropType_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = PropType_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.PropType = (GameEditor.ConfigEditor.Model.PropType)__item1.Value;
    }
    else
    {
        this.PropType = (GameEditor.ConfigEditor.Model.PropType)UnityEditor.EditorGUILayout.EnumPopup(this.PropType, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("图标路径", "IconPath"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("IconPath", "图标路径"), GUILayout.Width(100));
}
this.IconPath = UnityEditor.EditorGUILayout.TextField(this.IconPath, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(this.IconPath,__x =>this.IconPath = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("影响数值", "PropReferenceVaule"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("PropReferenceVaule", "影响数值"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.PropReferenceVaule.Count;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            this.PropReferenceVaule.RemoveAt(__i1);
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        GameEditor.ConfigEditor.Model.PropReference __e1 = this.PropReferenceVaule[__i1];
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
__e1.PropId = UnityEditor.EditorGUILayout.IntField(__e1.PropId, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("减少数量", "ChangeNum"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("ChangeNum", "减少数量"), GUILayout.Width(100));
}
__e1.ChangeNum = UnityEditor.EditorGUILayout.IntField(__e1.ChangeNum, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
};
        this.PropReferenceVaule[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        GameEditor.ConfigEditor.Model.PropReference __e1;
        __e1 = new PropReference();;
        this.PropReferenceVaule.Add(__e1);
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            GameEditor.ConfigEditor.Model.PropReference __importElement1;
            if(!__importJson1.IsObject) { throw new SerializationException(); }  __importElement1 = GameEditor.ConfigEditor.Model.PropReference.LoadJsonPropReference(__importJson1);
            this.PropReferenceVaule.Add(__importElement1);
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("宠物使用后动作名", "ReferenceActionName"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("ReferenceActionName", "宠物使用后动作名"), GUILayout.Width(100));
}
this.ReferenceActionName = UnityEditor.EditorGUILayout.TextField(this.ReferenceActionName, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(this.ReferenceActionName,__x =>this.ReferenceActionName = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("特殊效果", "SpecialEffect"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("SpecialEffect", "特殊效果"), GUILayout.Width(100));
}
this.SpecialEffect = UnityEditor.EditorGUILayout.TextField(this.SpecialEffect, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(this.SpecialEffect,__x =>this.SpecialEffect = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static PropConfig LoadJsonPropConfig(SimpleJSON.JSONNode _json)
    {
        PropConfig obj = new PropConfig();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonPropConfig(PropConfig _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id;
    /// <summary>
    /// 道具名
    /// </summary>
    public string Name;
    /// <summary>
    /// 描述
    /// </summary>
    public string Describe;
    /// <summary>
    /// 道具类型
    /// </summary>
    public GameEditor.ConfigEditor.Model.PropType PropType;
    /// <summary>
    /// 图标路径
    /// </summary>
    public string IconPath;
    /// <summary>
    /// 影响数值
    /// </summary>
    public System.Collections.Generic.List<GameEditor.ConfigEditor.Model.PropReference> PropReferenceVaule;
    /// <summary>
    /// 宠物使用后动作名
    /// </summary>
    public string ReferenceActionName;
    /// <summary>
    /// 特殊效果
    /// </summary>
    public string SpecialEffect;

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Describe:" + Describe + ","
        + "PropType:" + PropType + ","
        + "IconPath:" + IconPath + ","
        + "PropReferenceVaule:" + Luban.StringUtil.CollectionToString(PropReferenceVaule) + ","
        + "ReferenceActionName:" + ReferenceActionName + ","
        + "SpecialEffect:" + SpecialEffect + ","
        + "}";
    }
}

}

