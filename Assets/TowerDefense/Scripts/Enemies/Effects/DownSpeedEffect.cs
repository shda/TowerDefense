using TowerDefense.Scripts.Towers;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public class DownSpeedEffect : EffectBase
    {
        private ITarget _target;
        private readonly float _downSpeedModifier;
        private float _timeEffect;

        private float _currentTime;
        private CompositeDisposable composite = new();

        private float _lastSpeed;

        public DownSpeedEffect(ITarget target, float downSpeedModifier, float timeEffect)
        {
            _timeEffect = timeEffect;
            _target = target;
            _downSpeedModifier = downSpeedModifier;
            _currentTime = 0;

            _target.OnDead.Subscribe(unit =>
            {
                ResetEffect();
            }).AddTo(composite);
        }

        public override void SetEffect(EffectContext effectContext)
        {
            _lastSpeed = effectContext.MovingToPathHandler.SpeedMoving;
            effectContext.MovingToPathHandler.SpeedMoving *= _downSpeedModifier;
            
            base.SetEffect(effectContext);
        }

        private void ResetEffect()
        {
            _effectContext.MovingToPathHandler.SpeedMoving = _lastSpeed;
            composite.Dispose();
            CompleteEffect();
        }

        public override void CustomUpdate()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _timeEffect)
            {
                _currentTime = 0;
                ResetEffect();
            }
        }
    }
}