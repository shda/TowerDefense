using System;
using System.Collections.Generic;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IPoolable<IMemoryPool> , IGunTarget
    {
        [SerializeField] private float speedMoving;
        
        private IMemoryPool _memoryPool;
        private MovingHandler _movingHandler;

        public Health Health { get; private set; }
        public bool IsDead { get; private set; }
        public Action OnCompleteMove;
        
        //IGunTarget
        Transform IGunTarget.Transform => transform;
        bool IGunTarget.IsCanByTarget => Health.CurrentHealth.Value > 0 && !IsDead;

        [Inject]
        public void Construct(Health health , MovingHandler movingHandler)
        {
            _movingHandler = movingHandler;
            _movingHandler.Init(transform, speedMoving);
            _movingHandler.OnCompleteMove = () => { OnCompleteMove?.Invoke(); };

            Health = health;

            //
            Health.CurrentHealth.Value = 100;
        }
        
        public void SetStartPosition(Vector3 worldPosition)
        {
            _movingHandler.SetStartPosition(worldPosition);
        }

        public void SetMovingPath(Vector3 firstTargetPoint, IReadOnlyList<Vector3> movingPath)
        {
            _movingHandler.SetMovingPath(firstTargetPoint, movingPath);
        }

        public void CustomUpdate()
        {
            _movingHandler.CustomUpdate();
        }
        
        #region Pooling
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

        public class Factory : PlaceholderFactory<Enemy>
        {
        }
        #endregion
    }
}