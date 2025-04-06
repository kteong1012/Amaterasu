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
            var domain = newState.Domain;
            await SSS.SetCurrentDomain(domain);
            await _currentState.Enter();
        }
        public async UniTask RestartState()
        {
            if (_currentState != null)
            {
                await _currentState.Exit();
            }
            var domain = _currentState.Domain;
            await SSS.SetCurrentDomain(domain, true);
            await _currentState.Enter();
        }
    }
}