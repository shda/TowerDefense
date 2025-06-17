using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class Enemy : MonoBehaviour , IPoolable<IMemoryPool>
    {
        [SerializeField] private float speedMoving;
        
        private IReadOnlyList<Vector3> _movingPath;
        private IMemoryPool _memoryPool;
        private int _indexPointToPath;
        private Vector3 _targetPosition;
        
        void IPoolable<IMemoryPool>.OnDespawned()
        {
            
        }

        void IPoolable<IMemoryPool>.OnSpawned(IMemoryPool pool)
        {
            _memoryPool = pool;
        }

        public void SetMovingPath(IReadOnlyList<Vector3> movingPath)
        {
            _movingPath = movingPath;
            _indexPointToPath = 0;
            transform.position = _movingPath[_indexPointToPath];
            _indexPointToPath++;
        }

        public void CustomUpdate()
        {
            StepMovingToPath();
        }

        private void StepMovingToPath()
        {
            if(_indexPointToPath >= _movingPath.Count)
                return;

            var targetPoint = _movingPath[_indexPointToPath];
            transform.position = Vector3.MoveTowards(
                transform.position , 
                targetPoint ,  
                speedMoving * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint) <= 0.1f)
            {
                _indexPointToPath++;
            }
        }
    }
}