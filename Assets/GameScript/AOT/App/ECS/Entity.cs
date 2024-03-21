using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.ECS
{
    /// <summary>
    /// 实体
    /// </summary>
    public abstract class Entity : IDisposable
    {
        #region Abstract & Virtual Methods
        protected abstract UniTask OnInitialize();
        protected abstract void OnUpdate();
        protected abstract void OnRelease();
        #endregion

        #region Static & Const Fields
        private static long GENERATE_ID = 0;
        #endregion

        #region Fields & Properties
        protected Dictionary<long, Entity> _children = new Dictionary<long, Entity>();
        protected Entity _parent;
        protected bool _active;

        public long Id { get; private set; }
        public Entity Parent => _parent;
        #endregion

        #region Public Methods
        public async UniTask<T> AddChild<T>() where T : Entity, new()
        {
            var child = new T();
            //赋值字段
            child.Id = ++GENERATE_ID;
            child._parent = this;
            child._active = true;

            //初始化
            await child.Initialize();

            //添加到子节点
            _children.Add(child.Id, child);

            return child;
        }

        public async UniTask Initialize()
        {
            if (_active)
            {
                await OnInitialize();
            }
        }

        public void Update()
        {
            foreach (var (_, child) in _children)
            {
                child.Update();
            }
        }

        public void Dispose()
        {
            foreach (var (_, child) in _children)
            {
                child.Dispose();
            }
            if (_children.Count > 0)
            {
                throw new Exception("Should not get here.");
            }

            OnRelease();

            if (_parent != null)
            {
                _parent._children.Remove(Id);
                _parent = null;
            }
        }
        #endregion

        #region Private & Protected Methods
        #endregion
    }
}
