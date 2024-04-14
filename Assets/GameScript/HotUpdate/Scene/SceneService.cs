using UnityEngine;
using YooAsset;

namespace Game
{
    [GameService(GameServiceLifeSpan.Game)]
    public class SceneService : GameService
    {
        public void ChangeToBattleScene()
        {
            YooAssets.LoadSceneAsync("scene_battle");
            MainCamera.Instance.Camera.transform.position = new Vector3(0, 20, -2);
            MainCamera.Instance.Camera.transform.rotation = Quaternion.Euler(85, 0, 0);
        }
    }
}