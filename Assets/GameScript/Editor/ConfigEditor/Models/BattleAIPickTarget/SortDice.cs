
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

namespace GameEditor.ConfigEditor.Model.BattleAIPickTarget
{

public sealed class SortDice :  BattleAIPickTarget.PickTargetParam 
{
    public SortDice()
    {
            camp = GameEditor.ConfigEditor.Model.BattleAIPickTarget.PickTargetCamp.Enemy;
            diceType = GameEditor.ConfigEditor.Model.DicePoolType.Extra;
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "SortDice";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["camp"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { camp = (GameEditor.ConfigEditor.Model.BattleAIPickTarget.PickTargetCamp)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.BattleAIPickTarget.PickTargetCamp), _fieldJson); } else if(_fieldJson.IsNumber) { camp = (GameEditor.ConfigEditor.Model.BattleAIPickTarget.PickTargetCamp)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                camp = GameEditor.ConfigEditor.Model.BattleAIPickTarget.PickTargetCamp.Enemy;
            }
        }
        
        { 
            var _fieldJson = _json["dice_type"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { diceType = (GameEditor.ConfigEditor.Model.DicePoolType)System.Enum.Parse(typeof(GameEditor.ConfigEditor.Model.DicePoolType), _fieldJson); } else if(_fieldJson.IsNumber) { diceType = (GameEditor.ConfigEditor.Model.DicePoolType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
            else
            {
                diceType = GameEditor.ConfigEditor.Model.DicePoolType.Extra;
            }
        }
        
        { 
            var _fieldJson = _json["is_desc"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsBoolean) { throw new SerializationException(); }  isDesc = _fieldJson;
            }
            else
            {
                isDesc = false;
            }
        }
        
        { 
            var _fieldJson = _json["num"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  num = _fieldJson;
            }
            else
            {
                num = 0;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["camp"] = new JSONNumber((int)camp);
        }
        {
            _json["dice_type"] = new JSONNumber((int)diceType);
        }
        {
            _json["is_desc"] = new JSONBool(isDesc);
        }
        {
            _json["num"] = new JSONNumber(num);
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderSortDice(SortDice obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("阵营", "camp"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("camp", "阵营"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.camp;
        var __alias1 = (BattleAIPickTarget.PickTargetCamp_Alias)this.camp;
        __alias1 = (BattleAIPickTarget.PickTargetCamp_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = BattleAIPickTarget.PickTargetCamp_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.camp = (GameEditor.ConfigEditor.Model.BattleAIPickTarget.PickTargetCamp)__item1.Value;
    }
    else
    {
        this.camp = (GameEditor.ConfigEditor.Model.BattleAIPickTarget.PickTargetCamp)UnityEditor.EditorGUILayout.EnumPopup(this.camp, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("骰子类型", "dice_type"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("dice_type", "骰子类型"), GUILayout.Width(100));
}
{
    if (ConfigEditorSettings.showComment)
    {
        var __index1 = (int)this.diceType;
        var __alias1 = (DicePoolType_Alias)this.diceType;
        __alias1 = (DicePoolType_Alias)UnityEditor.EditorGUILayout.EnumPopup(__alias1, GUILayout.Width(150));
        var __item1 = DicePoolType_Metadata.GetByNameOrAlias(__alias1.ToString());
        this.diceType = (GameEditor.ConfigEditor.Model.DicePoolType)__item1.Value;
    }
    else
    {
        this.diceType = (GameEditor.ConfigEditor.Model.DicePoolType)UnityEditor.EditorGUILayout.EnumPopup(this.diceType, GUILayout.Width(150));
    }
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("是否降序", "is_desc"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("is_desc", "是否降序"), GUILayout.Width(100));
}
this.isDesc = UnityEditor.EditorGUILayout.Toggle(this.isDesc, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("第几个", "num"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("num", "第几个"), GUILayout.Width(100));
}
this.num = UnityEditor.EditorGUILayout.IntField(this.num, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static SortDice LoadJsonSortDice(SimpleJSON.JSONNode _json)
    {
        SortDice obj = new BattleAIPickTarget.SortDice();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonSortDice(SortDice _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 阵营
    /// </summary>
    public GameEditor.ConfigEditor.Model.BattleAIPickTarget.PickTargetCamp camp;
    /// <summary>
    /// 骰子类型
    /// </summary>
    public GameEditor.ConfigEditor.Model.DicePoolType diceType;
    /// <summary>
    /// 是否降序
    /// </summary>
    public bool isDesc;
    /// <summary>
    /// 第几个
    /// </summary>
    public int num;

    public override string ToString()
    {
        return "{ "
        + "camp:" + camp + ","
        + "diceType:" + diceType + ","
        + "isDesc:" + isDesc + ","
        + "num:" + num + ","
        + "}";
    }
}

}
