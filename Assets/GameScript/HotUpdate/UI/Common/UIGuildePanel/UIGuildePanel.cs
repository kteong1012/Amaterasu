using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UIGuildePanel")]
    public class UIGuildePanel : UI2DPanel
    {
        private List<Button> _buttons;

        private void OnClick()
        {
            foreach (var button in _buttons)
            {
                button.onClick.RemoveListener(OnClick);
            }

            Close();
        }
    }
}
