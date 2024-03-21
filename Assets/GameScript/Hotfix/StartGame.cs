using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StartGame
    {
        public static void Start()
        {
            DoStart().Forget();
        }

        private static async UniTask DoStart()
        {
            await G.Ins.RegisterGameComponent<ResourceComponent>();
            await G.Ins.RegisterGameComponent<ConfigComponent>();
        }
    }
}
