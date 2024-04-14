using Cysharp.Threading.Tasks;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

#pragma warning disable LOG003 // 禁止使用YooAssets.LoadSceneAsync
namespace Game
{
    [GameService(GameServiceLifeSpan.Game)]
    public class SceneService : GameService
    {
        private EventGroup _eventGroup = new EventGroup();
        public override async UniTask Init()
        {
            await base.Init();
        }

        public void ChangeToBattleScene()
        {
            YooAssets.LoadSceneAsync("scene_battle");
            MainCamera.Instance.Camera.transform.position = new Vector3(0, 20, -2);
            MainCamera.Instance.Camera.transform.rotation = Quaternion.Euler(85, 0, 0);
        }

        public void ChangeToLoginScene()
        {
            YooAssets.LoadSceneAsync("scene_login");
            MainCamera.Instance.Camera.transform.position = Vector3.zero;
            MainCamera.Instance.Camera.transform.rotation = Quaternion.identity;
        }
    }
}
#pragma warning restore LOG003 // 禁止使用YooAssets.LoadSceneAsync