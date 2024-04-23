using System;
using YIUIBind;
using YIUIFramework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.UI.UIBattleEnd
{



    /// <summary>
    /// 由YIUI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class UIBattleEndPanelBase:BasePanel
    {
        public const string PkgName = "UIBattleEnd";
        public const string ResName = "UIBattleEndPanel";
        
        public override EWindowOption WindowOption => EWindowOption.BanTween;
        public override EPanelLayer Layer => EPanelLayer.Panel;
        public override EPanelOption PanelOption => EPanelOption.None;
        public override EPanelStackOption StackOption => EPanelStackOption.VisibleTween;
        public override int Priority => 0;
        public TMPro.TextMeshProUGUI u_ComResultTextTextMeshProUGUI { get; private set; }
        public UnityEngine.UI.Button u_ComRestartButton { get; private set; }
        public UnityEngine.UI.Button u_ComBackToHomeButton { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_ComResultTextTextMeshProUGUI = ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComResultTextTextMeshProUGUI");
            u_ComRestartButton = ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComRestartButton");
            u_ComBackToHomeButton = ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComBackToHomeButton");

        }

        protected sealed override void UnUIBind()
        {

        }
     
   
   
    }
}