using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
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
        private Dictionary<string, Type> _buildPipelines;
        private SerializedObject _serializedObject;

        private void OnEnable()
        {
            LoadParameters();
            CollectBuildPipelines();
        }

        private void OnGUI()
        {
            DrawEnvironmentInfo();
            DrawPipelineOption();
            DrawBuildParameter();

            // check if compiling
            if (EditorApplication.isCompiling)
            {
                EditorGUILayout.HelpBox("编译中，请稍后再试", MessageType.Warning);
                GUI.enabled = false;
            }
            if (GUILayout.Button("构建"))
            {
                if (!_buildPipelines.TryGetValue(_buildParameters.pipelineName, out var pipelineType))
                {
                    _buildParameters.pipelineName = _buildPipelines.Keys.FirstOrDefault();
                    if (string.IsNullOrEmpty(_buildParameters.pipelineName))
                    {
                        EditorUtility.DisplayDialog("错误", "构建流程实力创建失败", "确定");
                        return;
                    }
                    pipelineType = _buildPipelines[_buildParameters.pipelineName];
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

            if (_buildPipelines == null)
            {
                return;
            }

            var options = new List<string>(_buildPipelines.Keys);
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

        private void CollectBuildPipelines()
        {
            _buildPipelines = new Dictionary<string, Type>();
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
                        _buildPipelines[attr.Name] = type;
                    }
                }
            }

            if (_buildPipelines.Count > 0)
            {
                if (!_buildPipelines.ContainsKey(_buildParameters.pipelineName))
                {
                    _buildParameters.pipelineName = _buildPipelines.Keys.FirstOrDefault();
                }
            }
        }

        private bool CheckBuildGroup(string name)
        {
            if (string.IsNullOrEmpty(_buildParameters.pipelineName))
            {
                return false;
            }
            var pipeline = _buildPipelines[_buildParameters.pipelineName];
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
