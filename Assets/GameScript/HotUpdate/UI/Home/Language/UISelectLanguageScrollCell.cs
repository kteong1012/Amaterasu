using Jing.TurbochargedScrollList;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UISelectLanguageScrollCell : ScrollListItem
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI _text;
        [SerializeField]
        private Button _button;

        private Language _language;
        private Action<Language> _onClick;


        private void Awake()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        public void SetData(Language language, bool interactable, Action<Language> onClick)
        {
            _language = language;
            _onClick = onClick;

            _text.text = language.ToString();
            _button.interactable = interactable;
        }

        private void OnClickButton()
        {
            _onClick?.Invoke(_language);
        }
    }
}
