
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;




namespace GameEditor.ConfigEditor.Model
{
    public partial class BattleAICategory : IConfigEditorTable
    {
        private List<GameEditor.ConfigEditor.Model.BattleAI> _datas = new List<GameEditor.ConfigEditor.Model.BattleAI>();
        private readonly string _dataFilePath;
        private readonly Dictionary<string, string> _originalDataJsons = new Dictionary<string, string>();
        private string _originalTableJson;

        public bool IsLoaded => _datas.Count > 0;
        private string _name => "BattleAICategory";
        private GUIStyle _areaStyle = new GUIStyle(GUI.skin.button);


        public BattleAICategory(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
        }

        public void Load()
        {
            _datas.Clear();
            _originalDataJsons.Clear();

            if (File.Exists(_dataFilePath))
            {
                var jsonText = File.ReadAllText(_dataFilePath);
                var json = JSON.Parse(jsonText);
                if (json.IsArray)
                {
                    foreach (var node in json.AsArray)
                    {
                        var data = GameEditor.ConfigEditor.Model.BattleAI.LoadJsonBattleAI(node.Value);
                        _datas.Add(data);
                        var dataJson = node.Value.ToString(4);
                        data.OriginalDataJson = dataJson;
                        _originalDataJsons.Add(GetId(data), dataJson);
                    }
                }
                else
                {
                    var data = GameEditor.ConfigEditor.Model.BattleAI.LoadJsonBattleAI(json);
                    _datas.Add(data);
                    var dataJson = json.ToString(4);
                    data.OriginalDataJson = dataJson;
                    _originalDataJsons.Add(GetId(data), dataJson);
                }
            }

            _originalTableJson = GetTableJson();
        }

        public void Save()
        {
            for (int i = 0; i < _datas.Count; i++)
            {
                if (string.IsNullOrEmpty(GetId(_datas[i])))
                {
                    EditorUtility.DisplayDialog("提示", $"{_name}表第[{i}]个元素的id为空", "确定");
                    return;
                }
            }
            var ids = new Dictionary<string, List<int>>();
            for (int i = 0; i < _datas.Count; i++)
            {
                var id = GetId(_datas[i]);
                if (!ids.ContainsKey(id))
                {
                    ids.Add(id, new List<int>());
                }
                ids[id].Add(i);
            }
            var repeatIds = ids.Where(pair => pair.Value.Count > 1).Select(pair => pair.Key).ToList();
            if (repeatIds.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var id in repeatIds)
                {
                    sb.Append($"{id}重复出现在{string.Join(", ", ids[id])}\n");
                }
                EditorUtility.DisplayDialog("提示", $"{_name}表id重复\n{sb}", "确定");
                return;
            }

            var jsonText = GetTableJson();
            File.WriteAllText(_dataFilePath, jsonText);

            _originalDataJsons.Clear();
            foreach (var data in _datas)
            {
                data.OriginalDataJson = GetDataJson(data);
                _originalDataJsons.Add(GetId(data), data.OriginalDataJson);
            }
            _originalTableJson = jsonText;
        }

        private string GetTableJson()
        {
            var jsonArray = new JSONArray();
            foreach (var data in _datas)
            {
                var json = new JSONObject();
                GameEditor.ConfigEditor.Model.BattleAI.SaveJsonBattleAI(data, json);
                jsonArray.Add(json);
            }
            return jsonArray.ToString(4);
        }

        private string GetDataJson(GameEditor.ConfigEditor.Model.BattleAI data)
        {
            var json = new JSONObject();
            GameEditor.ConfigEditor.Model.BattleAI.SaveJsonBattleAI(data, json);
            return json.ToString(4);
        }


