using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniFramework.Machine;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class GameLoop
    {
        enum GameState
        {
            None,
            Start,
            Update,
            Release
        }

        private static GameState _state = GameState.None;

        private static StateMachine _machine;

        public static void Start()
        {
            DoStart().Forget(); 
        }

        private static async UniTask DoStart()
        {
            _state = GameState.Start;

            await G.Ins.RegisterGameModule<ConfigModule>();

            // 切换到主页面场景
            await YooAssets.LoadSceneAsync("scene_home");

            _state = GameState.Update;
        }

        public static void Update()
        {
            if(_state != GameState.Update)
            {
                return;
            }

        }

        public static void Release()
        {

        }
    }
}
