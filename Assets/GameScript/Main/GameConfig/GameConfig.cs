using Game.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class GameConfig : ScriptableObject
    {
        private static GameConfig _instance;
        public static GameConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<GameConfig>("GameConfig");
                }

#if UNITY_EDITOR
                if(_instance == null)
                {
                    _instance = CreateInstance<GameConfig>();
                    // save to Assets/Resources
                    UnityEditor.AssetDatabase.CreateAsset(_instance, "Assets/Resources/GameConfig.asset");
                    UnityEditor.AssetDatabase.SaveAssets();
                }
#endif
                return _instance;
            }
        }


        [Header("App Settings")]
        [Tooltip("App相关配置")]
        public string Version = "1.0.0";
        public LogLevel LogLevel = LogLevel.Debug;

        [Tooltip("资源热更配置")]
        public HotupdateConfig HotupdateConfig;

        [Tooltip("UI相关配置")]
        public UIConfig UIConfig;

    }
}
