using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Camera))]
    public class CameraZoom : MonoBehaviour
    {
        private Camera _camera;
        public float zoomSpeed = 10f;
        public float minFov = 15f;
        public float maxFov = 90f;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            float fov = _camera.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            _camera.fieldOfView = fov;
        }
    }
}
