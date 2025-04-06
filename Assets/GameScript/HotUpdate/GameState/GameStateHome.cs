using Cysharp.Threading.Tasks;

namespace Game
{
    public class GameStateHome : GameStateBase
    {
        public override GameServiceDomain Domain => GameServiceDomain.Account;

        public override async UniTask Enter()
        {
            await SSS.Get<SceneService>().LoadSceneAsync("scene_home");
            SSS.Get<UIService>().OpenPanel<UIHomePanel>();
        }

        public override UniTask Exit()
        {
            SSS.Get<UIService>().ClosePanel<UIHomePanel>();
            return UniTask.CompletedTask;
        }
    }
}