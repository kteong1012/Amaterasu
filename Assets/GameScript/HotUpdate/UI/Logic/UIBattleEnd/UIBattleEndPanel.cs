using System;
using YIUIBind;
using YIUIFramework;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Log;

namespace Game.UI.UIBattleEnd
{
    /// <summary>
    /// Author  YIUI
    /// Date    2024.4.23
    /// </summary>
    public sealed partial class UIBattleEndPanel : UIBattleEndPanelBase
    {
        #region 生命周期

        protected override void Initialize()
        {
            u_ComRestartButton.onClick.AddListener(OnClickRestartButton);
            u_ComBackToHomeButton.onClick.AddListener(OnClickBackToHomeSceneButton);
        }

        private void OnClickBackToHomeSceneButton()
        {
            Close();
            var sceneService = GameEntry.Ins.GetService<SceneService>();
            sceneService.ChangeToHomeScene().Forget();
        }

        private void OnClickRestartButton()
        {
            var battleRoomService = GameEntry.Ins.GetService<BattleRoomService>();
            battleRoomService.CreateDemo();
            Close();
        }

        protected override void Start()
        {
            GameLog.Debug($"UIBattleEndPanel Start");
        }

        protected override void OnEnable()
        {
            GameLog.Debug($"UIBattleEndPanel OnEnable");
        }

        protected override void OnDisable()
        {
            GameLog.Debug($"UIBattleEndPanel OnDisable");
        }

        protected override void OnDestroy()
        {
            GameLog.Debug($"UIBattleEndPanel OnDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            GameLog.Debug($"UIBattleEndPanel OnOpen");
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }

        public void SetWinnerCamp(UnitCamp winnerCamp)
        {
            if (winnerCamp == UnitCamp.Player)
            {
                u_ComResultTextTextMeshProUGUI.text = "WIN";
            }
            else
            {
                u_ComResultTextTextMeshProUGUI.text = "LOSE";
            }
        }

        #endregion

        #region Event开始


        #endregion Event结束

    }
}