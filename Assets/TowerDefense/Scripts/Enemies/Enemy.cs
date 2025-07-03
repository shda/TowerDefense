using System;
using TowerDefense.Scripts.Common;
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
        [SerializeField] private FindAndAttackPlayer _findAndAttackPlayer;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BaseMovement movingHandler;
        [SerializeField] private DamageAnimation damageAnimation;
        [SerializeField] private EnemyType enemyType;
        private EffectContext _effectContext;

        private EffectHandler _effectHandler;
        public bool IsDead => Health.CurrentHealth.Value <= 0;
        public EnemySettings Settings { get; private set; }

        public Action<Enemy> OnDestinationReached { get; set; }

        public void Reset()
        {
            _effectHandler.RemoveAllEffects();
            Status.Value = EnemyActionStatus.None;
            movingHandler.Stop();
        }

        public Health Health { get; private set; }
        public EnemyType EnemyType => enemyType;
        public ReactiveProperty<EnemyActionStatus> Status { get; } = new();

        Transform ITarget.Transform => transform;
        bool ITarget.IsCanByTarget => !IsDead;

        public EffectContext GetEffectContext()
        {
            return _effectContext;
        }

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
                Debug.Log("Moving Complete");
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

            _findAndAttackPlayer.SetInfo(settings);
        }

        private void OnChangeHp(float hp)
        {
            if (hp <= 0)
                Status.Value = EnemyActionStatus.Dead;
            else
                damageAnimation.Play();
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
            movingHandler.IsMoving = !_findAndAttackPlayer.IsCanHitPlayer;
            movingHandler.CustomUpdate();
            _findAndAttackPlayer.CustomUpdate();
        }

        public void Repath()
        {
            movingHandler.Repath();
        }
    }
}