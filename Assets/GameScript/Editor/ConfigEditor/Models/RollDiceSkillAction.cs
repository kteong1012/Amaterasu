
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

public abstract class RollDiceSkillAction :  SkillAction 
{
    public RollDiceSkillAction()
    {
            atkPools = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.DicePoolType>();
            defPools = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.DicePoolType>();
            addDices = new System.Collections.Generic.List<int>();
            winResults = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam>();
            loseResults = new System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam>();
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "RollDiceSkillAction";

    private int _typeIndex = -1;
    private int TypeIndex => _typeIndex;
    private static string[] Types = new string[]
    {
        "StrikeSkillAction",
        "JudgeSkillAction",
    };
    private static string[] TypeAlias = new string[]
    {
        "打击",
        "判定",
    };

    public new static RollDiceSkillAction Create(string type)
    {
        switch (type)
        {
            case "StrikeSkillAction":
            {
                var obj = new StrikeSkillAction();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "JudgeSkillAction":
            {
                var obj = new JudgeSkillAction();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            default: return null;
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderRollDiceSkillAction(ref RollDiceSkillAction obj)
    {
        UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
        var array = ConfigEditorSettings.showComment ? TypeAlias : Types;
        UnityEditor.EditorGUILayout.BeginHorizontal();
        UnityEditor.EditorGUILayout.LabelField("类型", GUILayout.Width(100));
        var index = UnityEditor.EditorGUILayout.Popup(obj.TypeIndex, array, GUILayout.Width(200));
        if (obj.TypeIndex != index)
        {
            obj = Create(Types[index]);
        }
        UnityEditor.EditorGUILayout.EndHorizontal();
        obj?.Render();
        UnityEditor.EditorGUILayout.EndVertical();
    }

    public override void Render()
    {
    }
    public static RollDiceSkillAction LoadJsonRollDiceSkillAction(SimpleJSON.JSONNode _json)
    {
        string type = _json["$type"];
        RollDiceSkillAction obj;
        switch (type)
        {
            case "StrikeSkillAction":
            {
                obj = new StrikeSkillAction(); 
                obj._typeIndex = Array.IndexOf(Types, "StrikeSkillAction");
                break;
            }
            case "JudgeSkillAction":
            {
                obj = new JudgeSkillAction(); 
                obj._typeIndex = Array.IndexOf(Types, "JudgeSkillAction");
                break;
            }
            default: throw new SerializationException();
        }
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonRollDiceSkillAction(RollDiceSkillAction _obj, SimpleJSON.JSONNode _json)
    {
        _json["$type"] = _obj.GetTypeStr();
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 进攻池
    /// </summary>
    public System.Collections.Generic.List<GameEditor.ConfigEditor.Model.DicePoolType> atkPools;
    /// <summary>
    /// 防守池
    /// </summary>
    public System.Collections.Generic.List<GameEditor.ConfigEditor.Model.DicePoolType> defPools;
    /// <summary>
    /// 额外骰子
    /// </summary>
    public System.Collections.Generic.List<int> addDices;
    /// <summary>
    /// 胜利结果
    /// </summary>
    public System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam> winResults;
    /// <summary>
    /// 失败结果
    /// </summary>
    public System.Collections.Generic.List<GameEditor.ConfigEditor.Model.Result.ResultParam> loseResults;

    public override string ToString()
    {
        return "{ "
        + "atkPools:" + Luban.StringUtil.CollectionToString(atkPools) + ","
        + "defPools:" + Luban.StringUtil.CollectionToString(defPools) + ","
        + "addDices:" + Luban.StringUtil.CollectionToString(addDices) + ","
        + "winResults:" + Luban.StringUtil.CollectionToString(winResults) + ","
        + "loseResults:" + Luban.StringUtil.CollectionToString(loseResults) + ","
        + "}";
    }
}

}

