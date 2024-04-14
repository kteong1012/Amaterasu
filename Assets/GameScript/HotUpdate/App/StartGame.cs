using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class StartGame
    {
        public static void Start()
        {
            var go = new GameObject("GameEntry");
            go.AddComponent<GameEntry>();
        }
    }
}
