using System;
using YIUIBind;
using YIUIFramework;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.UI.UILogin
{
    /// <summary>
    /// Author  YIUI
    /// Date    2024.4.14
    /// </summary>
    public sealed partial class UILoginPanel:UILoginPanelBase
    {
    
        #region 生命周期
        
        protected override void Initialize()
        {
            u_ComBtn_EnterGame.onClick.AddListener(() =>
            {
                Debug.Log("点击了 u_ComBtn_EnterGame");
            });
        }

        protected override void Start()
        {
            Debug.Log($"UILoginPanel Start");
        }

        protected override void OnEnable()
        {
            Debug.Log($"UILoginPanel OnEnable");
        }

        protected override void OnDisable()
        {
            Debug.Log($"UILoginPanel OnDisable");
        }

        protected override void OnDestroy()
        {
            Debug.Log($"UILoginPanel OnDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"UILoginPanel OnOpen");
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }
        
        #endregion

        #region Event开始


        #endregion Event结束

    }
}