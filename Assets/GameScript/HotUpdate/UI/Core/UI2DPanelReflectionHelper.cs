using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
    public static class UI2DPanelReflectionHelper
    {
        private static Type _ui2DPanelBaseType = typeof(UI2DPanel);

        public static Dictionary<string, UI2DInfo> PanelInfos { get; private set; }

        static UI2DPanelReflectionHelper()
        {
            PanelInfos = new Dictionary<string, UI2DInfo>();
            LoadPanelInfos();
        }

        private static void LoadPanelInfos()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(_ui2DPanelBaseType) && !type.IsAbstract)
                    {
                        var attribute = type.GetCustomAttribute<UI2DAttribute>();
                        if (attribute != null)
                        {
                            PanelInfos[type.FullName] = attribute.info;
                        }
                    }
                }
            }
        }

        public static bool TryGetPanelInfo(string panelName, out UI2DInfo panelInfo)
        {
            return PanelInfos.TryGetValue(panelName, out panelInfo);
        }

        public static string GetPanelClassName<T>() where T : UI2DPanel
        {
            var type = typeof(T);
            return type.FullName;
        }
    }
}
