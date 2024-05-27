//using Game;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UI;

//namespace GameEditor
//{
//    [UnityEditor.CustomEditor(typeof(Game.UIComponent))]
//    public class UIComponentInspector : UnityEditor.Editor
//    {
//        private static Dictionary<string, Type> _componentTypes = new Dictionary<string, Type>()
//        {
//            { nameof(GameObject), typeof(GameObject) },
//            { nameof(Transform), typeof(Transform) },
//            { nameof(RectTransform), typeof(RectTransform) },
//            { nameof(Image), typeof(Image) },
//            { nameof(Text), typeof(Text) },
//            { nameof(Button), typeof(Button) },
//            { nameof(UXScrollRect), typeof(UXScrollRect) },
//            { nameof(RawImage), typeof(RawImage) }
//        };

//        private static Dictionary<string, string> _componentPrefixNames = new Dictionary<string, string>()
//        {
//            { nameof(GameObject), "go" },
//            { nameof(Transform) , "tran" },
//            { nameof(RectTransform) , "rect" },
//            { nameof(Image) , "img" },
//            { nameof(Text) , "txt" },
//            { nameof(Button) , "btn" },
//            { nameof(UXScrollRect) , "scroll" },
//            { nameof(RawImage) , "tex" }
//        };

//        public override void OnInspectorGUI()
//        {
//            Undo.RecordObject(serializedObject.targetObject, "UIComponent Modify");

//            var uiComponent = serializedObject.targetObject as Game.UIComponent;


//            if (uiComponent == null)
//            {
//                return;
//            }
//            if (uiComponent.bindNodes == null)
//            {
//                uiComponent.bindNodes = new List<Game.BindNode>();
//            }


//            GUILayout.BeginVertical("box");
//            GUILayout.Label("界面配置", EditorStyles.boldLabel);
//            // 界面名称
//            uiComponent.uiName = EditorGUILayout.TextField("界面名称", uiComponent.uiName);

//            GUILayout.EndVertical();

//            GUILayout.BeginVertical("box");
//            GUILayout.Label("绑定节点", EditorStyles.boldLabel);
//            for (int i = 0; i < uiComponent.bindNodes.Count; i++)
//            {
//                GUILayout.BeginVertical("box");
//                GUILayout.BeginHorizontal();
//                var bindNode = uiComponent.bindNodes[i];
//                bindNode.name = GUILayout.TextField(bindNode.name, GUILayout.Width(200));
//                if (GUILayout.Button("建议", GUILayout.Width(50)))
//                {
//                    var suggestedName = GetSuggestedName(uiComponent.bindNodes[i]);
//                    if (suggestedName != null)
//                    {
//                        if (uiComponent.bindNodes.Any((x) => x != bindNode && x.name == suggestedName))
//                        {
//                            // 匹配_{num}后缀，如果有则取出num并且给num+1，否则直接加_1
//                            var match = System.Text.RegularExpressions.Regex.Match(suggestedName, @"_(\d+)$");
//                            if (match.Success)
//                            {
//                                var num = int.Parse(match.Groups[1].Value);
//                                suggestedName = suggestedName.Replace($"_{num}", $"_{num + 1}");
//                            }
//                            else
//                            {
//                                suggestedName = $"{suggestedName}_1";
//                            }
//                        }
//                        uiComponent.bindNodes[i].name = suggestedName;
//                    }
//                }

//                GUILayout.FlexibleSpace();
//                if (GUILayout.Button("↑"))
//                {
//                    if (i > 0)
//                    {
//                        var temp = uiComponent.bindNodes[i - 1];
//                        uiComponent.bindNodes[i - 1] = bindNode;
//                        uiComponent.bindNodes[i] = temp;
//                    }
//                }
//                if (GUILayout.Button("↓"))
//                {
//                    if (i < uiComponent.bindNodes.Count - 1)
//                    {
//                        var temp = uiComponent.bindNodes[i + 1];
//                        uiComponent.bindNodes[i + 1] = bindNode;
//                        uiComponent.bindNodes[i] = temp;
//                    }
//                }

//                //复制按钮
//                var copyIcon = EditorGUIUtility.IconContent("Clipboard").image;
//                if (GUILayout.Button(copyIcon))
//                {
//                    uiComponent.bindNodes.Insert(i, new Game.BindNode()
//                    {
//                        componentType = bindNode.componentType,
//                        isArray = bindNode.isArray,
//                        name = bindNode.name + "_Copy",
//                        nodes = bindNode.nodes.Select(x => x).ToList()
//                    });
//                    i++;
//                }

//                //删除按钮
//                var trashIcon = EditorGUIUtility.IconContent("TreeEditor.Trash").image;
//                if (GUILayout.Button(trashIcon))
//                {
//                    uiComponent.bindNodes.RemoveAt(i);
//                    i--;
//                    continue;
//                }
//                GUILayout.EndHorizontal();

