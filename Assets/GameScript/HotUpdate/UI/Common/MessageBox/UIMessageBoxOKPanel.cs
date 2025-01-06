using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UIMessageBoxOKPanel")]
    public partial class UIMessageBoxOKPanel : UIMessageBoxPanel
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI _titleText;
        [SerializeField]
        private TMPro.TextMeshProUGUI _messageText;
        [SerializeField]
        private TMPro.TextMeshProUGUI _okText;
        [SerializeField]
        private Button _okButton;

        protected override void OnCreate()
        {
            _okButton.onClick.AddListener(OnOkButtonClick);
        }

        protected override void ShowBoxView()
        {
            _titleText.text = _currentData.Title;
            _messageText.text = _currentData.Message;
            _okText.text = _currentData.OkText;
        }

        private void OnOkButtonClick()
        {
            var tcs = _currentData.TCS as UniTaskCompletionSource;
            tcs.TrySetResult();
            Next();
        }
    }
}
