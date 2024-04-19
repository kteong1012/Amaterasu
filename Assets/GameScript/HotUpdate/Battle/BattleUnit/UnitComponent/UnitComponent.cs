using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Game
{
    public abstract class UnitComponent : MonoBehaviour
    {
        protected UnitController _controller;

        public void Init(UnitController unitController)
        {
            _controller = unitController;
            OnInit();
        }

        protected virtual void OnInit()
        {

        }

        protected virtual void OnRelease()
        {

        }

        private void OnDestroy()
        {
            OnRelease();
        }
    }
}
