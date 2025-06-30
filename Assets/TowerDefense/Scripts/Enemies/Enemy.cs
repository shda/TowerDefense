using System;
using TowerDefense.Scripts.Enemies.Effects;
using TowerDefense.Scripts.Towers;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IPoolable<IMemoryPool>, ITarget
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private MovingHandlerBase movingHandler;
        [SerializeField] private HitAnimation hitAnimation;
        [SerializeField] private EnemyType enemyType;
        
        private EffectHandler _effectHandler;
        private EffectContext _effectContext;
        public Health Health { get; private set; }
        public EnemyType EnemyType => enemyType;
        public bool IsDead => Health.CurrentHealth.Value <= 0;
        public ReactiveProperty<EnemyStatus> Status { get; } = new();
        public EnemySettings Settings { get; private set; }

        Transform ITarget.Transform => transform;
        bool ITarget.IsCanByTarget => !IsDead;
        
        public Action<Enemy> OnTargetHit { get; set; }
        
        [Inject]
        public void Construct(
            Health health,
            EffectHandler effectHandler)
        {
            _effectHandler = effectHandler;
            Health = health;
            Reset();

            Health.CurrentHealth.Subscribe(OnChangeHp).AddTo(this);

            _effectContext = new EffectContext(_effectHandler, Health, movingHandler);
            movingHandler.OnMovingComplete = () =>
            {
                UnityEngine.Debug.Log("Moving Complete");
                Status.Value = EnemyStatus.Complete;
                OnTargetHit?.Invoke(this);
            };
        }

        public void SetInfo(EnemySettings settings)
        {
            Settings = settings;
            
            enemyType = settings.enemyType;
            spriteRenderer.sprite = settings.sprite;

            Health.CurrentHealth.Value = settings.health;
            Health.MaxHealth.Value = settings.health;
            movingHandler.SpeedMoving = settings.speedMoving;
        }

        private void OnChangeHp(float hp)
        {
            if (hp <= 0)
            {
                Status.Value = EnemyStatus.Dead;
            }
            else
            {
                hitAnimation.Play();
            }
        }

        public EffectContext GetEffectContext()
        {
            return _effectContext;
        }

        public void Reset()
        {
            _effectHandler.RemoveAllEffects();
            Status.Value = EnemyStatus.None;
            movingHandler.Stop();
        }

        public void SetStartPosition(Vector3 worldPosition)
        {
            movingHandler.SetStartPosition(worldPosition);
        }

        public void SetMovingPath(Vector3 movingPath)
        {
            Status.Value = EnemyStatus.Moving;
            movingHandler.StartMoving(movingPath);
        }

        public void CustomUpdate()
        {
            _effectHandler.CustomUpdate();
            movingHandler.CustomUpdate();
        }

        public void Repath()
        {
            movingHandler.Repath();
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