using System;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game
{
    public static class LocalizationComponentChecker
    {
        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod()
        {
            PrefabStage.prefabSaving -= OnPrefabSaving;
            PrefabStage.prefabSaving += OnPrefabSaving;
        }

        private static void OnPrefabSaving(GameObject @object)
        {
            UpdateLocalizationTexts(@object.transform);
        }

        private static void UpdateLocalizationTexts(Transform node)
        {
            if (node == null)
            {
                return;
            }
            if (IsPrefab(node.gameObject))
            {
                return;
            }

            for (int i = 0; i < node.childCount; i++)
            {
                var child = node.GetChild(i);
                UpdateLocalizationTexts(child);
            }

            var text = node.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                if (node.name.StartsWith("_") || text.text.IsNullOrWhiteSpace())
                {
                    var localization = node.GetComponent<Localization>();
                    if (localization != null)
                    {
                        UnityEngine.Object.DestroyImmediate(localization);
                        Debug.Log($"<color=#FFA500>{node.name}</color><color=#FF0000>删除</color>了一个Localization组件", node.gameObject);
                    }
                }
                else
                {
                    var localization = node.GetComponent<Localization>();
                    if (localization == null)
                    {
                        localization = node.gameObject.AddComponent<Localization>();
                        localization.key = text.text;

                        Debug.Log($"<color=#FFA500>{node.name}</color><color=#00FF00>新增</color>了一个Localization组件，key为 <color=#00FFFF>{localization.key}</color>", node.gameObject);
                    }
                    else
                    {
                        if (localization.key != text.text)
                        {
                            localization.key = text.text;

                            Debug.Log($"<color=#FFA500>{node.name}</color><color=#FFEA04>更新</color>了一个Localization组件，key为 <color=#00FFFF>{localization.key}</color>", node.gameObject);
                        }
                    }
                }
            }
        }

        private static bool IsPrefab(GameObject go)
        {
            return PrefabUtility.GetPrefabAssetType(go) != PrefabAssetType.NotAPrefab;
        }
    }
}