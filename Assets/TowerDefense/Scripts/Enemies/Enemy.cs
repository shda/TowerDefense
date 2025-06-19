using System;
using System.Collections.Generic;
using TowerDefense.Scripts.Towers;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IPoolable<IMemoryPool> , IGunTarget
    {
        [SerializeField] private float speedMoving;
        
        private IMemoryPool _memoryPool;
        private MovingToPathHandler _movingToPathHandler;

        public Health Health { get; private set; }
        public bool IsDead => Health.CurrentHealth.Value <= 0;

        public ReactiveCommand OnDead { get; } = new();
        
        Transform IGunTarget.Transform => transform;
        bool IGunTarget.IsCanByTarget => !IsDead;

        [Inject]
        public void Construct(Health health , MovingToPathHandler movingToPathHandler)
        {
            _movingToPathHandler = movingToPathHandler;
            _movingToPathHandler.Init(transform, speedMoving);
            _movingToPathHandler.OnCompleteMove = () =>
            {
                OnDead?.Execute();
            };

            Health = health;
            Reset();
            
            Health.CurrentHealth.Subscribe(f =>
            {
                if (f <= 0)
                {
                    OnDead?.Execute();
                }
            }).AddTo(this);
        }

        public void Reset()
        {
            //
            Health.CurrentHealth.Value = 100;
            Health.MaxHealth.Value = 100;
        }
        
        public void SetStartPosition(Vector3 worldPosition)
        {
            _movingToPathHandler.SetStartPosition(worldPosition);
        }

        public void SetMovingPath(Vector3 firstTargetPoint, IReadOnlyList<Vector3> movingPath)
        {
            _movingToPathHandler.SetMovingToPath(firstTargetPoint, movingPath);
        }

        public void CustomUpdate()
        {
            _movingToPathHandler.CustomUpdate();
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