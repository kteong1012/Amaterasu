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

        public float Radius { get => _navMeshAgent.radius; set => _navMeshAgent.radius = value; }
        public float Speed { get => _navMeshAgent.speed; set => _navMeshAgent.speed = value; }
        public Vector3 Destination { get => _navMeshAgent.destination; set => _navMeshAgent.destination = value; }
        public float StopDistance { get => _navMeshAgent.stoppingDistance; set => _navMeshAgent.stoppingDistance = value; }
        public bool IsStopped { get => _navMeshAgent.isStopped; set => _navMeshAgent.isStopped = value; }

        protected override void OnInit()
        {
            _navMeshAgent = gameObject.GetOrAddComponent<NavMeshAgent>();
            _navMeshAgent.radius = 1f;
            _navMeshAgent.height = 2f;
            _navMeshAgent.acceleration = float.MaxValue;
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
            var radius = 1f;
            var height = 2f;
            var speed = _attributes.GetValue(Cfg.NumericId.Speed).ToFloat();


            _navMeshAgent.radius = radius;
            _navMeshAgent.height = height;
            _navMeshAgent.speed = speed;
        }
    }
}