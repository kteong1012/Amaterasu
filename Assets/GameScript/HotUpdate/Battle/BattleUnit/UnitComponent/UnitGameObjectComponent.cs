using UnityEngine;

namespace Game
{
    internal class UnitGameObjectComponent : UnitComponent
    {
        public GameObject GameObject { get; private set; }
        public void Init(UnitController battleUnit)
        {
            GameObject = new GameObject();
        }

        public void Release()
        {
            Object.Destroy(GameObject);
        }

        public void Update()
        {
        }
    }
}
