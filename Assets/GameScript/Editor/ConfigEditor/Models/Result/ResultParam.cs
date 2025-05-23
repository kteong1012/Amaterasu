
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

public abstract class ResultParam :  Luban.EditorBeanBase 
{
    public ResultParam()
    {
    }
    public abstract string GetTypeStr();

    private int _typeIndex = -1;
    private int TypeIndex => _typeIndex;
    private static string[] Types = new string[]
    {
        "DifferDmg",
        "FixedDmg",
        "DifferVamp",
        "FixedVamp",
        "CharaAttrDmg",
        "DicePoolDmg",
        "BuffStackDmg",
        "ItemTagCountDmg",
        "FixedRecover",
        "CharaAttrRecover",
        "GainExtraMana",
        "CharaAttrGainExtraMana",
        "RemoveMana",
        "AddDice",
        "AddDiceInSamePool",
        "RemoveDiceById",
        "RemoveDiceByIdInSamePool",
        "RemoveAllDiceById",
        "RemoveDiceByTag",
        "RemoveAllDiceByTag",
        "MoveDiceById",
        "MoveAllDiceById",
        "MoveDiceByTag",
        "MoveAllDiceByTag",
        "PreRollDice",
        "SelfClone",
        "SelfAddDice",
        "DmgByDiceFace",
        "SelfRemove",
        "SelfMove",
        "SelfValueChange",
        "AddBuffToDiceByTag",
        "ChangeDiceValueByTag",
        "AddDecidedToDiceByTag",
        "AddBuff",
        "RemoveBuff",
        "Vulnerability",
        "IncreaseDamage",
        "ExtraDefend",
        "ChangeCounter",
        "AssignCounter",
        "WinTheGame",
        "GoToNextFloor",
        "SetRoomBattleState",
        "ChangeNumeric",
        "DamageJumpTextTemp",
        "TargetDmg",
        "TargetHeal",
        "ShootDmg",
        "ScreenDmg",
        "AoeDmg",
        "TargetEffect",
        "ShootEffect",
        "ScreenEffect",
        "AoeEffect",
    };
    private static string[] TypeAlias = new string[]
    {
        "差值伤害",
        "定值伤害",
        "差值吸血",
        "定值吸血",
        "属性伤害",
        "骰子池伤害",
        "Buff层数伤害",
        "物品Tag数量伤害",
        "定值恢复",
        "属性恢复",
        "获得额外行动点",
        "属性获得额外行动点",
        "移除目标行动点",
        "id获得骰子",
        "id获得骰子进同位置",
        "id移除骰子",
        "id移除同位置指定id骰子",
        "id移除所有骰子",
        "tag移除骰子",
        "tag移除所有骰子",
        "id移动骰子",
        "id移动所有骰子",
        "tag移动骰子",
        "tag移动所有骰子",
        "tag预投掷骰子",
        "自我克隆",
        "自我加骰子",
        "骰面伤害",
        "自我移除",
        "自我移动",
        "自我数值改动",
        "Tag选择骰子附加Buff",
        "Tag选择骰子数值改动",
        "Tag选择骰子与骰面附加命定",
        "附加Buff",
        "移除Buff",
        "易伤",
        "增伤",
        "额外参与防守投掷",
        "修改计数器",
        "赋值计数器",
        "游戏胜利",
        "进入下一楼",
        "改变房间战斗状态",
        "修改属性值",
        "伤害跳字",
        "表现_目标伤害",
        "表现_目标治疗",
        "表现_发射伤害",
        "表现_全屏伤害",
        "表现_AOE伤害",
        "表现_目标特效",
        "表现_发射特效",
        "表现_全屏特效",
        "表现_AOE特效",
    };

