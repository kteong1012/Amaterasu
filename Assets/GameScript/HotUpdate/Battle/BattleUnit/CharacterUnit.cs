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
            AddUnitComponent<UnitAIComponent>();
        }

        protected override void OnInit()
        {
            var aiComponent = GetUnitComponent<UnitAIComponent>();
            aiComponent.SetAI<UnitAI_XiaoMing>();
        }
    }
}
