using Cysharp.Threading.Tasks;

namespace Game
{
    public class GameStateLogin : GameStateBase
    {
        public override GameServiceDomain Domain => GameServiceDomain.Game;

        public override async UniTask Enter()
        {
            await SSS.Get<SceneService>().LoadSceneAsync("scene_login");
            SSS.Get<UIService>().OpenPanel<UILoginPanel>();
        }

        public override UniTask Exit()
        {
            SSS.Get<UIService>().ClosePanel<UILoginPanel>();
            return UniTask.CompletedTask;
        }
    }
}