using Cysharp.Threading.Tasks;

namespace Game
{
    public class GameStateMachine
    {
        private GameStateBase _currentState;

        public async UniTask ChangeState<T>(T state) where T : GameStateBase
        {
            if (_currentState != null)
            {
                await _currentState.Exit();
            }
            var newState = state;
            _currentState = newState;
            await _currentState.Enter();
            if (_currentState.NextState != null)
            {
                await ChangeState(_currentState.NextState);
            }
        }
    }
}