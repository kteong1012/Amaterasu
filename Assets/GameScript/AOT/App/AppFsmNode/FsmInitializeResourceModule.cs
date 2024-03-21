namespace Game
{
    public class FsmInitializeResourceModule : FsmNode
    {

        public override async void OnEnter()
        {
            await G.Ins.RegisterGameModule<ResourceModule>();

            _machine.ChangeState<FsmGameLoop>();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

        }
    }
}
