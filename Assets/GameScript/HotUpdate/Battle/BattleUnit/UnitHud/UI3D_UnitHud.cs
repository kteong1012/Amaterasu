using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UI3D_UnitHud : MonoBehaviour
    {
        public Image HpBar;
        public TextMeshProUGUI text;

        public void SetHP(NumberX1000 hp, NumberX1000 maxHp)
        {
            HpBar.fillAmount = hp.ToFloat() / maxHp.ToFloat();
            text.text = $"{hp.Ceil().ToIntegerString()}/{maxHp.Ceil().ToIntegerString()}";
        }
    }
}