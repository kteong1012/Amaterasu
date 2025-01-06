using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public static class BuildPipelineAliasCollection
    {
        private static Dictionary<string, Type> _aliasToTypeMap;
        public static Dictionary<string, Type> AliasToTypeMap => _aliasToTypeMap;

        static BuildPipelineAliasCollection()
        {
            CollectBuildPipelines();
        }
        private static void CollectBuildPipelines()
        {
            _aliasToTypeMap = new Dictionary<string, Type>();
            var ass = typeof(IBuildPipline).Assembly;
            var types = ass.GetTypes()
                .Where(t =>
                {
                    if (t.IsAbstract)
                    {
                        return false;
                    }
                    if (t.IsInterface)
                    {
                        return false;
                    }

                    return t.GetInterfaces().Any(t => t == typeof(IBuildPipline));
                });

            var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

            // 获取IBuildPipeline的所有实现类
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<BuildPipelineAttribute>();
                if (attr != null)
                {
                    if (targetGroup == attr.TargetGroup)
                    {
                        _aliasToTypeMap[attr.Name] = type;
                    }
                }
            }
        }

        public static Type GetPipelineType(string nameOrAlias)
        {
            // 优先级：alias > fullName > name
            if (_aliasToTypeMap.TryGetValue(nameOrAlias, out var type))
            {
                return type;
            }

            if (_aliasToTypeMap.Values.Any(t => t.FullName == nameOrAlias))
            {
                return _aliasToTypeMap.Values.First(t => t.FullName == nameOrAlias);
            }

            if (_aliasToTypeMap.Values.Any(t => t.Name == nameOrAlias))
            {
                return _aliasToTypeMap.Values.First(t => t.Name == nameOrAlias);
            }

            return null;
        }
    }

    public class BuildWindow : EditorWindow
    {
        [MenuItem("Tools/Build/BuildWindow")]
        public static void ShowWindow()
        {
            var window = GetWindow<BuildWindow>();
            window.titleContent = new GUIContent("BuildWindow");
            window.Show();
        }

        private readonly string _buildParametersAssetPath = "Assets/GameScript/Editor/BuildTools/BuildPipline/BuildParameters.asset";

        private BuildParameters _buildParameters;
        private Dictionary<string, BuildParamGroup> _buildParamGroups;
        private Dictionary<string, SerializedProperty> _buildParamProperties;
        private SerializedObject _serializedObject;

        private void OnEnable()
        {
            LoadParameters();


            if (BuildPipelineAliasCollection.AliasToTypeMap.Count > 0)
            {
                if (!BuildPipelineAliasCollection.AliasToTypeMap.ContainsKey(_buildParameters.pipelineName))
                {
                    _buildParameters.pipelineName = BuildPipelineAliasCollection.AliasToTypeMap.Keys.FirstOrDefault();
                }
            }
        }

        private Vector2 _scrollPosition;

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            DrawWindow();
            EditorGUILayout.EndScrollView();
        }

        private void DrawWindow()
        {
            DrawEnvironmentInfo();
            DrawPipelineOption();
            DrawBuildParameter();

            var aliasToTypeMap = BuildPipelineAliasCollection.AliasToTypeMap;

            // check if compiling
            if (EditorApplication.isCompiling)
            {
                EditorGUILayout.HelpBox("编译中，请稍后再试", MessageType.Warning);
                GUI.enabled = false;
            }
            if (GUILayout.Button("构建"))
            {
                if (!aliasToTypeMap.TryGetValue(_buildParameters.pipelineName, out var pipelineType))
                {
                    _buildParameters.pipelineName = aliasToTypeMap.Keys.FirstOrDefault();
                    if (string.IsNullOrEmpty(_buildParameters.pipelineName))
                    {
                        EditorUtility.DisplayDialog("错误", "构建流程实力创建失败", "确定");
                        return;
                    }
                    pipelineType = aliasToTypeMap[_buildParameters.pipelineName];
                }

                var pipeline = Activator.CreateInstance(pipelineType) as IBuildPipline;
                if (pipeline == null)
                {
                    EditorUtility.DisplayDialog("错误", "构建流程实力创建失败", "确定");
                    return;
                }

                pipeline.Build(_buildParameters);
            }
            GUI.enabled = true;
        }

        private void DrawEnvironmentInfo()
        {
            GUILayout.BeginVertical("box");
            var content = new GUIContent("环境信息");
            var style = new GUIStyle(EditorStyles.boldLabel);
            EditorGUILayout.LabelField(content, style);

            // Unity版本
            EditorGUILayout.LabelField("Unity版本", Application.unityVersion);
            // 操作系统
            EditorGUILayout.LabelField("操作系统", SystemInfo.operatingSystem);
            // 当前平台
            EditorGUILayout.LabelField("当前平台", EditorUserBuildSettings.activeBuildTarget.ToString());

            GUILayout.EndVertical();
        }

        private void DrawPipelineOption()
        {
            GUILayout.BeginVertical("box");
            var content = new GUIContent("构建流程");
            var style = new GUIStyle(EditorStyles.boldLabel);
            EditorGUILayout.LabelField(content, style);

            var aliasToTypeMap = BuildPipelineAliasCollection.AliasToTypeMap;

            if (aliasToTypeMap == null)
            {
                return;
            }

            var options = new List<string>(aliasToTypeMap.Keys);
            var selectedIndex = options.IndexOf(_buildParameters.pipelineName);
            selectedIndex = EditorGUILayout.Popup("选择构建流程", selectedIndex, options.ToArray());
            if (selectedIndex >= 0)
            {
                _buildParameters.pipelineName = options[selectedIndex];
            }

            GUILayout.EndVertical();
        }

        private void DrawBuildParameter()
        {
            GUILayout.BeginVertical("box");
            var content = new GUIContent("构建参数");
            var style = new GUIStyle(EditorStyles.boldLabel);
            EditorGUILayout.LabelField(content, style);

            _serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            foreach (var property in _buildParamProperties.Values)
            {
                if (property.name == nameof(_buildParameters.exportProject))
                {
                    var isAndroid = _buildParameters.BuildTarget is BuildTarget.Android;
                    var isWindows = _buildParameters.BuildTarget is BuildTarget.StandaloneWindows or BuildTarget.StandaloneWindows64;
                    if (!isAndroid && !isWindows)
                    {
                        property.boolValue = false;
                        continue;
                    }
                }
                if (property.name == nameof(_buildParameters.cdnHostUrl))
                {
                    if (!_buildParameters.enableHotupdate)
                    {
                        continue;
                    }
                }
                if (CheckBuildGroup(property.name))
                {
                    continue;
                }

                EditorGUILayout.PropertyField(property);
            }
            if (EditorGUI.EndChangeCheck())
            {
                _serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndVertical();
        }

        private void LoadParameters()
        {
            var buildParameters = AssetDatabase.LoadAssetAtPath<BuildParameters>(_buildParametersAssetPath);
            if (buildParameters == null)
            {
                buildParameters = CreateInstance<BuildParameters>();
                AssetDatabase.CreateAsset(buildParameters, _buildParametersAssetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            _buildParameters = buildParameters;

            // _serializedObject
            _serializedObject?.Dispose();
            _serializedObject = new SerializedObject(_buildParameters);

            // _buildParamGroups && _buildParamProperties
            _buildParamGroups = new Dictionary<string, BuildParamGroup>();
            _buildParamProperties = new Dictionary<string, SerializedProperty>();
            var fields = typeof(BuildParameters).GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                var attr = field.GetCustomAttribute<BuildParamGroupAttribute>();
                if (attr != null)
                {
                    _buildParamGroups[field.Name] = attr.group;
                    _buildParamProperties[field.Name] = _serializedObject.FindProperty(field.Name);
                }
            }
        }

        private bool CheckBuildGroup(string name)
        {
            if (string.IsNullOrEmpty(_buildParameters.pipelineName))
            {
                return false;
            }
            var pipeline = BuildPipelineAliasCollection.AliasToTypeMap[_buildParameters.pipelineName];
            if (pipeline == null)
            {
                return false;
            }

            var attr = pipeline.GetCustomAttribute<BuildPipelineAttribute>();
            if (attr == null)
            {
                return false;
            }

            if (!_buildParamGroups.ContainsKey(name))
            {
                return false;
            }

            var buildGroup = _buildParamGroups[name];

            // buildGroup 必须是 attr.ParamGroup 的子集
            return (attr.ParamGroup & buildGroup) != buildGroup;
        }
    }
}
