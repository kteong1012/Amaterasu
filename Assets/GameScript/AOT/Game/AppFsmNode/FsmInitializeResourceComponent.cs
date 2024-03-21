namespace Game
{
    public class FsmInitializeResourceComponent : FsmNode
    {

        public override async void OnEnter()
        {
            var resourceComponent = await G.Ins.RegisterGameComponent<ResourceComponent>();
            await resourceComponent.UpdatePatch();

            _machine.ChangeState<FsmLoadDll>();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

        }
    }
}
