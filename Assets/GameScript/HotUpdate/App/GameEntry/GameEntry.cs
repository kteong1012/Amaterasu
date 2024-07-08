using UnityEngine;
using UniFramework.Event;
using Game.Log;
using YooAsset;
using Cysharp.Threading.Tasks;

namespace Game
{
    public class GameEntry : MonoBehaviour
    {
        #region Constants
        private const int TARGET_FRAME_RATE = 60;
        #endregion

        #region Fields & Properties
        public static GameEntry Ins { get; private set; }

        private readonly EventGroup _eventGroup = new EventGroup();
        #endregion

        private async void Awake()
        {
            Ins = this;
            Application.targetFrameRate = TARGET_FRAME_RATE;
            Application.runInBackground = true;
            DontDestroyOnLoad(this.gameObject);

            // 添加事件监听
            AddEventListeners();

            // 关闭更新窗口
            PatchEventDefine.ClosePatchWindow.SendEventMessage();

            // 启动Game服务
            await SSS.StartServices( GameServiceDomain.Game);

            // 进入登录场景
            SSS.SceneService.ChangeToLoginScene().Forget();

            // 打开登录界面
            UIService.OpenPanel<UILoginPanel>();
        }

        private void AddEventListeners()
        {
        }

        private void Update()
        {
            SSS.Update();
        }

        private void OnDestroy()
        {
            _eventGroup?.RemoveAllListener();
            GameLog.ClearLogger();
        }
        private void OnApplicationQuit()
        {
            SSS.StopAll();
        }
    }
}