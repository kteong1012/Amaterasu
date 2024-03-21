using System.Collections;
using System.Collections.Generic;
using UniFramework.Machine;
using UnityEngine;

namespace Game
{
    public class FsmInitializeAppConst : FsmNode
    {

        public override void OnEnter()
        {
            //todo set app const

            _machine.ChangeState<FsmInitializeResourceComponent>();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

        }
    }
}
