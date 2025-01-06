using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Game
{
    public class HotUpdateAssemblyManager
    {
        private static HotUpdateAssemblyManager _instance;
        public static HotUpdateAssemblyManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HotUpdateAssemblyManager();
                }
                return _instance;
            }
        }

        private Assembly _hotUpdateAssembly;
        private Type[] _hotUpdateTypes;

        public HotUpdateAssemblyManager()
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
