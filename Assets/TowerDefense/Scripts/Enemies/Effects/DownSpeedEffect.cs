using TowerDefense.Scripts.Towers;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public class DownSpeedEffect : Effect
    {
        private readonly ITarget _target;
        private readonly float _downSpeedModifier;
        private readonly CompositeDisposable composite = new();
        private float _lastSpeed;

        public DownSpeedEffect(ITarget target, float downSpeedModifier, float duration)
        {
            Duration = duration;
            _target = target;
            _downSpeedModifier = downSpeedModifier;

            _target.OnDead.Subscribe(unit =>
            {
                Duration = 0;
            }).AddTo(composite);
        }

        public override void SetEffect(EffectContext effectContext)
        {
            _lastSpeed = effectContext.MovingToPathHandler.SpeedMoving;
            effectContext.MovingToPathHandler.SpeedMoving *= _downSpeedModifier;

            base.SetEffect(effectContext);
        }

        protected override void ResetEffect()
        {
            _effectContext.MovingToPathHandler.SpeedMoving = _lastSpeed;
            composite.Dispose();
        }
    }
}