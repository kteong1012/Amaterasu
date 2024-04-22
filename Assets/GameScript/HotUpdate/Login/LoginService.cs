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

    [GameService(GameServiceLifeSpan.Game)]
    public class LoginService : GameService
    {
        #region Fields & Properties
        public LoginChannel LoginChannel { get; private set; }

        public string PlayerId { get; private set; }

        private Dictionary<Type, PlayerData> _cachePlayerDataMap = new();

        private SceneService _sceneService;

        private EventGroup _eventGroup;
        #endregion

        #region Life Cycle
        protected override UniTask OnInit()
        {
            return base.OnInit();
        }

        protected override void OnRelease()
        {
            _cachePlayerDataMap.Clear();
            base.OnRelease();
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
            await GameEntry.Ins.StartServices(GameServiceLifeSpan.Login);

            // 发送登录事件
            GameEntryEventsDefine.LoginSuccess.SendEventMessage(LoginChannel, playerId);

            await _sceneService.ChangeToHomeScene();
        }

        public void Logout()
        {
            // 发送登出事件
            GameEntryEventsDefine.LogoutSuccess.SendEventMessage(LoginChannel, PlayerId);

            // 关闭LoginService
            GameEntry.Ins.StopServices(GameServiceLifeSpan.Login);

            // 返回登录界面
            _sceneService.ChangeToLoginScene().Forget();
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion
    }
}