using Cysharp.Threading.Tasks;

namespace Game
{
    public abstract class GameStateBase
    {
        public abstract UniTask Enter();
        public abstract UniTask Exit();

        public GameStateBase NextState { get; protected set; }
    }
}