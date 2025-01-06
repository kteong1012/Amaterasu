using TMPro;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Localization : MonoBehaviour
    {
        public static ILocalizationHandler LocalizationHandler { get; set; }

        public string key;
        private TextMeshProUGUI _text;
        private TextMeshProUGUI _Text
        {
            get
            {
                if (_text == null)
                {
                    _text = GetComponent<TextMeshProUGUI>();
                }
                return _text;
            }
        }

        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            _Text.text = LocalizationHandler.GetText(key);
        }
    }
}
