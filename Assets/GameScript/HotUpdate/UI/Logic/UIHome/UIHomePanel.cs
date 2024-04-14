using System;
using YIUIBind;
using YIUIFramework;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Log;

namespace Game.UI.UIHome
{
    /// <summary>
    /// Author  YIUI
    /// Date    2024.4.14
    /// </summary>
    public sealed partial class UIHomePanel : UIHomePanelBase
    {

        #region 生命周期

        protected override void Initialize()
        {
            u_ComBtn_StartBattleButton.onClick.AddListener(OnClickBtnStartBattle);
        }

        protected override void Start()
        {
            GameLog.Debug($"UIHomePanel Start");
        }

        protected override void OnEnable()
        {
            GameLog.Debug($"UIHomePanel OnEnable");
        }

        protected override void OnDisable()
        {
            GameLog.Debug($"UIHomePanel OnDisable");
        }

        protected override void OnDestroy()
        {
            GameLog.Debug($"UIHomePanel OnDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            GameLog.Debug($"UIHomePanel OnOpen");
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }

        #endregion

        #region Event开始

        private async void OnClickBtnStartBattle()
        {
            var sceneService = GameEntry.Ins.GetService<SceneService>();
            await sceneService.ChangeToBattleScene();
            Close();
        }

        #endregion Event结束

    }
}