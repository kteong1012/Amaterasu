using Cysharp.Threading.Tasks;
using System;

namespace Game
{
    public class GameServiceAttribute : Attribute
    {
        public GameServiceLifeSpan LifeSpan { get; }

        public GameServiceAttribute(GameServiceLifeSpan lifeSpan)
        {
            LifeSpan = lifeSpan;
        }
    }
    public abstract class GameService
    {
        public virtual UniTask Init()
        {
            return UniTask.CompletedTask;
        }

        public virtual void Update()
        {
        }

        public virtual void Release()
        {
        }
    }
}
