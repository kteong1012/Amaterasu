using Game.Cfg;
using Game.Log;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class UnitController : MonoBehaviour
    {
        #region Fields & Properties
        /// <summary>
        /// 运行时的实例Id
        /// </summary>
        public int InstanceId { get; private set; }

        /// <summary>
        /// 配置数据
        /// </summary>
        public UnitData UnitData { get; private set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 阵营
        /// </summary>
        public UnitCamp Camp { get; private set; }

        private Dictionary<System.Type, UnitComponent> _components = new Dictionary<System.Type, UnitComponent>();
        #endregion

        #region Life Cycle
        public void Init(int instanceId, UnitData unitData, int level, UnitCamp unitCamp)
        {
            this.InstanceId = instanceId;
            this.UnitData = unitData;
            this.Level = level;
            this.Camp = unitCamp;

            AddComponents();
            var unitComponents = GetComponents<UnitComponent>();
            foreach (var unitComponent in unitComponents)
            {
                unitComponent.Init(this);
            }
            OnInit();
        }

        public void Release()
        {
            OnRelease();
            foreach (var component in _components.Values)
            {
                component.Release();
            }
            _components.Clear();
        }
        #endregion

        #region Public Methods
        public T AddUnitComponent<T>() where T : UnitComponent
        {
            if (gameObject.GetComponent<T>() != null)
            {
                GameLog.Error($"UnitController.AddComponent: {typeof(T).Name} 已经存在，不需要重复添加");
                return null;
            }
            T component = gameObject.AddComponent<T>();
            _components.Add(typeof(T), component);
            return component;
        }

        public T GetUnitComponent<T>() where T : UnitComponent
        {
            if (_components.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }
            return null;
        }
        #endregion

        #region Private & Protected Methods
        /// <summary>
        /// 这个是为了方便子类添加组件，子类只需要调用base.AddComponents()，减少重复代码
        /// </summary>
        protected abstract void AddComponents();
        protected virtual void OnInit() { }
        protected virtual void OnRelease() { }
        #endregion
    }
}
