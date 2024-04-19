using Game.Cfg;
using Game.Log;
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

        public int Level { get; private set; }

        /// <summary>
        /// 阵营
        /// </summary>
        public UnitCamp Camp { get; private set; }
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
            //component.OnInit(this);
            return component;
        }

        public T GetUnitComponent<T>() where T : UnitComponent
        {
            return gameObject.GetComponent<T>();
        }
        #endregion

        #region Private & Protected Methods
        /// <summary>
        /// 这个是为了方便子类添加组件，子类只需要调用base.AddComponents()，减少重复代码
        /// </summary>
        protected abstract void AddComponents();
        protected virtual void OnInit() { }
        #endregion
    }
}
