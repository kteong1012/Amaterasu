using System;
using UnityEngine;

namespace Game
{
    public class CharacterUnit : UnitController
    {
        protected override void AddComponents()
        {
            AddComponent<UnitNavigationComponent>();
        }
    }
}
