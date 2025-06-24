using System;
using System.Collections.Generic;
using DG.Tweening;
using TowerDefense.Scripts.Enemies.Effects;
using TowerDefense.Scripts.Towers;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IPoolable<IMemoryPool>, ITarget
    {
        [SerializeField] private HitAnimation hitAnimation;
        [SerializeField] private float speedMoving;

        private MovingToPathHandler _movingToPathHandler;
        private EffectHandler _effectHandler;
        private EffectContext _effectContext;

        public Health Health { get; private set; }
        public bool IsDead => Health.CurrentHealth.Value <= 0;

        public ReactiveCommand OnDead { get; } = new();

        Transform ITarget.Transform => transform;
        bool ITarget.IsCanByTarget => !IsDead;

        [Inject]
        public void Construct(
            Health health,
            MovingToPathHandler movingToPathHandler,
            EffectHandler effectHandler)
        {
            _effectHandler = effectHandler;
            _movingToPathHandler = movingToPathHandler;
            _movingToPathHandler.Init(transform, speedMoving);
            _movingToPathHandler.OnCompleteMove = () => { OnDead?.Execute(); };

            Health = health;
            Reset();

            Health.CurrentHealth.Subscribe(f =>
            {
                if (f <= 0)
                {
                    OnDead?.Execute();
                }
                else
                {
                    hitAnimation.Play();
                }
            }).AddTo(this);

            _effectContext = new EffectContext(_effectHandler, Health, _movingToPathHandler);
        }

        public EffectContext GetEffectContext()
        {
            return _effectContext;
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
        public void SetMovingPath(IEnumerable<Vector3> movingPath)
        {
            _movingToPathHandler.SetMovingToPath(movingPath);
        }

        public void CustomUpdate()
        {
            _effectHandler.CustomUpdate();
            _movingToPathHandler.CustomUpdate();
        }

        #region Spawn

        private IMemoryPool _memoryPool;

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