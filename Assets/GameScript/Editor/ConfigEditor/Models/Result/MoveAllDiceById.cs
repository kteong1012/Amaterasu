
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

public sealed class MoveAllDiceById :  Result.DataResult 
{
    public MoveAllDiceById()
    {
            targetType = GameEditor.ConfigEditor.Model.Result.TargetType.Caster;
            srcDicePoolType = GameEditor.ConfigEditor.Model.DicePoolType.Extra;
            dstDicePoolType = GameEditor.ConfigEditor.Model.DicePoolType.Extra;
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "MoveAllDiceById";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
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
            var _fieldJson = _json["src_dice_pool_type"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { srcDicePoolType = (GameEditor.ConfigEditor.Model.DicePoolType)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.DicePoolType), _fieldJson); } else if(_fieldJson.IsNumber) { srcDicePoolType = (GameEditor.ConfigEditor.Model.DicePoolType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                srcDicePoolType = GameEditor.ConfigEditor.Model.DicePoolType.Extra;
            }
        }
        
        { 
            var _fieldJson = _json["dst_dice_pool_type"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { dstDicePoolType = (GameEditor.ConfigEditor.Model.DicePoolType)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.DicePoolType), _fieldJson); } else if(_fieldJson.IsNumber) { dstDicePoolType = (GameEditor.ConfigEditor.Model.DicePoolType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                dstDicePoolType = GameEditor.ConfigEditor.Model.DicePoolType.Extra;
            }
        }
        
        { 
            var _fieldJson = _json["dice_id"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  diceId = _fieldJson;
            }
            else
            {
                diceId = 0;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["target_type"] = new JSONNumber((int)targetType);
        }
        {
            _json["src_dice_pool_type"] = new JSONNumber((int)srcDicePoolType);
        }
        {
            _json["dst_dice_pool_type"] = new JSONNumber((int)dstDicePoolType);
        }
        {
            _json["dice_id"] = new JSONNumber(diceId);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderMoveAllDiceById(MoveAllDiceById obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
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
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("源骰子池类型", "src_dice_pool_type"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("src_dice_pool_type", "源骰子池类型"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.srcDicePoolType;
        var __alias1 = (DicePoolType_Alias)this.srcDicePoolType;
        __alias1 = (DicePoolType_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = DicePoolType_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.srcDicePoolType = (GameEditor.ConfigEditor.Model.DicePoolType)__item1.Value;
    }
    else
    {
        this.srcDicePoolType = (GameEditor.ConfigEditor.Model.DicePoolType)UnityEditor.EditorGUILayout.EnumPopup(this.srcDicePoolType, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("目标骰子池类型", "dst_dice_pool_type"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("dst_dice_pool_type", "目标骰子池类型"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.dstDicePoolType;
        var __alias1 = (DicePoolType_Alias)this.dstDicePoolType;
        __alias1 = (DicePoolType_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = DicePoolType_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.dstDicePoolType = (GameEditor.ConfigEditor.Model.DicePoolType)__item1.Value;
    }
    else
    {
        this.dstDicePoolType = (GameEditor.ConfigEditor.Model.DicePoolType)UnityEditor.EditorGUILayout.EnumPopup(this.dstDicePoolType, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("骰子id", "dice_id"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("dice_id", "骰子id"), GUILayout.Width(100));
}
this.diceId = UnityEditor.EditorGUILayout.IntField(this.diceId, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static MoveAllDiceById LoadJsonMoveAllDiceById(SimpleJSON.JSONNode _json)
    {
        MoveAllDiceById obj = new Result.MoveAllDiceById();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonMoveAllDiceById(MoveAllDiceById _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 目标类型
    /// </summary>
    public GameEditor.ConfigEditor.Model.Result.TargetType targetType;
    /// <summary>
    /// 源骰子池类型
    /// </summary>
    public GameEditor.ConfigEditor.Model.DicePoolType srcDicePoolType;
    /// <summary>
    /// 目标骰子池类型
    /// </summary>
    public GameEditor.ConfigEditor.Model.DicePoolType dstDicePoolType;
    /// <summary>
    /// 骰子id
    /// </summary>
    public int diceId;

    public override string ToString()
    {
        return "{ "
        + "targetType:" + targetType + ","
        + "srcDicePoolType:" + srcDicePoolType + ","
        + "dstDicePoolType:" + dstDicePoolType + ","
        + "diceId:" + diceId + ","
        + "}";
    }
}

}
