using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Game
{
    public class GameDomainDebugger : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        bool _isDebugMode = true;

        public void OnGUI()
        {
            if (GUILayout.Button("Toggle Debug Mode"))
            {
                _isDebugMode = !_isDebugMode;
            }
            if (!_isDebugMode)
            {
                return;
            }
            foreach (GameServiceDomain domain in System.Enum.GetValues(typeof(GameServiceDomain)))
            {
                GUILayout.Label(domain.ToString(), new GUIStyle("Button"));
            }
        }
    }
}
