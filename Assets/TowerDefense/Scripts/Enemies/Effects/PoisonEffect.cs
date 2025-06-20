using TowerDefense.Scripts.Towers;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public class PoisonEffect : EffectBase
    {
        private PoisonData _poisonData;
        private ITarget _target;
        private float _currentTime;
        private int _hitCounter;
        private CompositeDisposable composite = new();
        
        public PoisonEffect(ITarget target, PoisonData poisonData)
        {
            _hitCounter = poisonData.MaxDamages;
            _poisonData = poisonData;
            _target = target;
            _currentTime = 0;

            _target.OnDead.Subscribe(unit =>
            {
                ResetEffect();
            }).AddTo(composite);
        }
        private void ResetEffect()
        {
            composite.Dispose();
            CompleteEffect();
        }

        public override void CustomUpdate()
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