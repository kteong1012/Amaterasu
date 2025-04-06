using Cysharp.Threading.Tasks;
using UniFramework.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UIHomePanel")]
    public partial class UIHomePanel : UI2DPanel
    {
        public override UI2DPanelLayer Layer => UI2DPanelLayer.Normal;

        public override UI2DPanelOptions Options => UI2DPanelOptions.None;

        public Button btnPlayerData;
        public Button btnContinuePlayerGame;
        public Button btnStartNewGame;
        public Button btnSwitchLanguage;
        public Button btnQuitGame;

        protected override void OnCreate()
        {
            btnPlayerData.onClick.AddListener(OnPlayerData);
            btnContinuePlayerGame.onClick.AddListener(OnContinuePlayerGame);
            btnStartNewGame.onClick.AddListener(OnStartNewGame);
            btnSwitchLanguage.onClick.AddListener(OnSwitchLanguage);
            btnQuitGame.onClick.AddListener(OnQuitGame);
            
            btnContinuePlayerGame.gameObject.SetActive(false);
        }

        protected override void OnShow()
        {
            UpdateView();
        }

        protected override void UpdateView()
        {
            UpdateViewAsync().Forget();
        }

        private async UniTaskVoid UpdateViewAsync()
        {
            await UniTask.CompletedTask;
        }

        private void OnPlayerData()
        {
        }

        private void OnContinuePlayerGame()
        {
            EnterGame().Forget();
            Close();
        }

        private void OnStartNewGame()
        {
            EnterGame().Forget();
            Close();
        }

        private async UniTask EnterGame()
        {
            await SSS.Get<GameStateService>().ChangeState(new GameStateBattle());
        }

        private void OnSwitchLanguage()
        {
            SSS.Get<UIService>().OpenPanel<UISelectLanguagePanel>();
        }

        private void OnQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}