using UnityEngine;

namespace Game
{
    public class MainCamera : MonoBehaviour
    {
        public static MainCamera Instance { get; private set; }
        public Camera Camera { get; private set; }
        public Canvas worldCanvas;
        public Camera worldCanvasCamera;
        public Transform worldCanvasRoot;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Camera = GetComponent<Camera>();
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(worldCanvas.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
