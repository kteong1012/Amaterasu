using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Count() == 0;
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Count() > 0;
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
    }
}