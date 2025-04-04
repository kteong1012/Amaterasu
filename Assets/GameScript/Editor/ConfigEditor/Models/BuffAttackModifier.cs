
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

public sealed class BuffAttackModifier :  BuffHandler 
{
    public BuffAttackModifier()
    {
    }
    public override string GetTypeStr() => TYPE_STR;
    private const string TYPE_STR = "BuffAttackModifier";

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["modifier"];
            if (_fieldJson != null)
            {
                
                if (!_fieldJson.IsObject)
                {
                    throw new SerializationException();
                }
                modifier = GameEditor.ConfigEditor.Model.DamageModifier.DamageModifierParam.LoadJsonDamageModifierParam(_fieldJson);
            }
            else
            {
                modifier = DamageModifier.DamageModifierParam.Create("CommonModifier");
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {

        if (modifier != null)
        {
            { var __bjson = new JSONObject();  GameEditor.ConfigEditor.Model.DamageModifier.DamageModifierParam.SaveJsonDamageModifierParam(modifier, __bjson); _json["modifier"] = __bjson; }
        }
    }

    private static GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);

    public static void RenderBuffAttackModifier(BuffAttackModifier obj)
    {
        obj.Render();
    }

    public override void Render()
    {
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("修正器", "modifier"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("modifier", "修正器"), GUILayout.Width(100));
}
{
    if (this.modifier == null)
{   
    this.modifier = DamageModifier.DamageModifierParam.Create("CommonModifier");
}
    DamageModifier.DamageModifierParam.RenderDamageModifierParam(ref this.modifier);
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}    }
    public static BuffAttackModifier LoadJsonBuffAttackModifier(SimpleJSON.JSONNode _json)
    {
        BuffAttackModifier obj = new BuffAttackModifier();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonBuffAttackModifier(BuffAttackModifier _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 修正器
    /// </summary>
    public GameEditor.ConfigEditor.Model.DamageModifier.DamageModifierParam modifier;

    public override string ToString()
    {
        return "{ "
        + "modifier:" + modifier + ","
        + "}";
    }
}

}

