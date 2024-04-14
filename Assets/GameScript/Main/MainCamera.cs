using UnityEngine;

namespace Game
{
    public class MainCamera : MonoBehaviour
    {
        public static MainCamera Instance { get; private set; }
        public Camera Camera;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Camera = GetComponent<Camera>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
