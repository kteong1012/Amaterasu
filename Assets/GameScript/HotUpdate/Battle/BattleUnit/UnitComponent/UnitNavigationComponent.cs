using DG.Tweening;
using Game.Log;
using System;
using UniFramework.Event;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class UnitNavigationComponent : UnitComponent
    {
        public NumberX1000 Radius { get => _navMeshAgent.radius.ToLogic(); set => _navMeshAgent.radius = value.ToScene(); }
        public NumberX1000 Speed { get => _navMeshAgent.speed.ToLogic(); set => _navMeshAgent.speed = value.ToScene(); }
        public NumberX1000 StopDistance { get => _navMeshAgent.stoppingDistance.ToLogic(); set => _navMeshAgent.stoppingDistance = value.ToScene(); }
        public Vector3 Destination { get => _navMeshAgent.destination.ToLogic(); set => _navMeshAgent.destination = value.ToScene(); }
        public bool IsStopped { get => _navMeshAgent.isStopped; set => _navMeshAgent.isStopped = value; }

        private BattleUnitController _battleUnit;
        private NavMeshAgent _navMeshAgent;
        private UnitController _targetUnit;

        // 重置路径的距离,即目标移动超过这个距离时,重新计算路径
        private NumberX1000 _resetPathDistance = NumberX1000.CreateFromX1000Value(50);

        protected override void OnInit()
        {
            _battleUnit = _controller as BattleUnitController;
            _navMeshAgent = gameObject.GetOrAddComponent<NavMeshAgent>();
            _navMeshAgent.height = 2f;
            _navMeshAgent.acceleration = float.MaxValue;
            _navMeshAgent.autoBraking = true;
            _navMeshAgent.angularSpeed = 360f;

            Radius = NumberX1000.CreateFromX1000Value(_controller.UnitData.RadiusX1000);

            _navMeshAgent.Warp(transform.position);
        }

        protected override void OnRelease()
        {
            transform.DOKill();
        }

        private void Update()
        {
            if (_targetUnit != null)
            {
                // 目标可能移动，所以要实时更新目标位置
                var targetPosition = _targetUnit.LogicPosition;
                if (Vector3.Distance(Destination, targetPosition) > _resetPathDistance)
                {
                    Destination = targetPosition;
                }
            }
        }

        public void StopMove()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
        }

        /// <summary>
        /// 追逐目标
        /// </summary>
        /// <param name="unitController">要追逐的目标</param>
        /// <param name="stopDistance">逻辑坐标</param>
        public void ChaseUnit(UnitController unitController, NumberX1000 stopDistance)
        {
            IsStopped = false;
            _targetUnit = unitController;
            Speed = _battleUnit.GetStatsValue(Cfg.NumericId.MoveSPD);
            StopDistance = stopDistance;
        }

        /// <summary>
        /// 移动到目的地
        /// </summary>
        /// <param name="targetPosition">逻辑坐标</param>
        /// <param name="stopDistance">逻辑坐标</param>
        public void MoveToPlace(Vector3 targetPosition, NumberX1000 stopDistance)
        {
            _targetUnit = null;
            IsStopped = false;
            Destination = targetPosition;
            Speed = _battleUnit.GetStatsValue(Cfg.NumericId.MoveSPD);
            StopDistance = stopDistance;
            Destination = targetPosition;
        }
    }
}