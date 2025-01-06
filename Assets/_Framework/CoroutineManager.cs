using UnityEngine;

namespace Game
{
    public class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager _instance;

        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("CoroutineManager");
                    _instance = go.AddComponent<CoroutineManager>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }
    }
}
