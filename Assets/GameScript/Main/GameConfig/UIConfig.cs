using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class UIConfig
    {
        [SerializeField]
        private float _clearInterval = 60f;
        public float ClearInterval => _clearInterval;

        [SerializeField]
        private float _expire = 60f;
        public float Expire => _expire;
    }
}
