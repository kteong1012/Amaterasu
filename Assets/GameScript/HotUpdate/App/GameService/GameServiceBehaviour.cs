using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using UnityEngine;

namespace Game
{
    public class GameServiceBehaviour : MonoBehaviour
    {
        public GameServiceLifeSpan lifeSpan = GameServiceLifeSpan.Game;

        public async UniTask StartServices()
        {
            GameLog.Info($"GameServiceBehaviour StartServices, LifeSpan: {lifeSpan}");
            await GameEntry.Ins.StartServices(lifeSpan);
        }

        public void StopServices()
        {
            GameLog.Info($"GameServiceBehaviour StopServices, LifeSpan: {lifeSpan}");
            GameEntry.Ins.StopServices(lifeSpan);
        }
    }
}
