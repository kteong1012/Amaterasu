using UniFramework.Machine;

namespace Game
{
    public abstract class FsmNode : IStateNode
    {
        protected StateMachine _machine;

        public void OnCreate(StateMachine machine)
        {
            _machine = machine;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
    }
}
