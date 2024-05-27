using Cysharp.Threading.Tasks;
using Game.Log;
using System.Collections.Generic;

namespace Game
{
    public partial class UIService
    {
        private Dictionary<string,UI2DPanel> _activePanels = new Dictionary<string, UI2DPanel>();
    }
}
