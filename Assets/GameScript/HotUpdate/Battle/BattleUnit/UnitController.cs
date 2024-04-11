using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class UnitController : MonoBehaviour
    {
        #region Fields & Properties
        public int UnitId { get; set; }
        public UnitCamp Camp { get; set; }

        #endregion

        #region Life Cycle
        protected virtual void Awake()
        {
            InitComponents();
        }
        #endregion

        #region Public Methods
        public T AddComponent<T>() where T : UnitComponent
        {
            if (gameObject.GetComponent<T>() != null)
            {
                GameLog.Error($"UnitController.AddComponent: {typeof(T).Name} 已经存在，不需要重复添加");
                return null;
            }
            T component = gameObject.AddComponent<T>();
            //component.Init(this);
            return component;
        }
        #endregion

        #region Private & Protected Methods
        /// <summary>
        /// 这个是为了方便子类添加组件，子类只需要调用base.InitComponents()，减少重复代码
        /// </summary>
        protected abstract void InitComponents();
        #endregion
    }
}