//                GUILayout.BeginHorizontal();
//                if (_componentTypes.ContainsValue(_componentTypes[bindNode.componentType]))
//                {
//                    bindNode.componentType = _componentTypes.FirstOrDefault(x => x.Value == _componentTypes[bindNode.componentType]).Key;
//                }
//                var index = EditorGUILayout.Popup(Array.IndexOf(_componentTypes.Keys.ToArray(), bindNode.componentType), _componentTypes.Keys.ToArray(), GUILayout.Width(200));
//                bindNode.componentType = _componentTypes.ElementAt(index).Key;
//                bindNode.isArray = GUILayout.Toggle(bindNode.isArray, "数组");
//                if (bindNode.isArray)
//                {
//                    GUILayout.FlexibleSpace();
//                    var addIcon = EditorGUIUtility.IconContent("Toolbar Plus").image;
//                    if (GUILayout.Button(addIcon))
//                    {
//                        bindNode.nodes = bindNode.nodes.Concat(new GameObject[] { null }).ToList();
//                    }
//                    var removeIcon = EditorGUIUtility.IconContent("Toolbar Minus").image;
//                    if (GUILayout.Button(removeIcon))
//                    {
//                        bindNode.nodes = bindNode.nodes.Take(bindNode.nodes.Count - 1).ToList();
//                    }
//                }
//                GUILayout.EndHorizontal();
//                if (bindNode.isArray)
//                {
//                    for (int j = 0; j < bindNode.nodes.Count; j++)
//                    {
//                        GUILayout.BeginHorizontal();
//                        var displayObject = GetComponentObject(bindNode.nodes[j], bindNode.componentType);
//                        displayObject = EditorGUILayout.ObjectField(displayObject, _componentTypes[bindNode.componentType], true);
//                        bindNode.nodes[j] = GetGameObject(displayObject);
//                        GUILayout.EndHorizontal();
//                    }
//                }
//                else
//                {
//                    if (bindNode.nodes.Count > 1)
//                    {
//                        bindNode.nodes = bindNode.nodes.Take(1).ToList();
//                    }
//                    else if (bindNode.nodes.Count == 0)
//                    {
//                        bindNode.nodes.Add(null);
//                    }
//                    var displayObject = GetComponentObject(bindNode.nodes[0], bindNode.componentType);
//                    displayObject = EditorGUILayout.ObjectField(displayObject, _componentTypes[bindNode.componentType], true);
//                    bindNode.nodes[0] = GetGameObject(displayObject);
//                }
//                GUILayout.EndVertical();
//            }
//            GUILayout.BeginHorizontal();
//            GUILayout.FlexibleSpace();
//            {
//                var addIcon = EditorGUIUtility.IconContent("Toolbar Plus").image;
//                if (GUILayout.Button(addIcon))
//                {
//                    uiComponent.bindNodes.Add(new Game.BindNode()
//                    {
//                        componentType = _componentTypes.First().Key,
//                        isArray = false,
//                        name = ""
//                    });
//                }
//            }
//            GUILayout.EndHorizontal();

//            // 如果target是一个预制体，那么就显示一个按钮，点击后将所有的绑定节点的GameObject设置为null
//            if (PrefabUtility.GetPrefabAssetType(serializedObject.targetObject) != PrefabAssetType.Regular)
//            {
//                GUILayout.BeginHorizontal();
//                GUILayout.FlexibleSpace();
//                if (GUILayout.Button("生成代码"))
//                {
//                    GenerateCode(uiComponent);
//                }
//                GUILayout.EndHorizontal();
//            }

//            GUILayout.EndVertical();


//            serializedObject.Update();
//            serializedObject.ApplyModifiedProperties();
//        }

//        private void GenerateCode(UIComponent uiComponent)
//        {

//        }

//        private string GetSuggestedName(Game.BindNode bindNode)
//        {
//            if (bindNode == null || bindNode.nodes == null || bindNode.nodes.Count == 0 || bindNode.nodes.All(x => x == null))
//            {
//                Debug.LogWarning("没有检测到绑定的物体");
//                return null;
//            }
//            if (!_componentPrefixNames.TryGetValue(bindNode.componentType, out var prefix))
//            {
//                prefix = bindNode.componentType;
//            }
//            var name = bindNode.nodes[0].name.Replace(" ", "_");
//            if (name.ToLower().StartsWith(prefix.ToLower()))
//            {
//                name = name.Substring(prefix.Length);
//            }
//            name = name.TrimStart('_');

//            var suffix = "";
//            if (bindNode.isArray)
//            {
//                suffix = "s";
//            }
//            return $"{prefix}_{name}{suffix}";
//        }

//        private UnityEngine.Object GetComponentObject(GameObject gob, string typeName)
//        {
//            if (gob == null)
//            {
//                return null;
//            }
//            if (typeName == nameof(GameObject))
//            {
//                return gob;
//            }
//            return gob.GetComponent(_componentTypes[typeName]);
//        }

//        private GameObject GetGameObject(UnityEngine.Object obj)
//        {
//            if (obj == null)
//            {
//                return null;
//            }
//            if (obj is GameObject)
//            {
//                return obj as GameObject;
//            }
//            return (obj as Component).gameObject;
//        }
//    }
//}
