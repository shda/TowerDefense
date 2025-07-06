using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Scripts.Enemies.Movements
{
    public class NavigationMashMovement : BaseMovement
    {
        private const float StoppingThreshold = 0.3f;
        [SerializeField] private NavMeshAgent navMeshAgent;
        private bool _destinationReached;
        private Vector3 _startPosition;
        private Vector3 _targetPoint;
        public override float SpeedMoving
        {
            get => navMeshAgent.speed;
            set => navMeshAgent.speed = value;
        }

        public override void StartMoving(Vector3 targetPoint)
        {
            _destinationReached = false;
            _targetPoint = targetPoint;
            navMeshAgent.enabled = true;
            navMeshAgent.updateRotation = false;
            navMeshAgent.SetDestination(targetPoint);
        }

        public override void Stop()
        {
            navMeshAgent.enabled = false;
        }

        public override void SetStartPosition(Vector3 worldPosition)
        {
            navMeshAgent.enabled = false;
            transform.position = worldPosition;
        }

        public override void Repath()
        {
            _destinationReached = false;
            StartMoving(_targetPoint);
        }

        public override void CustomUpdate()
        {
            if (navMeshAgent.enabled && navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.isStopped = !IsMoving;
            }

            CheckIsPathComplete();
        }

        private void CheckIsPathComplete()
        {
            if (_destinationReached)
            {
                return;
            }

            if (Vector3.Distance(transform.position, _targetPoint) < StoppingThreshold)
            {
                _destinationReached = true;
                OnMovingComplete?.Invoke();
            }
        }
    }
}