using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UIMessageBoxOkCancelPanel")]
    public partial class UIMessageBoxOkCancelPanel : UIMessageBoxPanel
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI _titleText;
        [SerializeField]
        private TMPro.TextMeshProUGUI _messageText;
        [SerializeField]
        private TMPro.TextMeshProUGUI _okText;
        [SerializeField]
        private TMPro.TextMeshProUGUI _cancelText;
        [SerializeField]
        private Button _okButton;
        [SerializeField]
        private Button _cancelButton;

        protected override void OnCreate()
        {
            _okButton.onClick.AddListener(OnOkButtonClick);
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
        }

        protected override void ShowBoxView()
        {
            _titleText.text = _currentData.Title;
            _messageText.text = _currentData.Message;
            _okText.text = _currentData.OkText;
            _cancelText.text = _currentData.CancelText;
        }

        private void OnOkButtonClick()
        {
            var tcs = _currentData.TCS as UniTaskCompletionSource<OkCancelResult>;
            tcs.TrySetResult(OkCancelResult.Ok);
            Next();
        }

        private void OnCancelButtonClick()
        {
            var tcs = _currentData.TCS as UniTaskCompletionSource<OkCancelResult>;
            tcs.TrySetResult(OkCancelResult.Cancel);
            Next();
        }
    }
}
