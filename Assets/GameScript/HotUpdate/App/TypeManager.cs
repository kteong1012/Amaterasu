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

        private Assembly _hotUpdateAssembly;
        private Type[] _hotUpdateTypes;

        public TypeManager()
        {
            _hotUpdateAssembly = GetType().Assembly;
            _hotUpdateTypes = _hotUpdateAssembly.GetTypes();
        }

        public IEnumerable<Type> GetTypes()
        {
            return _hotUpdateTypes;
        }
    }
}
