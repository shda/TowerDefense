using System;
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
        
        public Action OnCompleteMove;

        void IPoolable<IMemoryPool>.OnDespawned()
        {
            
        }

        void IPoolable<IMemoryPool>.OnSpawned(IMemoryPool pool)
        {
            _memoryPool = pool;
        }

        public void RemoveToPool()
        {
            _memoryPool.Despawn(this);
        }

        public void SetStartPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        public void SetMovingPath(Vector3 firstTargetPoint, IReadOnlyList<Vector3> movingPath)
        {
            _movingPath = movingPath;
            _indexPointToPath = 0;
            _targetPosition = firstTargetPoint;
        }

        public void CustomUpdate()
        {
            StepMovingToPath();
        }

        private void StepMovingToPath()
        {
            if(_indexPointToPath >= _movingPath.Count)
            {
                return;
            }
            
            transform.position = Vector3.MoveTowards(
                transform.position , 
                _targetPosition ,  
                speedMoving * Time.deltaTime);
            
            var direction = _targetPosition - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            if (Vector3.Distance(transform.position, _targetPosition) <= 0.1f)
            {
                _indexPointToPath++;
                if (_indexPointToPath >= _movingPath.Count)
                {
                    OnCompleteMove?.Invoke();
                    return;
                }
                
                _targetPosition = _movingPath[_indexPointToPath];
            }
        }
    }
}