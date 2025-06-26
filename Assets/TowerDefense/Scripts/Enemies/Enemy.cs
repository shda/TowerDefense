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
        [SerializeField] private MovingToNavigationMash movingToNavigationMash;
        [SerializeField] private HitAnimation hitAnimation;
        [SerializeField] private float speedMoving;
        
        private EffectHandler _effectHandler;
        private EffectContext _effectContext;
        public MovingToNavigationMash MovingToNavigationMas => movingToNavigationMash;
        public Health Health { get; private set; }
        public bool IsDead => Health.CurrentHealth.Value <= 0;
        public ReactiveProperty<EnemyStatus> Status { get; } = new();

        Transform ITarget.Transform => transform;
        bool ITarget.IsCanByTarget => !IsDead;

        [Inject]
        public void Construct(
            Health health,
            EffectHandler effectHandler)
        {
            _effectHandler = effectHandler;
            Health = health;
            Reset();

            
            Health.CurrentHealth.Subscribe(f =>
            {
                if (f <= 0)
                {
                    Status.Value = EnemyStatus.Dead;
                }
                else
                {
                    hitAnimation.Play();
                }
            }).AddTo(this);

            _effectContext = new EffectContext(_effectHandler, Health, movingToNavigationMash);
            movingToNavigationMash.OnMovingComplete = () =>
            {
                UnityEngine.Debug.Log("Moving Complete");
                Status.Value = EnemyStatus.Complete;
            };
        }

        public EffectContext GetEffectContext()
        {
            return _effectContext;
        }

        public void Reset()
        {
            _effectHandler.RemoveAllEffects();
            Status.Value = EnemyStatus.None;
            Health.CurrentHealth.Value = 100;
            Health.MaxHealth.Value = 100;
        }

        public void SetStartPosition(Vector3 worldPosition)
        {
            movingToNavigationMash.SetStartPosition(worldPosition);
        }
        public void SetMovingPath(Vector3 movingPath)
        {
            Status.Value = EnemyStatus.Moving;
            movingToNavigationMash.StartMoving(movingPath);
        }

        public void CustomUpdate()
        {
            _effectHandler.CustomUpdate();
            movingToNavigationMash.CustomUpdate();
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