        private Vector2 _idScrollPos;
        private Vector2 _dataScrollPos;
        private int _selectIndex;
        public void OnGUI()
        {
            GUILayout.BeginHorizontal("Box");
            GUILayout.BeginVertical("Box");
            _idScrollPos = GUILayout.BeginScrollView(_idScrollPos, GUILayout.Width(200));
            for (int i = 0; i < _datas.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (_selectIndex == i)
                {
                    GUI.color = Color.cyan;
                }
                else
                {
                    GUI.color = Color.white;
                }
                EditorGUILayout.LabelField($"[{i}]", GUILayout.Width(50));
                if (GUILayout.Button(GetId(_datas[i])))
                {
                    _selectIndex = i;
                }
                GUI.color = Color.white;
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                _datas.Add(new GameEditor.ConfigEditor.Model.BattleAI());
            }
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                if (_selectIndex >= 0 && _selectIndex < _datas.Count)
                {
                    _datas.RemoveAt(_selectIndex);
                    if (_selectIndex >= _datas.Count)
                    {
                        _selectIndex = _datas.Count - 1;
                    }
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("复制Json", GUILayout.Width(100)))
            {
                if (__SelectData != null)
                {
                    var text = GetDataJson(__SelectData);
                    GUIUtility.systemCopyBuffer = text;
                    EditorUtility.DisplayDialog("提示", $"已复制到剪切板:\n{text}", "确定");
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "请选择数据", "确定");
                }
            }
            if (GUILayout.Button("新增拷贝", GUILayout.Width(100)))
            {
                if (__SelectData != null)
                {
                    var text = GetDataJson(__SelectData);
                    var json = JSON.Parse(text);
                    var data = GameEditor.ConfigEditor.Model.BattleAI.LoadJsonBattleAI(json);
                    _selectIndex = _datas.Count;
                    _datas.Add(data);
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "请选择数据", "确定");
                }
            }
            if (GUILayout.Button("预览差异", GUILayout.Width(100)))
            {
                if (__SelectData != null)
                {
                    var id = GetId(__SelectData);
                    var originalJson = __SelectData.OriginalDataJson ?? "";
                    var newJson = GetDataJson(__SelectData);
                    FileDiffTool.ShowWindow(originalJson, newJson, $"{_name}:{id}");
                }
            }
            if (GUILayout.Button("预览表差异", GUILayout.Width(100)))
            {
                var originalJson = _originalTableJson;
                var newJson = GetTableJson();
                FileDiffTool.ShowWindow(originalJson, newJson, _name);
            }
            if (GUILayout.Button("打开Json文件", GUILayout.Width(100)))
            {
                if (File.Exists(_dataFilePath))
                {
                    EditorUtility.OpenWithDefaultApp(_dataFilePath);
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "文件不存在", "确定");
                }
            }
            if (GUILayout.Button("还原当前数据", GUILayout.Width(100)))
            {
                if (EditorUtility.DisplayDialog("提示", "确定还原?", "确定", "取消"))
                {
                    if (__SelectData != null)
                    {
                        var id = GetId(__SelectData);
                        var originalJson = __SelectData.OriginalDataJson ?? "";
                        if (string.IsNullOrEmpty(originalJson))
                        {
                            EditorUtility.DisplayDialog("提示", "这是新数据，无需还原", "确定");
                        }
                        else
                        {
                            var json = JSON.Parse(originalJson);
                            var data = GameEditor.ConfigEditor.Model.BattleAI.LoadJsonBattleAI(json);
                            data.OriginalDataJson = originalJson;
                            _datas[_selectIndex] = data;
                        }
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("提示", "请选择数据", "确定");   
                    }
                }
            }
            GUILayout.EndHorizontal();
            _dataScrollPos = GUILayout.BeginScrollView(_dataScrollPos);
            if (__SelectData != default)
            {
                var renderData = __SelectData;
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
renderData.id = UnityEditor.EditorGUILayout.IntField(renderData.id, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("随机行动", "act_args"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("act_args", "随机行动"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = renderData.actArgs.Count;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            renderData.actArgs.RemoveAt(__i1);
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        GameEditor.ConfigEditor.Model.AiArg __e1 = renderData.actArgs[__i1];
        {
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("技能id", "skill_id"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("skill_id", "技能id"), GUILayout.Width(100));
}
__e1.skillId = UnityEditor.EditorGUILayout.IntField(__e1.skillId, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("AI索敌参数", "target_param"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("target_param", "AI索敌参数"), GUILayout.Width(100));
}
{
    if (__e1.targetParam == null)
{   
    __e1.targetParam = BattleAIPickTarget.PickTargetParam.Create("SkillAoe");
}
    BattleAIPickTarget.PickTargetParam.RenderPickTargetParam(ref __e1.targetParam);
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
};
        renderData.actArgs[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        GameEditor.ConfigEditor.Model.AiArg __e1;
        __e1 = new AiArg();;
        renderData.actArgs.Add(__e1);
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            GameEditor.ConfigEditor.Model.AiArg __importElement1;
            if(!__importJson1.IsObject) { throw new SerializationException(); }  __importElement1 = GameEditor.ConfigEditor.Model.AiArg.LoadJsonAiArg(__importJson1);
            renderData.actArgs.Add(__importElement1);
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("预设行动", "preset_args"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("preset_args", "预设行动"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = renderData.presetArgs.Count;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            renderData.presetArgs.RemoveAt(__i1);
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        GameEditor.ConfigEditor.Model.BattleAIPickTarget.PresetOrLoopArgs __e1 = renderData.presetArgs[__i1];
        {
    if (__e1 == null)
{   
    __e1 = BattleAIPickTarget.PresetOrLoopArgs.Create("NoRandom");
}
    BattleAIPickTarget.PresetOrLoopArgs.RenderPresetOrLoopArgs(ref __e1);
};
        renderData.presetArgs[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        GameEditor.ConfigEditor.Model.BattleAIPickTarget.PresetOrLoopArgs __e1;
        __e1 = BattleAIPickTarget.PresetOrLoopArgs.Create("NoRandom");;
        renderData.presetArgs.Add(__e1);
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            GameEditor.ConfigEditor.Model.BattleAIPickTarget.PresetOrLoopArgs __importElement1;
            
if (!__importJson1.IsObject)
{
    throw new SerializationException();
}
__importElement1 = GameEditor.ConfigEditor.Model.BattleAIPickTarget.PresetOrLoopArgs.LoadJsonPresetOrLoopArgs(__importJson1);
            renderData.presetArgs.Add(__importElement1);
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("循环行动", "loop_args"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("loop_args", "循环行动"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = renderData.loopArgs.Count;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            renderData.loopArgs.RemoveAt(__i1);
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        GameEditor.ConfigEditor.Model.BattleAIPickTarget.PresetOrLoopArgs __e1 = renderData.loopArgs[__i1];
        {
    if (__e1 == null)
{   
    __e1 = BattleAIPickTarget.PresetOrLoopArgs.Create("NoRandom");
}
    BattleAIPickTarget.PresetOrLoopArgs.RenderPresetOrLoopArgs(ref __e1);
};
        renderData.loopArgs[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        GameEditor.ConfigEditor.Model.BattleAIPickTarget.PresetOrLoopArgs __e1;
        __e1 = BattleAIPickTarget.PresetOrLoopArgs.Create("NoRandom");;
        renderData.loopArgs.Add(__e1);
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            GameEditor.ConfigEditor.Model.BattleAIPickTarget.PresetOrLoopArgs __importElement1;
            
if (!__importJson1.IsObject)
{
    throw new SerializationException();
}
__importElement1 = GameEditor.ConfigEditor.Model.BattleAIPickTarget.PresetOrLoopArgs.LoadJsonPresetOrLoopArgs(__importJson1);
            renderData.loopArgs.Add(__importElement1);
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("额外行动", "extra_args"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("extra_args", "额外行动"), GUILayout.Width(100));
}
{
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);
    int __n1 = renderData.extraArgs.Count;
    UnityEditor.EditorGUILayout.LabelField("长度: " + __n1.ToString());
    for (int __i1 = 0; __i1 < __n1; __i1++)
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("-", GUILayout.Width(20)))
        {
            renderData.extraArgs.RemoveAt(__i1);
            UnityEditor.EditorGUILayout.EndHorizontal();
            break;
        }
        UnityEditor.EditorGUILayout.LabelField(__i1.ToString(), GUILayout.Width(20));
        GameEditor.ConfigEditor.Model.AiArg __e1 = renderData.extraArgs[__i1];
        {
    UnityEditor.EditorGUILayout.BeginVertical(_areaStyle);UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("技能id", "skill_id"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("skill_id", "技能id"), GUILayout.Width(100));
}
__e1.skillId = UnityEditor.EditorGUILayout.IntField(__e1.skillId, GUILayout.Width(150));
UnityEditor.EditorGUILayout.EndHorizontal();UnityEditor.EditorGUILayout.BeginHorizontal();
if (ConfigEditorSettings.showComment)
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("AI索敌参数", "target_param"), GUILayout.Width(100));
}
else
{
    UnityEditor.EditorGUILayout.LabelField(new UnityEngine.GUIContent("target_param", "AI索敌参数"), GUILayout.Width(100));
}
{
    if (__e1.targetParam == null)
{   
    __e1.targetParam = BattleAIPickTarget.PickTargetParam.Create("SkillAoe");
}
    BattleAIPickTarget.PickTargetParam.RenderPickTargetParam(ref __e1.targetParam);
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
};
        renderData.extraArgs[__i1] = __e1;
        UnityEditor.EditorGUILayout.EndHorizontal();
    }
    UnityEditor.EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("+", GUILayout.Width(20)))
    {
        GameEditor.ConfigEditor.Model.AiArg __e1;
        __e1 = new AiArg();;
        renderData.extraArgs.Add(__e1);
    }
    if (ConfigEditorSettings.showImportButton && GUILayout.Button("import", GUILayout.Width(100)))
    {
        ConfigEditorImportWindow.Open((__importJsonText1) => 
        {
            var __importJson1 = SimpleJSON.JSON.Parse(__importJsonText1);
            GameEditor.ConfigEditor.Model.AiArg __importElement1;
            if(!__importJson1.IsObject) { throw new SerializationException(); }  __importElement1 = GameEditor.ConfigEditor.Model.AiArg.LoadJsonAiArg(__importJson1);
            renderData.extraArgs.Add(__importElement1);
        });
    }
    UnityEditor.EditorGUILayout.EndHorizontal();
    UnityEditor.EditorGUILayout.EndVertical();
}
UnityEditor.EditorGUILayout.EndHorizontal();    UnityEditor.EditorGUILayout.EndVertical();
}                //GameEditor.ConfigEditor.Model.BattleAI.RenderBattleAI(__SelectData);
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private string GetId(GameEditor.ConfigEditor.Model.BattleAI data)
        {
            if (data == null)
            {
                throw new Exception("data is null");
            }
            return data.id.ToString();
        }

        public void Sort()
        {
            var temp = GetId(__SelectData);
            _datas = _datas.OrderBy(data => Convert.ToInt64(GetId(data))).ToList();
            if (!string.IsNullOrEmpty(temp))
            {
                _selectIndex = _datas.FindIndex(data => GetId(data) == temp);
            }
        }

        public void ResolveRef(EditorConfig tables)
        {
        }

        private GameEditor.ConfigEditor.Model.BattleAI __SelectData
        {
            get
            {
                if (_selectIndex >= 0 && _selectIndex < _datas.Count)
                {
                    return _datas[_selectIndex];
                }
                return null;
            }
        }
    }
}