    public static ResultParam Create(string type)
    {
        switch (type)
        {
            case "Result.DifferDmg":   
            case "DifferDmg":
            {
                var obj = new Result.DifferDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.FixedDmg":   
            case "FixedDmg":
            {
                var obj = new Result.FixedDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.DifferVamp":   
            case "DifferVamp":
            {
                var obj = new Result.DifferVamp();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.FixedVamp":   
            case "FixedVamp":
            {
                var obj = new Result.FixedVamp();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.CharaAttrDmg":   
            case "CharaAttrDmg":
            {
                var obj = new Result.CharaAttrDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.DicePoolDmg":   
            case "DicePoolDmg":
            {
                var obj = new Result.DicePoolDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.BuffStackDmg":   
            case "BuffStackDmg":
            {
                var obj = new Result.BuffStackDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ItemTagCountDmg":   
            case "ItemTagCountDmg":
            {
                var obj = new Result.ItemTagCountDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.FixedRecover":   
            case "FixedRecover":
            {
                var obj = new Result.FixedRecover();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.CharaAttrRecover":   
            case "CharaAttrRecover":
            {
                var obj = new Result.CharaAttrRecover();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.GainExtraMana":   
            case "GainExtraMana":
            {
                var obj = new Result.GainExtraMana();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.CharaAttrGainExtraMana":   
            case "CharaAttrGainExtraMana":
            {
                var obj = new Result.CharaAttrGainExtraMana();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.RemoveMana":   
            case "RemoveMana":
            {
                var obj = new Result.RemoveMana();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.AddDice":   
            case "AddDice":
            {
                var obj = new Result.AddDice();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.AddDiceInSamePool":   
            case "AddDiceInSamePool":
            {
                var obj = new Result.AddDiceInSamePool();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.RemoveDiceById":   
            case "RemoveDiceById":
            {
                var obj = new Result.RemoveDiceById();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.RemoveDiceByIdInSamePool":   
            case "RemoveDiceByIdInSamePool":
            {
                var obj = new Result.RemoveDiceByIdInSamePool();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.RemoveAllDiceById":   
            case "RemoveAllDiceById":
            {
                var obj = new Result.RemoveAllDiceById();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.RemoveDiceByTag":   
            case "RemoveDiceByTag":
            {
                var obj = new Result.RemoveDiceByTag();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.RemoveAllDiceByTag":   
            case "RemoveAllDiceByTag":
            {
                var obj = new Result.RemoveAllDiceByTag();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.MoveDiceById":   
            case "MoveDiceById":
            {
                var obj = new Result.MoveDiceById();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.MoveAllDiceById":   
            case "MoveAllDiceById":
            {
                var obj = new Result.MoveAllDiceById();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.MoveDiceByTag":   
            case "MoveDiceByTag":
            {
                var obj = new Result.MoveDiceByTag();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.MoveAllDiceByTag":   
            case "MoveAllDiceByTag":
            {
                var obj = new Result.MoveAllDiceByTag();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.PreRollDice":   
            case "PreRollDice":
            {
                var obj = new Result.PreRollDice();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.SelfClone":   
            case "SelfClone":
            {
                var obj = new Result.SelfClone();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.SelfAddDice":   
            case "SelfAddDice":
            {
                var obj = new Result.SelfAddDice();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.DmgByDiceFace":   
            case "DmgByDiceFace":
            {
                var obj = new Result.DmgByDiceFace();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.SelfRemove":   
            case "SelfRemove":
            {
                var obj = new Result.SelfRemove();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.SelfMove":   
            case "SelfMove":
            {
                var obj = new Result.SelfMove();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.SelfValueChange":   
            case "SelfValueChange":
            {
                var obj = new Result.SelfValueChange();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.AddBuffToDiceByTag":   
            case "AddBuffToDiceByTag":
            {
                var obj = new Result.AddBuffToDiceByTag();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ChangeDiceValueByTag":   
            case "ChangeDiceValueByTag":
            {
                var obj = new Result.ChangeDiceValueByTag();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.AddDecidedToDiceByTag":   
            case "AddDecidedToDiceByTag":
            {
                var obj = new Result.AddDecidedToDiceByTag();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.AddBuff":   
            case "AddBuff":
            {
                var obj = new Result.AddBuff();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.RemoveBuff":   
            case "RemoveBuff":
            {
                var obj = new Result.RemoveBuff();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.Vulnerability":   
            case "Vulnerability":
            {
                var obj = new Result.Vulnerability();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.IncreaseDamage":   
            case "IncreaseDamage":
            {
                var obj = new Result.IncreaseDamage();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ExtraDefend":   
            case "ExtraDefend":
            {
                var obj = new Result.ExtraDefend();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ChangeCounter":   
            case "ChangeCounter":
            {
                var obj = new Result.ChangeCounter();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.AssignCounter":   
            case "AssignCounter":
            {
                var obj = new Result.AssignCounter();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.WinTheGame":   
            case "WinTheGame":
            {
                var obj = new Result.WinTheGame();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.GoToNextFloor":   
            case "GoToNextFloor":
            {
                var obj = new Result.GoToNextFloor();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.SetRoomBattleState":   
            case "SetRoomBattleState":
            {
                var obj = new Result.SetRoomBattleState();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ChangeNumeric":   
            case "ChangeNumeric":
            {
                var obj = new Result.ChangeNumeric();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.DamageJumpTextTemp":   
            case "DamageJumpTextTemp":
            {
                var obj = new Result.DamageJumpTextTemp();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.TargetDmg":   
            case "TargetDmg":
            {
                var obj = new Result.TargetDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.TargetHeal":   
            case "TargetHeal":
            {
                var obj = new Result.TargetHeal();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ShootDmg":   
            case "ShootDmg":
            {
                var obj = new Result.ShootDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ScreenDmg":   
            case "ScreenDmg":
            {
                var obj = new Result.ScreenDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.AoeDmg":   
            case "AoeDmg":
            {
                var obj = new Result.AoeDmg();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.TargetEffect":   
            case "TargetEffect":
            {
                var obj = new Result.TargetEffect();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ShootEffect":   
            case "ShootEffect":
            {
                var obj = new Result.ShootEffect();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.ScreenEffect":   
            case "ScreenEffect":
            {
                var obj = new Result.ScreenEffect();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            case "Result.AoeEffect":   
            case "AoeEffect":
            {
                var obj = new Result.AoeEffect();
                obj._typeIndex = Array.IndexOf(Types,type);
                return obj;
            }
            default: return null;
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderResultParam(ref ResultParam obj)
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
    public static ResultParam LoadJsonResultParam(SimpleJSON.JSONNode _json)
    {
        string type = _json["$type"];
        ResultParam obj;
        switch (type)
        {
            case "Result.DifferDmg":   
            case "DifferDmg":
            {
                obj = new Result.DifferDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "DifferDmg");
                break;
            }
            case "Result.FixedDmg":   
            case "FixedDmg":
            {
                obj = new Result.FixedDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "FixedDmg");
                break;
            }
            case "Result.DifferVamp":   
            case "DifferVamp":
            {
                obj = new Result.DifferVamp(); 
                obj._typeIndex = Array.IndexOf(Types, "DifferVamp");
                break;
            }
            case "Result.FixedVamp":   
            case "FixedVamp":
            {
                obj = new Result.FixedVamp(); 
                obj._typeIndex = Array.IndexOf(Types, "FixedVamp");
                break;
            }
            case "Result.CharaAttrDmg":   
            case "CharaAttrDmg":
            {
                obj = new Result.CharaAttrDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "CharaAttrDmg");
                break;
            }
            case "Result.DicePoolDmg":   
            case "DicePoolDmg":
            {
                obj = new Result.DicePoolDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "DicePoolDmg");
                break;
            }
            case "Result.BuffStackDmg":   
            case "BuffStackDmg":
            {
                obj = new Result.BuffStackDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "BuffStackDmg");
                break;
            }
            case "Result.ItemTagCountDmg":   
            case "ItemTagCountDmg":
            {
                obj = new Result.ItemTagCountDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "ItemTagCountDmg");
                break;
            }
            case "Result.FixedRecover":   
            case "FixedRecover":
            {
                obj = new Result.FixedRecover(); 
                obj._typeIndex = Array.IndexOf(Types, "FixedRecover");
                break;
            }
            case "Result.CharaAttrRecover":   
            case "CharaAttrRecover":
            {
                obj = new Result.CharaAttrRecover(); 
                obj._typeIndex = Array.IndexOf(Types, "CharaAttrRecover");
                break;
            }
            case "Result.GainExtraMana":   
            case "GainExtraMana":
            {
                obj = new Result.GainExtraMana(); 
                obj._typeIndex = Array.IndexOf(Types, "GainExtraMana");
                break;
            }
            case "Result.CharaAttrGainExtraMana":   
            case "CharaAttrGainExtraMana":
            {
                obj = new Result.CharaAttrGainExtraMana(); 
                obj._typeIndex = Array.IndexOf(Types, "CharaAttrGainExtraMana");
                break;
            }
            case "Result.RemoveMana":   
            case "RemoveMana":
            {
                obj = new Result.RemoveMana(); 
                obj._typeIndex = Array.IndexOf(Types, "RemoveMana");
                break;
            }
            case "Result.AddDice":   
            case "AddDice":
            {
                obj = new Result.AddDice(); 
                obj._typeIndex = Array.IndexOf(Types, "AddDice");
                break;
            }
            case "Result.AddDiceInSamePool":   
            case "AddDiceInSamePool":
            {
                obj = new Result.AddDiceInSamePool(); 
                obj._typeIndex = Array.IndexOf(Types, "AddDiceInSamePool");
                break;
            }
            case "Result.RemoveDiceById":   
            case "RemoveDiceById":
            {
                obj = new Result.RemoveDiceById(); 
                obj._typeIndex = Array.IndexOf(Types, "RemoveDiceById");
                break;
            }
            case "Result.RemoveDiceByIdInSamePool":   
            case "RemoveDiceByIdInSamePool":
            {
                obj = new Result.RemoveDiceByIdInSamePool(); 
                obj._typeIndex = Array.IndexOf(Types, "RemoveDiceByIdInSamePool");
                break;
            }
            case "Result.RemoveAllDiceById":   
            case "RemoveAllDiceById":
            {
                obj = new Result.RemoveAllDiceById(); 
                obj._typeIndex = Array.IndexOf(Types, "RemoveAllDiceById");
                break;
            }
            case "Result.RemoveDiceByTag":   
            case "RemoveDiceByTag":
            {
                obj = new Result.RemoveDiceByTag(); 
                obj._typeIndex = Array.IndexOf(Types, "RemoveDiceByTag");
                break;
            }
            case "Result.RemoveAllDiceByTag":   
            case "RemoveAllDiceByTag":
            {
                obj = new Result.RemoveAllDiceByTag(); 
                obj._typeIndex = Array.IndexOf(Types, "RemoveAllDiceByTag");
                break;
            }
            case "Result.MoveDiceById":   
            case "MoveDiceById":
            {
                obj = new Result.MoveDiceById(); 
                obj._typeIndex = Array.IndexOf(Types, "MoveDiceById");
                break;
            }
            case "Result.MoveAllDiceById":   
            case "MoveAllDiceById":
            {
                obj = new Result.MoveAllDiceById(); 
                obj._typeIndex = Array.IndexOf(Types, "MoveAllDiceById");
                break;
            }
            case "Result.MoveDiceByTag":   
            case "MoveDiceByTag":
            {
                obj = new Result.MoveDiceByTag(); 
                obj._typeIndex = Array.IndexOf(Types, "MoveDiceByTag");
                break;
            }
            case "Result.MoveAllDiceByTag":   
            case "MoveAllDiceByTag":
            {
                obj = new Result.MoveAllDiceByTag(); 
                obj._typeIndex = Array.IndexOf(Types, "MoveAllDiceByTag");
                break;
            }
            case "Result.PreRollDice":   
            case "PreRollDice":
            {
                obj = new Result.PreRollDice(); 
                obj._typeIndex = Array.IndexOf(Types, "PreRollDice");
                break;
            }
            case "Result.SelfClone":   
            case "SelfClone":
            {
                obj = new Result.SelfClone(); 
                obj._typeIndex = Array.IndexOf(Types, "SelfClone");
                break;
            }
            case "Result.SelfAddDice":   
            case "SelfAddDice":
            {
                obj = new Result.SelfAddDice(); 
                obj._typeIndex = Array.IndexOf(Types, "SelfAddDice");
                break;
            }
            case "Result.DmgByDiceFace":   
            case "DmgByDiceFace":
            {
                obj = new Result.DmgByDiceFace(); 
                obj._typeIndex = Array.IndexOf(Types, "DmgByDiceFace");
                break;
            }
            case "Result.SelfRemove":   
            case "SelfRemove":
            {
                obj = new Result.SelfRemove(); 
                obj._typeIndex = Array.IndexOf(Types, "SelfRemove");
                break;
            }
            case "Result.SelfMove":   
            case "SelfMove":
            {
                obj = new Result.SelfMove(); 
                obj._typeIndex = Array.IndexOf(Types, "SelfMove");
                break;
            }
            case "Result.SelfValueChange":   
            case "SelfValueChange":
            {
                obj = new Result.SelfValueChange(); 
                obj._typeIndex = Array.IndexOf(Types, "SelfValueChange");
                break;
            }
            case "Result.AddBuffToDiceByTag":   
            case "AddBuffToDiceByTag":
            {
                obj = new Result.AddBuffToDiceByTag(); 
                obj._typeIndex = Array.IndexOf(Types, "AddBuffToDiceByTag");
                break;
            }
            case "Result.ChangeDiceValueByTag":   
            case "ChangeDiceValueByTag":
            {
                obj = new Result.ChangeDiceValueByTag(); 
                obj._typeIndex = Array.IndexOf(Types, "ChangeDiceValueByTag");
                break;
            }
            case "Result.AddDecidedToDiceByTag":   
            case "AddDecidedToDiceByTag":
            {
                obj = new Result.AddDecidedToDiceByTag(); 
                obj._typeIndex = Array.IndexOf(Types, "AddDecidedToDiceByTag");
                break;
            }
            case "Result.AddBuff":   
            case "AddBuff":
            {
                obj = new Result.AddBuff(); 
                obj._typeIndex = Array.IndexOf(Types, "AddBuff");
                break;
            }
            case "Result.RemoveBuff":   
            case "RemoveBuff":
            {
                obj = new Result.RemoveBuff(); 
                obj._typeIndex = Array.IndexOf(Types, "RemoveBuff");
                break;
            }
            case "Result.Vulnerability":   
            case "Vulnerability":
            {
                obj = new Result.Vulnerability(); 
                obj._typeIndex = Array.IndexOf(Types, "Vulnerability");
                break;
            }
            case "Result.IncreaseDamage":   
            case "IncreaseDamage":
            {
                obj = new Result.IncreaseDamage(); 
                obj._typeIndex = Array.IndexOf(Types, "IncreaseDamage");
                break;
            }
            case "Result.ExtraDefend":   
            case "ExtraDefend":
            {
                obj = new Result.ExtraDefend(); 
                obj._typeIndex = Array.IndexOf(Types, "ExtraDefend");
                break;
            }
            case "Result.ChangeCounter":   
            case "ChangeCounter":
            {
                obj = new Result.ChangeCounter(); 
                obj._typeIndex = Array.IndexOf(Types, "ChangeCounter");
                break;
            }
            case "Result.AssignCounter":   
            case "AssignCounter":
            {
                obj = new Result.AssignCounter(); 
                obj._typeIndex = Array.IndexOf(Types, "AssignCounter");
                break;
            }
            case "Result.WinTheGame":   
            case "WinTheGame":
            {
                obj = new Result.WinTheGame(); 
                obj._typeIndex = Array.IndexOf(Types, "WinTheGame");
                break;
            }
            case "Result.GoToNextFloor":   
            case "GoToNextFloor":
            {
                obj = new Result.GoToNextFloor(); 
                obj._typeIndex = Array.IndexOf(Types, "GoToNextFloor");
                break;
            }
            case "Result.SetRoomBattleState":   
            case "SetRoomBattleState":
            {
                obj = new Result.SetRoomBattleState(); 
                obj._typeIndex = Array.IndexOf(Types, "SetRoomBattleState");
                break;
            }
            case "Result.ChangeNumeric":   
            case "ChangeNumeric":
            {
                obj = new Result.ChangeNumeric(); 
                obj._typeIndex = Array.IndexOf(Types, "ChangeNumeric");
                break;
            }
            case "Result.DamageJumpTextTemp":   
            case "DamageJumpTextTemp":
            {
                obj = new Result.DamageJumpTextTemp(); 
                obj._typeIndex = Array.IndexOf(Types, "DamageJumpTextTemp");
                break;
            }
            case "Result.TargetDmg":   
            case "TargetDmg":
            {
                obj = new Result.TargetDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "TargetDmg");
                break;
            }
            case "Result.TargetHeal":   
            case "TargetHeal":
            {
                obj = new Result.TargetHeal(); 
                obj._typeIndex = Array.IndexOf(Types, "TargetHeal");
                break;
            }
            case "Result.ShootDmg":   
            case "ShootDmg":
            {
                obj = new Result.ShootDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "ShootDmg");
                break;
            }
            case "Result.ScreenDmg":   
            case "ScreenDmg":
            {
                obj = new Result.ScreenDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "ScreenDmg");
                break;
            }
            case "Result.AoeDmg":   
            case "AoeDmg":
            {
                obj = new Result.AoeDmg(); 
                obj._typeIndex = Array.IndexOf(Types, "AoeDmg");
                break;
            }
            case "Result.TargetEffect":   
            case "TargetEffect":
            {
                obj = new Result.TargetEffect(); 
                obj._typeIndex = Array.IndexOf(Types, "TargetEffect");
                break;
            }
            case "Result.ShootEffect":   
            case "ShootEffect":
            {
                obj = new Result.ShootEffect(); 
                obj._typeIndex = Array.IndexOf(Types, "ShootEffect");
                break;
            }
            case "Result.ScreenEffect":   
            case "ScreenEffect":
            {
                obj = new Result.ScreenEffect(); 
                obj._typeIndex = Array.IndexOf(Types, "ScreenEffect");
                break;
            }
            case "Result.AoeEffect":   
            case "AoeEffect":
            {
                obj = new Result.AoeEffect(); 
                obj._typeIndex = Array.IndexOf(Types, "AoeEffect");
                break;
            }
            default: throw new SerializationException();
        }
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonResultParam(ResultParam _obj, SimpleJSON.JSONNode _json)
    {
        _json["$type"] = _obj.GetTypeStr();
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }


    public override string ToString()
    {
        return "{ "
        + "}";
    }
}

}

