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

        private GameServiceManager _serviceManager;
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

            // 初始化服务管理器
            InitServiceManager();

            // 启动Game服务
            await _serviceManager.StartServices(GameServiceDomain.Game);

            // 进入登录场景
            var sceneService = GetService<SceneService>();
            sceneService.ChangeToLoginScene().Forget();

            // 打开登录界面
            UIService.OpenPanel<UILoginPanel>();
        }

        private void AddEventListeners()
        {
        }

        private void InitServiceManager()
        {
            _serviceManager = new GameServiceManager();
        }

        public async UniTask StartServices(GameServiceDomain domain)
        {
            await _serviceManager.StartServices(domain);
        }

        public void StopServices(GameServiceDomain domain)
        {
            _serviceManager.StopServices(domain);
        }

        private void Update()
        {
            _serviceManager?.Update();
        }

        private void OnDestroy()
        {
            _eventGroup?.RemoveAllListener();
            YooAssets.Destroy();
            GameLog.ClearLogger();
        }
        private void OnApplicationQuit()
        {
            _serviceManager?.Release();
        }

        public T GetService<T>() where T : GameService
        {
            return _serviceManager.GetService<T>();
        }
    }
}