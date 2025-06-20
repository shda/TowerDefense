using TowerDefense.Scripts.Enemies.Effects;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class DownSpeedTower : ProjectileGunTowerBase
    {
        [SerializeField] private float downSpeedModifier;
        [SerializeField] private float timeEffect;
        protected override void OnHitToTarget(ITarget target)
        {
            if (target is { IsCanByTarget: true } && IsCanUsingByTarget(target))
            {
               var effect = EffectMake.MakeDownSpeedEffect(target , downSpeedModifier , timeEffect);
               var effectContext = target.GetEffectContext();
               effect.SetEffect(effectContext);
            }
        }

        protected override bool IsCanUsingByTarget(ITarget target)
        {
            var effectContext = target.GetEffectContext();
            return !effectContext.EffectHandler.IsContainEffectByType(typeof(DownSpeedEffect));
        }
    }
}