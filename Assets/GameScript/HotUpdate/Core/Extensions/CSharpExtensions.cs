using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public static class CSharpExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static bool TryAdd<T>(this List<T> list, T item)
        {
            if (list.Contains(item))
            {
                return false;
            }
            list.Add(item);
            return true;
        }

        public static bool TryRemove<T>(this List<T> list, T item)
        {
            if (list.Contains(item))
            {
                list.Remove(item);
                return true;
            }
            return false;
        }

        public static List<T> CreateList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static T[] CreateArray<T>(this T item)
        {
            return new[] { item };
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static HashSet<T> CreateHashSet<T>(this T item)
        {
            return new HashSet<T>() { item };
        }

        public static bool IsNullOrEmpty<T>(this HashSet<T> hashSet)
        {
            return hashSet == null || hashSet.Count == 0;
        }

        public static string Format(this string str, params object[] args)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return string.Format(str, args);
        }

        // value = Max(0, value)
        public static int ReLU(this int value)
        {
            return value < 0 ? 0 : value;
        }

        // 规则：向下取整
        public static int Multiply(this int value, float multiplier)
        {
            return Mathf.FloorToInt(value * multiplier);
        }

        // 规则：先加减后乘除, 向下取整
        public static int AddAndMultiply(this int value, int add, float multiplier)
        {
            return Mathf.FloorToInt((value + add) * multiplier);
        }
    }
}