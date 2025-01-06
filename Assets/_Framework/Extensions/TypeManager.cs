using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Game
{
    public class TypeManager
    {
        private static TypeManager _instance;

        public static TypeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TypeManager();
                }

                return _instance;
            }
        }

        private Dictionary<string, Type> _typeDict = new Dictionary<string, Type>();

        public void AddAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                _typeDict.TryAdd(type.FullName, type);
            }
        }

        public Type[] GetTypes()
        {
            return _typeDict.Values.ToArray();
        }
    }
}