using Cysharp.Threading.Tasks;
using Jing.TurbochargedScrollList;
using TMPro;
using UnityEngine.UI;

namespace Game
{
    public class UIGMButtonCell : ScrollListItem
    {
        public TextMeshProUGUI text;
        public Button button;


        private GMButtonData _data;

        public void SetData(GMButtonData buttonData)
        {
            _data = buttonData;

            text.text = buttonData.name;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Task().Forget();
            return;

            async UniTaskVoid Task()
            {
                switch (_data.actionType)
                {
                    case GMButtonActionType.Simple:
                        _data.onClick?.Invoke(null);
                        break;
                    case GMButtonActionType.OneParam:
                    {
                        if (_data.paramTexts == null || _data.paramTexts.Length == 0)
                        {
                            _data.paramTexts = new[] { "参数1" };
                        }

                        var (isCanceled, param) = await UIGMParamPopupPanel.GetOneNumberParamAsync(_data.paramTexts[0]).SuppressCancellationThrow();

                        if (!isCanceled)
                        {
                            _data.onClick?.Invoke(new[] { param });
                        }

                        break;
                    }
                    case GMButtonActionType.TwoParam:
                    {
                        if (_data.paramTexts == null || _data.paramTexts.Length < 2)
                        {
                            if (_data.paramTexts is { Length: 0 })
                            {
                                _data.paramTexts = new[] { "参数1", "参数2" };
                            }
                            else
                            {
                                if (_data.paramTexts != null)
                                {
                                    _data.paramTexts = new[] { _data.paramTexts[0], "参数2" };
                                }
                            }
                        }

                        if (_data.paramTexts != null)
                        {
                            var (isCanceled, (param1, param2)) = await UIGMParamPopupPanel.GetTwoNumberParamAsync(_data.paramTexts[0], _data.paramTexts[1]).SuppressCancellationThrow();
                            if (!isCanceled)
                            {
                                _data.onClick?.Invoke(new[] { param1, param2 });
                            }
                        }

                        break;
                    }
                }
            }
        }
    }
}