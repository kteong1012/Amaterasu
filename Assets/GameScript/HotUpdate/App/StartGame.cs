using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class StartGame
    {
        public static async void Start()
        {
            var handle = SceneManager.LoadSceneAsync("Lobby");
            await handle.ToUniTask();

            var go = new GameObject("G");
            go.AddComponent<GameEntry>();
        }
    }
}
