using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Game.Log;

namespace Game
{
    public interface IState
    {
        void SetStateMachine<T>(StateMachine<T> stateMachine) where T : class, IState;
        void Enter();
        void Update();
        void Exit();
    }
    public class StateMachine<T> where T : class, IState
    {
        private T _currentState;

        private Queue<T> _stateQueue = new Queue<T>();
        public void ChangeState(T state)
        {
            _stateQueue.Enqueue(state);
        }

        private void CheckNextState()
        {
            if (!_stateQueue.TryDequeue(out var nextState))
            {
                return;
            }
            _currentState?.Exit();
            _currentState = nextState;
            _currentState.SetStateMachine(this);
            _currentState.Enter();
            
            GameLog.Debug($"进入状态:{_currentState.GetType().Name}");
        }

        public void Update()
        {
            CheckNextState();
            _currentState?.Update();
        }

        public void Dispose()
        {
            _currentState?.Exit();
            _currentState = null;
        }
    }
}
