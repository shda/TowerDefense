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
        
        private readonly float stoppingThreshold = 0.1f; 
        private bool _destinationReached = false;
        private Vector3 _targetPoint;

        public float SpeedMoving
        {
            get => navMeshAgent.speed;
            set => navMeshAgent.speed = value;
        }

        [Button]
        public void StartMoving(Vector3 targetPoint)
        {
            _destinationReached = false;
            _targetPoint = targetPoint;
            navMeshAgent.updateRotation = false;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(targetPoint);
        }

        public void Repath()
        {
            _destinationReached = false;
            navMeshAgent.ResetPath();
            navMeshAgent.SetDestination(_targetPoint);
        }

        private void CheckIsPathComplete()
        {
            if(_destinationReached)
                return;
            
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + stoppingThreshold)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        _destinationReached = true;
                        OnMovingComplete?.Invoke();
                    }
                }
            }
            
            /*
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !_destinationReached)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    _destinationReached = true;
                    OnMovingComplete?.Invoke();
                }
            }
            else if (navMeshAgent.hasPath)
            {
                _destinationReached = false;
            }
            */
        }

        public void CustomUpdate()
        {
            CheckIsPathComplete();
        }
    }
}