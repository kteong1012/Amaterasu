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

        private void Awake()
        {
            AsyncMethod().Forget();
            return;

            async UniTaskVoid AsyncMethod()
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
                await SSS.SetCurrentDomain(GameServiceDomain.Game);

                // 预加载Loading界面
                PreloadLoadingPanel();

                // 切换至登录状态
                SSS.Get<GameStateService>().ChangeState(new GameStateLogin()).Forget();
            }
        }

        private void AddEventListeners()
        {
        }

        private void PreloadLoadingPanel()
        {
            SSS.Get<UIService>().OpenPanel<UILoadingPanel>();
            SSS.Get<UIService>().ClosePanel<UILoadingPanel>();
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