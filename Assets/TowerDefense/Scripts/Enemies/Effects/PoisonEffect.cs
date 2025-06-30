using TowerDefense.Scripts.Towers;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public class PoisonEffect : Effect
    {
        private PoisonData _poisonData;
        private ITarget _target;
        private float _currentTime;
        private int _hitCounter;
        private CompositeDisposable composite = new();
        
        public PoisonEffect(ITarget target, PoisonData poisonData)
        {
            Duration = float.MaxValue;
            _hitCounter = poisonData.MaxDamages;
            _poisonData = poisonData;
            _target = target;
            _currentTime = float.MaxValue;

            _target.Status.Subscribe(status =>
            {
                if (status == EnemyActionStatus.Dead)
                {
                    ResetEffect();
                    Duration = 0;
                }
            }).AddTo(composite);
        }

        protected override void ResetEffect()
        {
            composite.Dispose();
        }

        public override void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _poisonData.DelayBetweenDamages)
            {
                _currentTime = 0;
                _hitCounter--;
                _target.Health.CurrentHealth.Value -= _poisonData.DamageOnIneHit;
            }

            if (_hitCounter <= 0)
            {
                ResetEffect();
            }
        }
    }
}