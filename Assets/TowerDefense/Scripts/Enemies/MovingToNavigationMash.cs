using System;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Scripts.Enemies
{
    public class MovingToNavigationMash : MovingHandlerBase
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        public Action OnMovingComplete { get; set; }

        private const float StoppingThreshold = 0.3f;
        private bool _destinationReached;
        private Vector3 _targetPoint;

        public override float SpeedMoving
        {
            get => navMeshAgent.speed;
            set => navMeshAgent.speed = value;
        }

        public override void StartMoving(Vector3 targetPoint)
        {
            navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            
            _destinationReached = false;
            _targetPoint = targetPoint;
            navMeshAgent.updateRotation = false;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(targetPoint);
        }

        public override void SetStartPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        public override void Repath()
        {
            _destinationReached = false;
            navMeshAgent.SetDestination(_targetPoint);
        }
        
        public override void CustomUpdate()
        {
            CheckIsPathComplete();
        }
        
        private void CheckIsPathComplete()
        {
            if(_destinationReached)
                return;
            
            if (Vector3.Distance(transform.position, _targetPoint) < StoppingThreshold)
            {
                _destinationReached = true;
                OnMovingComplete?.Invoke();
            }
        }
    }
}