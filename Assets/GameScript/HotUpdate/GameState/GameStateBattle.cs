using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameStateBattle : GameStateBase
    {
        public override async UniTask Enter()
        {
            var sceneName = "scene_battle";
            SSS.Get<SceneService>().SetCameraTransformConfig(sceneName, new Vector3(0, 39.4f, -4.24f), new Vector3(85f, 0, 0), 52.6f);
            await SSS.Get<SceneService>().LoadSceneAsync(sceneName);
            await SSS.StartServices(GameServiceDomain.Battle);
        }

        public override UniTask Exit()
        {
            SSS.StopServices(GameServiceDomain.Battle);
            return UniTask.CompletedTask;
        }
    }
}