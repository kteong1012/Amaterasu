
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

public sealed class SkillDiceFaceConfig :  Luban.EditorBeanBase 
{
    public SkillDiceFaceConfig()
    {
            name = "";
            content = "";
            type = GameEditor.ConfigEditor.Model.SkillType.SkillActive;
            tag = System.Array.Empty<GameEditor.ConfigEditor.Model.SkillTag>();
            showTag = System.Array.Empty<string>();
            targetType = GameEditor.ConfigEditor.Model.Result.TargetType.Caster;
            conditions = System.Array.Empty<int>();
            results = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam>();
            diceWindow = System.Array.Empty<int>();
            buffWindow = System.Array.Empty<int>();
            icon = null;
            projectilePath = "";
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["id"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  id = _fieldJson;
            }
            else
            {
                id = 0;
            }
        }
        
        { 
            var _fieldJson = _json["name"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  name = _fieldJson;
            }
            else
            {
                name = "";
            }
        }
        
        { 
            var _fieldJson = _json["content"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  content = _fieldJson;
            }
            else
            {
                content = "";
            }
        }
        
        { 
            var _fieldJson = _json["type"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { type = (GameEditor.ConfigEditor.Model.SkillType)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.SkillType), _fieldJson); } else if(_fieldJson.IsNumber) { type = (GameEditor.ConfigEditor.Model.SkillType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                type = GameEditor.ConfigEditor.Model.SkillType.SkillActive;
            }
        }
        
