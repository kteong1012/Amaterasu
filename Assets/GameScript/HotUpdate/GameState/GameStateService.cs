using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    [GameService(GameServiceDomain.Game)]
    public partial class GameStateService : GameService
    {
        private GameStateMachine _gameStateMachine;

        protected override UniTask Awake()
        {
            base.Awake();
            _gameStateMachine = new GameStateMachine();
            return UniTask.CompletedTask;
        }

        public async UniTask ChangeState<T>(T state) where T : GameStateBase
        {
            await _gameStateMachine.ChangeState(state);
        }
    }
}