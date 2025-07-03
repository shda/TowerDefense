using TowerDefense.Scripts.Towers;
using UniRx;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public class SlowdownEffect : Effect
    {
        private readonly float _downSpeedModifier;
        private readonly CompositeDisposable composite = new();

        public SlowdownEffect(ITarget target, float downSpeedModifier, float duration)
        {
            Duration = duration;
            _downSpeedModifier = downSpeedModifier;

            target.Status.Subscribe(status =>
            {
                if (status == EnemyActionStatus.Dead) Duration = 0;
            }).AddTo(composite);
        }

        public override void SetEffect(EffectContext effectContext)
        {
            //_lastSpeed = effectContext.movingHandler.SpeedMoving;
            effectContext.movingHandler.SpeedMoving *= _downSpeedModifier;

            base.SetEffect(effectContext);
        }

        protected override void ResetEffect()
        {
            _effectContext.movingHandler.SpeedMoving /= _downSpeedModifier;
            composite.Dispose();
        }
    }
}