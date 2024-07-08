using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UILoginPanel")]
    public partial class UILoginPanel : UI2DPanel
    {
        [SerializeField] private Button _loginButton;

        public override UI2DPanelLayer Layer => UI2DPanelLayer.Normal;
        public override UI2DPanelOptions Options => UI2DPanelOptions.None;

        protected override void OnCreate()
        {
            _loginButton.onClick.AddListener(OnClickBtnEnterGame);

            CreateChild<UILoginNode>();
        }

        private async void OnClickBtnEnterGame()
        {
            await SSS.LoginService.Login(LoginChannel.Local, "0");
            Close();
        }
    }
}
