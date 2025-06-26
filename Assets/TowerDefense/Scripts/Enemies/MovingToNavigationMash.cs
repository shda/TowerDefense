using System;
using TriInspector;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Scripts.Enemies
{
    public class MovingToNavigationMash : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;

        public Action OnMovingComplete;
        
        private readonly float stoppingThreshold = 0.3f; 
        private bool _destinationReached = false;
        private Vector3 _targetPoint;

        public float SpeedMoving
        {
            get => navMeshAgent.speed;
            set => navMeshAgent.speed = value;
        }
        
        public void StartMoving(Vector3 targetPoint)
        {
            navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            
            _destinationReached = false;
            _targetPoint = targetPoint;
            navMeshAgent.updateRotation = false;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(targetPoint);
        }
        
        public void SetStartPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        public void Repath()
        {
            _destinationReached = false;
            navMeshAgent.SetDestination(_targetPoint);
        }

        private void CheckIsPathComplete()
        {
            if(_destinationReached)
                return;
            
            if (Vector3.Distance(transform.position, _targetPoint) < stoppingThreshold)
            {
                _destinationReached = true;
                OnMovingComplete?.Invoke();
            }
        }

        public void CustomUpdate()
        {
            CheckIsPathComplete();
        }
    }
}