        { 
            var _fieldJson = _json["tag"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int __n0 = _fieldJson.Count; tag = new GameEditor.ConfigEditor.Model.SkillTag[__n0]; int __i0=0; foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { GameEditor.ConfigEditor.Model.SkillTag __v0;  if(__e0.IsString) { __v0 = (GameEditor.ConfigEditor.Model.SkillTag)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.SkillTag), __e0); } else if(__e0.IsNumber) { __v0 = (GameEditor.ConfigEditor.Model.SkillTag)(int)__e0; } else { throw new SerializationException(); }    tag[__i0++] = __v0; }  
            }
            else
            {
                tag = System.Array.Empty<GameEditor.ConfigEditor.Model.SkillTag>();
            }
        }
        
        { 
            var _fieldJson = _json["show_tag"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int __n0 = _fieldJson.Count; showTag = new string[__n0]; int __i0=0; foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { string __v0;  if(!__e0.IsString) { throw new SerializationException(); }  __v0 = __e0;  showTag[__i0++] = __v0; }  
            }
            else
            {
                showTag = System.Array.Empty<string>();
            }
        }
        
        { 
            var _fieldJson = _json["show"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsBoolean) { throw new SerializationException(); }  show = _fieldJson;
            }
            else
            {
                show = false;
            }
        }
        
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
        
        { 
            var _fieldJson = _json["conditions"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int __n0 = _fieldJson.Count; conditions = new int[__n0]; int __i0=0; foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { int __v0;  if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0;  conditions[__i0++] = __v0; }  
            }
            else
            {
                conditions = System.Array.Empty<int>();
            }
        }
        
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
        
        { 
            var _fieldJson = _json["dice_window"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int __n0 = _fieldJson.Count; diceWindow = new int[__n0]; int __i0=0; foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { int __v0;  if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0;  diceWindow[__i0++] = __v0; }  
            }
            else
            {
                diceWindow = System.Array.Empty<int>();
            }
        }
        
        { 
            var _fieldJson = _json["buff_window"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int __n0 = _fieldJson.Count; buffWindow = new int[__n0]; int __i0=0; foreach(SimpleJSON.JSONNode __e0 in _fieldJson.Children) { int __v0;  if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0;  buffWindow[__i0++] = __v0; }  
            }
            else
            {
                buffWindow = System.Array.Empty<int>();
            }
        }
        
        { 
            var _fieldJson = _json["cause_battle"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsBoolean) { throw new SerializationException(); }  causeBattle = _fieldJson;
            }
            else
            {
                causeBattle = false;
            }
        }
        
        { 
            var _fieldJson = _json["trigged"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsBoolean) { throw new SerializationException(); }  trigged = _fieldJson;
            }
            else
            {
                trigged = false;
            }
        }
        
        { 
            var _fieldJson = _json["icon"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) {throw new SerializationException(); } var icon_Json = _fieldJson;
                icon = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Sprite>(icon_Json);
            }
            else
            {
                icon = null;
            }
        }
        
        { 
            var _fieldJson = _json["projectile_path"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  projectilePath = _fieldJson;
            }
            else
            {
                projectilePath = "";
            }
        }
        
        { 
            var _fieldJson = _json["projectile_duration"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  projectileDuration = _fieldJson;
            }
            else
            {
                projectileDuration = 0;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["id"] = new JSONNumber(id);
        }

        if (name != null)
        {
            _json["name"] = new JSONString(name);
        }

        if (content != null)
        {
            _json["content"] = new JSONString(content);
        }
        {
            _json["type"] = new JSONNumber((int)type);
        }

        if (tag != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in tag) { __cjson0["null"] = new JSONNumber((int)__e0); } __cjson0.Inline = __cjson0.Count == 0; _json["tag"] = __cjson0; }
        }

        if (showTag != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in showTag) { __cjson0["null"] = new JSONString(__e0); } __cjson0.Inline = __cjson0.Count == 0; _json["show_tag"] = __cjson0; }
        }
        {
            _json["show"] = new JSONBool(show);
        }
        {
            _json["target_type"] = new JSONNumber((int)targetType);
        }

        if (conditions != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in conditions) { __cjson0["null"] = new JSONNumber(__e0); } __cjson0.Inline = __cjson0.Count == 0; _json["conditions"] = __cjson0; }
        }

        if (results != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in results) { { var __bjson = new JSONObject();  GameEditor.ConfigEditor.Model.Result.ResultParam.SaveJsonResultParam(__e0, __bjson); __cjson0["null"] = __bjson; } } __cjson0.Inline = __cjson0.Count == 0; _json["results"] = __cjson0; }
        }

        if (diceWindow != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in diceWindow) { __cjson0["null"] = new JSONNumber(__e0); } __cjson0.Inline = __cjson0.Count == 0; _json["dice_window"] = __cjson0; }
        }

        if (buffWindow != null)
        {
            { var __cjson0 = new JSONArray(); foreach(var __e0 in buffWindow) { __cjson0["null"] = new JSONNumber(__e0); } __cjson0.Inline = __cjson0.Count == 0; _json["buff_window"] = __cjson0; }
        }
        {
            _json["cause_battle"] = new JSONBool(causeBattle);
        }
        {
            _json["trigged"] = new JSONBool(trigged);
        }

        if (icon != null)
        {
            var icon_Path = icon == null ? "" : UnityEditor.AssetDatabase.GetAssetPath(icon);
            _json["icon"] = new JSONString(icon_Path);
        }

        if (projectilePath != null)
        {
            _json["projectile_path"] = new JSONString(projectilePath);
        }
        {
            _json["projectile_duration"] = new JSONNumber(projectileDuration);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderSkillDiceFaceConfig(SkillDiceFaceConfig obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("id", "id"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("id", "id"), GUILayout.Width(100));
}
this.id = UnityEditor.EditorGUILayout.IntField(this.id, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("名称", "name"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("name", "名称"), GUILayout.Width(100));
}
this.name = UnityEditor.EditorGUILayout.TextField(this.name, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(this.name,__x =>this.name = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("描述", "content"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("content", "描述"), GUILayout.Width(100));
}
this.content = UnityEditor.EditorGUILayout.TextField(this.content, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(this.content,__x =>this.content = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("类型", "type"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("type", "类型"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.type;
        var __alias1 = (SkillType_Alias)this.type;
        __alias1 = (SkillType_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = SkillType_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.type = (GameEditor.ConfigEditor.Model.SkillType)__item1.Value;
    }
    else
    {
        this.type = (GameEditor.ConfigEditor.Model.SkillType)UnityEditor.EditorGUILayout.EnumPopup(this.type, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("技能Tag", "tag"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("tag", "技能Tag"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.tag.Length;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            var __list1 = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.SkillTag>(this.tag);
            __list1.RemoveAt(__i1);
            this.tag = __list1.ToArray();
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        GameEditor.ConfigEditor.Model.SkillTag __e1 = this.tag[__i1];
        {
    if (ConfigEditorSettings.showComment)
    {
        var __index2 = (int)__e1;
        var __alias2 = (SkillTag_Alias)__e1;
        __alias2 = (SkillTag_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias2, GUILayout.Width(150));
        var __item2 = SkillTag_Metadata.GetByNameOrAlias(__alias2.ToString());
        __e1 = (GameEditor.ConfigEditor.Model.SkillTag)__item2.Value;
    }
    else
    {
        __e1 = (GameEditor.ConfigEditor.Model.SkillTag)UnityEditor.EditorGUILayout.EnumPopup(__e1, GUILayout.Width(150));
    }
};
        this.tag[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        var __list1 = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.SkillTag>(this.tag);
        GameEditor.ConfigEditor.Model.SkillTag __e1;
        __e1 = GameEditor.ConfigEditor.Model.SkillTag.Attack;;
        __list1.Add(__e1);
        this.tag = __list1.ToArray();
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            GameEditor.ConfigEditor.Model.SkillTag __importElement1;
            if(__importJson1.IsString) { __importElement1 = (GameEditor.ConfigEditor.Model.SkillTag)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.SkillTag), __importJson1); } else if(__importJson1.IsNumber) { __importElement1 = (GameEditor.ConfigEditor.Model.SkillTag)(int)__importJson1; } else { throw new SerializationException(); }  
            var __list1 = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.SkillTag>(this.tag);
            __list1.Add(__importElement1);
            this.tag = __list1.ToArray();
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("显示Tag", "show_tag"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("show_tag", "显示Tag"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.showTag.Length;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            var __list1 = new System.Collections.Generic.List<string>(this.showTag);
            __list1.RemoveAt(__i1);
            this.showTag = __list1.ToArray();
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        string __e1 = this.showTag[__i1];
        __e1 = UnityEditor.EditorGUILayout.TextField(__e1, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(__e1,__x =>__e1 = __x);
};
        this.showTag[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        var __list1 = new System.Collections.Generic.List<string>(this.showTag);
        string __e1;
        __e1 = "";;
        __list1.Add(__e1);
        this.showTag = __list1.ToArray();
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            string __importElement1;
            if(!__importJson1.IsString) { throw new SerializationException(); }  __importElement1 = __importJson1;
            var __list1 = new System.Collections.Generic.List<string>(this.showTag);
            __list1.Add(__importElement1);
            this.showTag = __list1.ToArray();
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("是否显示", "show"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("show", "是否显示"), GUILayout.Width(100));
}
this.show = UnityEditor.EditorGUILayout.Toggle(this.show, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
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
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("释放条件", "conditions"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("conditions", "释放条件"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.conditions.Length;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            var __list1 = new System.Collections.Generic.List<int>(this.conditions);
            __list1.RemoveAt(__i1);
            this.conditions = __list1.ToArray();
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        int __e1 = this.conditions[__i1];
        __e1 = UnityEditor.EditorGUILayout.IntField(__e1, GUILayout.Width(150));;
        this.conditions[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        var __list1 = new System.Collections.Generic.List<int>(this.conditions);
        int __e1;
        __e1 = 0;;
        __list1.Add(__e1);
        this.conditions = __list1.ToArray();
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            int __importElement1;
            if(!__importJson1.IsNumber) { throw new SerializationException(); }  __importElement1 = __importJson1;
            var __list1 = new System.Collections.Generic.List<int>(this.conditions);
            __list1.Add(__importElement1);
            this.conditions = __list1.ToArray();
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("Result参数", "results"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("results", "Result参数"), GUILayout.Width(100));
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
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("骰子弹窗", "dice_window"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("dice_window", "骰子弹窗"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.diceWindow.Length;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            var __list1 = new System.Collections.Generic.List<int>(this.diceWindow);
            __list1.RemoveAt(__i1);
            this.diceWindow = __list1.ToArray();
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        int __e1 = this.diceWindow[__i1];
        __e1 = UnityEditor.EditorGUILayout.IntField(__e1, GUILayout.Width(150));;
        this.diceWindow[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        var __list1 = new System.Collections.Generic.List<int>(this.diceWindow);
        int __e1;
        __e1 = 0;;
        __list1.Add(__e1);
        this.diceWindow = __list1.ToArray();
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            int __importElement1;
            if(!__importJson1.IsNumber) { throw new SerializationException(); }  __importElement1 = __importJson1;
            var __list1 = new System.Collections.Generic.List<int>(this.diceWindow);
            __list1.Add(__importElement1);
            this.diceWindow = __list1.ToArray();
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("buff弹窗", "buff_window"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("buff_window", "buff弹窗"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = this.buffWindow.Length;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            var __list1 = new System.Collections.Generic.List<int>(this.buffWindow);
            __list1.RemoveAt(__i1);
            this.buffWindow = __list1.ToArray();
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        int __e1 = this.buffWindow[__i1];
        __e1 = UnityEditor.EditorGUILayout.IntField(__e1, GUILayout.Width(150));;
        this.buffWindow[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        var __list1 = new System.Collections.Generic.List<int>(this.buffWindow);
        int __e1;
        __e1 = 0;;
        __list1.Add(__e1);
        this.buffWindow = __list1.ToArray();
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            int __importElement1;
            if(!__importJson1.IsNumber) { throw new SerializationException(); }  __importElement1 = __importJson1;
            var __list1 = new System.Collections.Generic.List<int>(this.buffWindow);
            __list1.Add(__importElement1);
            this.buffWindow = __list1.ToArray();
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("是否会引起战斗", "cause_battle"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("cause_battle", "是否会引起战斗"), GUILayout.Width(100));
}
this.causeBattle = UnityEditor.EditorGUILayout.Toggle(this.causeBattle, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("是否能被监听", "trigged"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("trigged", "是否能被监听"), GUILayout.Width(100));
}
this.trigged = UnityEditor.EditorGUILayout.Toggle(this.trigged, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("图像", "icon"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("icon", "图像"), GUILayout.Width(100));
}
this.icon = UnityEditor.EditorGUILayout.ObjectField(this.icon, typeof(UnityEngine.Sprite), false, GUILayout.Width(150)) as UnityEngine.Sprite;if (this.icon != null)
{
    UnityEngine.GUILayout.Label(((UnityEngine.Sprite)this.icon).texture, GUILayout.Height(50));
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("投射物特效", "projectile_path"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("projectile_path", "投射物特效"), GUILayout.Width(100));
}
this.projectilePath = UnityEditor.EditorGUILayout.TextField(this.projectilePath, GUILayout.Width(150));
if (GUILayout.Button("…", GUILayout.Width(20)))
{
    TextInputWindow.GetTextAsync(this.projectilePath,__x =>this.projectilePath = __x);
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("投射物飞行时间", "projectile_duration"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("projectile_duration", "投射物飞行时间"), GUILayout.Width(100));
}
this.projectileDuration = UnityEditor.EditorGUILayout.DoubleField(this.projectileDuration, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static SkillDiceFaceConfig LoadJsonSkillDiceFaceConfig(SimpleJSON.JSONNode _json)
    {
        SkillDiceFaceConfig obj = new SkillDiceFaceConfig();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonSkillDiceFaceConfig(SkillDiceFaceConfig _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// id
    /// </summary>
    public int id;
    /// <summary>
    /// 名称
    /// </summary>
    public string name;
    /// <summary>
    /// 描述
    /// </summary>
    public string content;
    /// <summary>
    /// 类型
    /// </summary>
    public GameEditor.ConfigEditor.Model.SkillType type;
    /// <summary>
    /// 技能Tag
    /// </summary>
    public GameEditor.ConfigEditor.Model.SkillTag[] tag;
    /// <summary>
    /// 显示Tag
    /// </summary>
    public string[] showTag;
    /// <summary>
    /// 是否显示
    /// </summary>
    public bool show;
    /// <summary>
    /// 目标类型
    /// </summary>
    public GameEditor.ConfigEditor.Model.Result.TargetType targetType;
    /// <summary>
    /// 释放条件
    /// </summary>
    public int[] conditions;
    /// <summary>
    /// Result参数
    /// </summary>
    public System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam> results;
    /// <summary>
    /// 骰子弹窗
    /// </summary>
    public int[] diceWindow;
    /// <summary>
    /// buff弹窗
    /// </summary>
    public int[] buffWindow;
    /// <summary>
    /// 是否会引起战斗
    /// </summary>
    public bool causeBattle;
    /// <summary>
    /// 是否能被监听
    /// </summary>
    public bool trigged;
    /// <summary>
    /// 图像
    /// </summary>
    public UnityEngine.Object icon;
    /// <summary>
    /// 投射物特效
    /// </summary>
    public string projectilePath;
    /// <summary>
    /// 投射物飞行时间
    /// </summary>
    public double projectileDuration;

    public override string ToString()
    {
        return "{ "
        + "id:" + id + ","
        + "name:" + name + ","
        + "content:" + content + ","
        + "type:" + type + ","
        + "tag:" + Luban.StringUtil.CollectionToString(tag) + ","
        + "showTag:" + Luban.StringUtil.CollectionToString(showTag) + ","
        + "show:" + show + ","
        + "targetType:" + targetType + ","
        + "conditions:" + Luban.StringUtil.CollectionToString(conditions) + ","
        + "results:" + Luban.StringUtil.CollectionToString(results) + ","
        + "diceWindow:" + Luban.StringUtil.CollectionToString(diceWindow) + ","
        + "buffWindow:" + Luban.StringUtil.CollectionToString(buffWindow) + ","
        + "causeBattle:" + causeBattle + ","
        + "trigged:" + trigged + ","
        + "icon:" + icon + ","
        + "projectilePath:" + projectilePath + ","
        + "projectileDuration:" + projectileDuration + ","
        + "}";
    }
}

}
