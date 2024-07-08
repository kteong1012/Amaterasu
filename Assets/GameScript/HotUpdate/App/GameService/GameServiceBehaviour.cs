using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using UnityEngine;

namespace Game
{
    public class GameServiceBehaviour : MonoBehaviour
    {
        public GameServiceDomain domain = GameServiceDomain.Game;

        public async UniTask StartServices()
        {
            GameLog.Info($"GameServiceBehaviour StartServices, Domain: {domain}");
            await SSS.StartServices(domain);
        }

        public void StopServices()
        {
            GameLog.Info($"GameServiceBehaviour StopServices, Domain: {domain}");
            SSS.StopServices(domain);
        }
    }
}
