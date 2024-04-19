using System;
using UnityEngine;

namespace Game
{
    public class CharacterUnit : UnitController
    {
        protected override void AddComponents()
        {
            AddUnitComponent<UnitModelComponent>();
            AddUnitComponent<UnitAttributesComponent>();
            AddUnitComponent<UnitNavigationComponent>();
        }
    }
}
