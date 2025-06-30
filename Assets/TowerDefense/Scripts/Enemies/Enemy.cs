using System;
using TowerDefense.Scripts.Enemies.Effects;
using TowerDefense.Scripts.Enemies.Movements;
using TowerDefense.Scripts.Towers;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, ITarget
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BaseMovement movingHandler;
        [SerializeField] private DamageAnimation damageAnimation;
        [SerializeField] private EnemyType enemyType;

        private EffectHandler _effectHandler;
        private EffectContext _effectContext;
        public Health Health { get; private set; }
        public EnemyType EnemyType => enemyType;
        public bool IsDead => Health.CurrentHealth.Value <= 0;
        public ReactiveProperty<EnemyActionStatus> Status { get; } = new();
        public EnemySettings Settings { get; private set; }

        Transform ITarget.Transform => transform;
        bool ITarget.IsCanByTarget => !IsDead;

        public Action<Enemy> OnDestinationReached { get; set; }

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
                Status.Value = EnemyActionStatus.Complete;
                OnDestinationReached?.Invoke(this);
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
                Status.Value = EnemyActionStatus.Dead;
            }
            else
            {
                damageAnimation.Play();
            }
        }

        public EffectContext GetEffectContext()
        {
            return _effectContext;
        }

        public void Reset()
        {
            _effectHandler.RemoveAllEffects();
            Status.Value = EnemyActionStatus.None;
            movingHandler.Stop();
        }

        public void SetStartPosition(Vector3 worldPosition)
        {
            movingHandler.SetStartPosition(worldPosition);
        }

        public void SetMovingPath(Vector3 movingPath)
        {
            Status.Value = EnemyActionStatus.Moving;
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
    }
}