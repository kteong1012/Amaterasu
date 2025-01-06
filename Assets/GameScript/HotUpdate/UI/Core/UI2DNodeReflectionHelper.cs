using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game
{
    public static class UI2DNodeReflectionHelper
    {
        private static Type _ui2DNodeBaseType = typeof(UI2DNode);

        public static Dictionary<string, UI2DInfo> NodeInfos { get; private set; }

        static UI2DNodeReflectionHelper()
        {
            NodeInfos = new Dictionary<string, UI2DInfo>();
            LoadNodeInfos();
        }

        private static void LoadNodeInfos()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(_ui2DNodeBaseType) && !type.IsAbstract)
                    {
                        var attribute = type.GetCustomAttribute<UI2DAttribute>();
                        if (attribute != null)
                        {
                            NodeInfos[type.FullName] = attribute.info;
                        }
                    }
                }
            }
        }

        public static bool TryGetNodeInfo(string nodeName, out UI2DInfo nodeInfo)
        {
            return NodeInfos.TryGetValue(nodeName, out nodeInfo);
        }

        public static string GetNodeClassName<T>() where T : UI2DNode
        {
            var type = typeof(T);
            return type.FullName;
        }
    }
}
