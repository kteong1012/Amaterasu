using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Collections.Generic;
using UniFramework.Event;

namespace Game
{
    public enum LoginState
    {

    }

    public enum LoginChannel
    {
        Local = 0,
        Google = 1,
        Facebook = 2,
        Apple = 3,
        WeChat = 4,
        QQ = 5
    }

    [GameService(GameServiceDomain.Game)]
    public partial class LoginService : GameService
    {
        #region Fields & Properties
        public LoginChannel LoginChannel { get; private set; }

        public string PlayerId { get; private set; }

        private Dictionary<Type, PlayerData> _cachePlayerDataMap = new();
        #endregion

        #region Life Cycle
        protected override UniTask Awake()
        {
            return base.Awake();
        }

        protected override void OnDestroy()
        {
            _cachePlayerDataMap.Clear();
            base.OnDestroy();
        }
        #endregion

        #region Public Methods
        public async UniTask Login(LoginChannel loginChannel, string playerId)
        {
            // 给LoginChannel和PlayerId赋值
            LoginChannel = loginChannel;
            PlayerId = playerId;
            GameLog.Info($"LoginService Login, LoginChannel: {LoginChannel}, PlayerId: {PlayerId}");

            // 开启LoginService
            await SSS.StartServices(GameServiceDomain.Login);

            // 发送登录事件
            GameEntryEventsDefine.LoginSuccess.SendEventMessage(LoginChannel, playerId);

            await SSS.SceneService.ChangeToHomeScene();
        }

        public void Logout()
        {
            // 发送登出事件
            GameEntryEventsDefine.LogoutSuccess.SendEventMessage(LoginChannel, PlayerId);

            // 关闭LoginService
            SSS.StopServices(GameServiceDomain.Login);

            // 返回登录界面
            SSS.SceneService.ChangeToLoginScene().Forget();
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion
    }
}