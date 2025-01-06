using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UIGMParamPopupPanel")]
    public partial class UIGMParamPopupPanel : UI2DPanel
    {
        public TextMeshProUGUI text1;
        public TMP_InputField inputField1;
        public TextMeshProUGUI text2;
        public TMP_InputField inputField2;
        public Button confirmButton;

        private UniTaskCompletionSource<string[]> _tcs;

        protected override void OnCreate()
        {
            confirmButton.onClick.AddListener(() =>
            {
                if (_tcs == null)
                {
                    return;
                }
                var results = new string[2];
                results[0] = inputField1.text;
                results[1] = inputField2.text;

                _tcs.TrySetResult(results);

                Close();
            });
        }

        private async UniTask<string> GetOneNumberParam(string text)
        {
            ResetPanel();

            text1.gameObject.SetActive(true);
            inputField1.gameObject.SetActive(true);
            this.text1.text = text;

            var results = await _tcs.Task;

            return results[0];
        }

        private async UniTask<(string, string)> GetTwoNumberParam(string text1, string text2)
        {
            ResetPanel();

            this.text1.gameObject.SetActive(true);
            this.text2.gameObject.SetActive(true);

            inputField1.gameObject.SetActive(true);
            inputField2.gameObject.SetActive(true);

            this.text1.text = text1;
            this.text2.text = text2;

            var results = await _tcs.Task;

            return (results[0], results[1]);
        }

        private void ResetPanel()
        {
            text1.gameObject.SetActive(false);
            text2.gameObject.SetActive(false);

            inputField1.gameObject.SetActive(false);
            inputField2.gameObject.SetActive(false);

            inputField1.text = "";
            inputField2.text = "";

            _tcs?.TrySetCanceled();
            _tcs = new UniTaskCompletionSource<string[]>();
        }

        public static UniTask<string> GetOneNumberParamAsync(string text)
        {
            var panel = SSS.Get<UIService>().OpenPanel<UIGMParamPopupPanel>();
            return panel.GetOneNumberParam(text);
        }

        public static UniTask<(string, string)> GetTwoNumberParamAsync(string text1, string text2)
        {
            var panel = SSS.Get<UIService>().OpenPanel<UIGMParamPopupPanel>();
            return panel.GetTwoNumberParam(text1, text2);
        }
    }
}
