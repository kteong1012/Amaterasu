using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Collections.Generic;
using UniFramework.Event;
using UnityEngine;

namespace Game
{
    public static class AccountConstants
    {
        public const string LastLoginPlayerId = "StrKey_LoginPlayerId";
    }
    [GameService(GameServiceDomain.Game)]
    public class LoginService : GameService
    {
        #region Fields & Properties
        public LoginChannel LoginChannel { get; private set; }

        public string PlayerId { get; private set; }
        #endregion

        #region Life Cycle
        protected override UniTask Awake()
        {
            return base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion

        #region Public Methods
        public async UniTask Login(LoginChannel loginChannel, string playerId)
        {
            PlayerPrefs.SetString(AccountConstants.LastLoginPlayerId, playerId);

            // 给LoginChannel和PlayerId赋值
            LoginChannel = loginChannel;
            PlayerId = playerId;
            GameLog.Info($"AccountService Login, LoginChannel: {LoginChannel}, PlayerId: {PlayerId}");

            // 进入Account作用域
            await SSS.SetCurrentDomain(GameServiceDomain.Account);

            // 发送登录事件
            GameEntryEventsDefine.LoginSuccess.SendEventMessage(LoginChannel, playerId);

            // 切换至Home状态
            SSS.Get<GameStateService>().ChangeState(new GameStateHome()).Forget();
        }

        public void Logout()
        {
            // 发送登出事件
            GameEntryEventsDefine.LogoutSuccess.SendEventMessage(LoginChannel, PlayerId);

            // 切换至登录场景
            SSS.Get<GameStateService>().ChangeState(new GameStateLogin()).Forget();
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion
    }
}