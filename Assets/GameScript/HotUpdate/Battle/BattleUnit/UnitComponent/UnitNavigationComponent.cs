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

        public NumberX1000 Radius { get => _navMeshAgent.radius.ToLogic(); set => _navMeshAgent.radius = value.ToScene(); }
        public NumberX1000 Speed { get => _navMeshAgent.speed.ToLogic(); set => _navMeshAgent.speed = value.ToScene(); }
        public NumberX1000 StopDistance { get => _navMeshAgent.stoppingDistance.ToLogic(); set => _navMeshAgent.stoppingDistance = value.ToScene(); }

        public Vector3 Destination { get => _navMeshAgent.destination.ToLogic(); set => _navMeshAgent.destination = value.ToScene(); }
        public bool IsStopped { get => _navMeshAgent.isStopped; set => _navMeshAgent.isStopped = value; }

        protected override void OnInit()
        {
            _navMeshAgent = gameObject.GetOrAddComponent<NavMeshAgent>();
            _navMeshAgent.radius = (NumberX1000.One * 0.5f).ToScene();
            _navMeshAgent.height = 2f;
            _navMeshAgent.acceleration = float.MaxValue;
            _navMeshAgent.autoBraking = true;
            _navMeshAgent.angularSpeed = 360f;
            _navMeshAgent.Warp(transform.position);

            _attributes = _controller.GetUnitComponent<UnitAttributesComponent>();
        }

        private void Update()
        {
            if (_attributes.IsDead())
            {
                _navMeshAgent.isStopped = true;
                return;
            }
            var speed = _attributes.GetValue(Cfg.NumericId.Speed).ToFloat();

            Speed = speed;
        }

        public void RotateTo(Vector3 targetPosition)
        {
            var direction = targetPosition - _controller.LogicPosition;

            // 根据角速度旋转
            var rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _navMeshAgent.angularSpeed * Time.deltaTime);
        }
    }
}