using Cysharp.Threading.Tasks;

namespace Game
{
    public abstract class GameStateBase
    {
        public abstract GameServiceDomain Domain { get; }
        public abstract UniTask Enter();
        public abstract UniTask Exit();

        public GameStateBase NextState { get; protected set; }
    }
}