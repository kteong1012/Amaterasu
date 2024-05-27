using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game
{
    public class MainCamera : MonoBehaviour
    {
        public static MainCamera Instance { get; private set; }
        public Camera Camera { get; private set; }
        public Canvas worldCanvas;
        public Camera worldCanvasCamera;
        public Transform worldCanvasRoot;

        private UniversalAdditionalCameraData _universalAdditionalCameraData;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Camera = GetComponent<Camera>();
                _universalAdditionalCameraData = GetComponent<UniversalAdditionalCameraData>();
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(worldCanvas.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddCameraStack(Camera camera)
        {
            _universalAdditionalCameraData.cameraStack.Add(camera);
        }
    }
}
