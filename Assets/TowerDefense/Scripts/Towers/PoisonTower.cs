using TowerDefense.Scripts.Enemies.Effects;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class PoisonTower : ProjectileGunTowerBase
    {
        [SerializeField] private PoisonData poisonData;
        protected override void OnHitToTarget(ITarget target)
        {
            if (target is { IsCanByTarget: true } && IsCanUsingByTarget(target))
            {
                var effect = EffectMake.MakePoisonEffect(target , poisonData);
                var effectContext = target.GetEffectContext();
                effect.SetEffect(effectContext);
            }
        }

        protected override bool IsCanUsingByTarget(ITarget target)
        {
            var effectContext = target.GetEffectContext();
            return !effectContext.EffectHandler.IsContainEffectByType(typeof(PoisonEffect));
        }
    }
}