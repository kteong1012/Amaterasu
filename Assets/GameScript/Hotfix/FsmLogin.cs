namespace Game
{
    public class FsmLogin : FsmNode
    {
        public override void OnEnter()
        {
            //todo set app const

            _machine.ChangeState<FsmInitializeResourceModule>();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

        }
    }
}
