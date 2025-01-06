using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class UnityEngineExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : UnityEngine.Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }

        public static Component GetOrAddComponent(this GameObject gameObject, System.Type type)
        {
            Component component = gameObject.GetComponent(type);
            if (component == null)
            {
                component = gameObject.AddComponent(type);
            }
            return component;
        }

        public static string ToHex(this Color color)
        {
            return $"#{(int)(color.r * 255):X2}{(int)(color.g * 255):X2}{(int)(color.b * 255):X2}";
        }

        public static string WrapColor(this string text, Color color)
        {
            return $"<color={color.ToHex()}>{text}</color>";
        }

        public static void TryDestroy(this UnityEngine.Object obj)
        {
            if (obj != null)
            {
                UnityEngine.Object.Destroy(obj);
            }
        }
        public static void TryDestroyImmediate(this UnityEngine.Object obj)
        {
            if (obj != null)
            {
                UnityEngine.Object.DestroyImmediate(obj);
            }
        }
        public static void SetLayerRecursively(this GameObject gob, int layer)
        {
            gob.layer = layer;
            foreach (Transform child in gob.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }
    }
}