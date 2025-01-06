using Cysharp.Threading.Tasks;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UILoginPanel")]
    public partial class UILoginPanel : UI2DPanel
    {
        [SerializeField]
        private Button _loginButton;
        [SerializeField]
        private TMP_InputField _inputAccountName;

        public override UI2DPanelLayer Layer => UI2DPanelLayer.Normal;
        public override UI2DPanelOptions Options => UI2DPanelOptions.None;

        protected override void OnCreate()
        {
            _loginButton.onClick.AddListener(OnClickBtnEnterGame);
        }

        protected override void OnShow()
        {
            UpdateView();
        }

        protected override void UpdateView()
        {
            base.UpdateView();
            var lastLoginPlayerId = PlayerPrefs.GetString(AccountConstants.LastLoginPlayerId, null);
            if (!lastLoginPlayerId.IsNullOrEmpty())
            {
                _inputAccountName.text = lastLoginPlayerId;
            }
        }

        private void OnClickBtnEnterGame()
        {
            AsyncMethod().Forget();
            return;
            
            async UniTaskVoid AsyncMethod()
            {
                if (_inputAccountName.text.IsNullOrEmpty())
                {
                    await MessageBox.ShowOk("请输入账号".Translate(), "登录失败".Translate());
                    return;
                }
                if (StringExtensions.IsInvalidFileName(_inputAccountName.text))
                {
                    await MessageBox.ShowOk("账号包含非法字符".Translate(), "登录失败".Translate());
                    return;
                }

                await SSS.Get<LoginService>().Login(LoginChannel.Local, _inputAccountName.text);
            }
        }
    }
}
