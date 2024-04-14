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
    }
}
