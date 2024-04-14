using System;
using YIUIBind;
using YIUIFramework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.UI.UILogin
{



    /// <summary>
    /// 由YIUI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class UILoginPanelBase:BasePanel
    {
        public const string PkgName = "UILogin";
        public const string ResName = "UILoginPanel";
        
        public override EWindowOption WindowOption => EWindowOption.None;
        public override EPanelLayer Layer => EPanelLayer.Panel;
        public override EPanelOption PanelOption => EPanelOption.None;
        public override EPanelStackOption StackOption => EPanelStackOption.VisibleTween;
        public override int Priority => 0;
        public UnityEngine.UI.Button u_ComBtn_EnterGame { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_ComBtn_EnterGame = ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComBtn_EnterGame");

        }

        protected sealed override void UnUIBind()
        {

        }
     
   
   
    }
}