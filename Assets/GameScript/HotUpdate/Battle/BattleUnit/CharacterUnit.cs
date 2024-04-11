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
