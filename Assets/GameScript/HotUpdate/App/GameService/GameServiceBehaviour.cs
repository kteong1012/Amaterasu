using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameServiceBehaviour : MonoBehaviour
    {
        public GameServiceLifeSpan lifeSpan = GameServiceLifeSpan.Game;

        private void Awake()
        {
            GameEntry.Ins.StartServices(lifeSpan).Forget();
        }

        private void OnDestroy()
        {
            GameEntry.Ins.StopServices(lifeSpan);
        }
    }
}
