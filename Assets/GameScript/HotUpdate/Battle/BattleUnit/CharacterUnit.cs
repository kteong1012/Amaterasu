using System;
using UnityEngine;

namespace Game
{
    public class CharacterUnit : UnitController
    {
        protected override void InitComponents()
        {
            AddComponent<UnitGameObjectComponent>();
            AddComponent<UnitAnimationComponent>();
            AddComponent<UnitMoveComponent>();
            gameObject.AddComponent<UnitGameObjectComponent>();
            int a = 1;
            if (a > 1)
                Debug.Log("a>1");
        }
        void Update()
        {
            try
            {
                DoSomethingInteresting();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }

        private void DoSomethingInteresting()
        {
            throw new System.NotImplementedException();
        }
    }
}
