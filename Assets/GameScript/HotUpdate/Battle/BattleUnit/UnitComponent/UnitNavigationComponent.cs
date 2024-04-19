using System;
using UniFramework.Event;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class UnitNavigationComponent : UnitComponent
    {
        private NavMeshAgent _navMeshAgent;
        private UnitAttributesComponent _attributes;
        private EventGroup _eventGroup = new EventGroup();

        protected override void OnInit()
        {
            _navMeshAgent = gameObject.GetOrAddComponent<NavMeshAgent>();
            _navMeshAgent.radius = 1f;
            _navMeshAgent.height = 2f;
            _navMeshAgent.acceleration = float.MaxValue;
            _navMeshAgent.Warp(transform.position);

            _attributes = _controller.GetUnitComponent<UnitAttributesComponent>();

            _eventGroup.AddListener<UnitAttributeChangeEvent>(OnUnitAttributeChange);
        }

        public void Update()
        {
            if (_attributes.IsDead())
            {
                _navMeshAgent.isStopped = true;
                return;
            }
            var radius = 1f;
            var height = 2f;
            var speed = _attributes.GetValue(Cfg.NumericId.Speed).ToFloat();


            _navMeshAgent.radius = radius;
            _navMeshAgent.height = height;
            _navMeshAgent.speed = speed;

            _navMeshAgent.SetDestination(Vector3.zero);
        }

        private void OnUnitAttributeChange(IEventMessage message)
        {
            var msg = message as UnitAttributeChangeEvent;
            if (msg.InstanceId != _controller.InstanceId)
            {
                return;
            }
            if (msg.NumericId == Cfg.NumericId.Hp)
            {
                if (msg.OldValue > 0 && msg.NewValue <= 0)
                {
                    _navMeshAgent.isStopped = true;
                }
                else if (msg.OldValue <= 0 && msg.NewValue > 0)
                {
                    _navMeshAgent.isStopped = false;
                }
            }
        }
    }
}