
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

public sealed class ItemFitRule :  Luban.EditorBeanBase 
{
    public ItemFitRule()
    {
            Position = GameEditor.ConfigEditor.Model.DicePoolType.Extra;
            Require = GameEditor.ConfigEditor.Model.ItemFitRequire.Greater;
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["Position"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { Position = (GameEditor.ConfigEditor.Model.DicePoolType)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.DicePoolType), _fieldJson); } else if(_fieldJson.IsNumber) { Position = (GameEditor.ConfigEditor.Model.DicePoolType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                Position = GameEditor.ConfigEditor.Model.DicePoolType.Extra;
            }
        }
        
        { 
            var _fieldJson = _json["Require"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { Require = (GameEditor.ConfigEditor.Model.ItemFitRequire)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.ItemFitRequire), _fieldJson); } else if(_fieldJson.IsNumber) { Require = (GameEditor.ConfigEditor.Model.ItemFitRequire)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                Require = GameEditor.ConfigEditor.Model.ItemFitRequire.Greater;
            }
        }
        
        { 
            var _fieldJson = _json["Value"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Value = _fieldJson;
            }
            else
            {
                Value = 0;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["Position"] = new JSONNumber((int)Position);
        }
        {
            _json["Require"] = new JSONNumber((int)Require);
        }
        {
            _json["Value"] = new JSONNumber(Value);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderItemFitRule(ItemFitRule obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("要求位置", "Position"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("Position", "要求位置"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.Position;
        var __alias1 = (DicePoolType_Alias)this.Position;
        __alias1 = (DicePoolType_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = DicePoolType_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.Position = (GameEditor.ConfigEditor.Model.DicePoolType)__item1.Value;
    }
    else
    {
        this.Position = (GameEditor.ConfigEditor.Model.DicePoolType)UnityEditor.EditorGUILayout.EnumPopup(this.Position, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("条件", "Require"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("Require", "条件"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.Require;
        var __alias1 = (ItemFitRequire_Alias)this.Require;
        __alias1 = (ItemFitRequire_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = ItemFitRequire_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.Require = (GameEditor.ConfigEditor.Model.ItemFitRequire)__item1.Value;
    }
    else
    {
        this.Require = (GameEditor.ConfigEditor.Model.ItemFitRequire)UnityEditor.EditorGUILayout.EnumPopup(this.Require, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("值", "Value"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("Value", "值"), GUILayout.Width(100));
}
this.Value = UnityEditor.EditorGUILayout.IntField(this.Value, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static ItemFitRule LoadJsonItemFitRule(SimpleJSON.JSONNode _json)
    {
        ItemFitRule obj = new ItemFitRule();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonItemFitRule(ItemFitRule _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 要求位置
    /// </summary>
    public GameEditor.ConfigEditor.Model.DicePoolType Position;
    /// <summary>
    /// 条件
    /// </summary>
    public GameEditor.ConfigEditor.Model.ItemFitRequire Require;
    /// <summary>
    /// 值
    /// </summary>
    public int Value;

    public override string ToString()
    {
        return "{ "
        + "Position:" + Position + ","
        + "Require:" + Require + ","
        + "Value:" + Value + ","
        + "}";
    }
}

}
