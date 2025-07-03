using TowerDefense.Scripts.Enemies.Effects;
using TowerDefense.Scripts.Towers.Projectiles;
using UnityEngine;

namespace TowerDefense.Scripts.Towers.TowersTypes
{
    public class PoisonTower : ProjectileGunTowerBase
    {
        [SerializeField] private PoisonData poisonData;

        protected override void OnHitToTarget(ITarget target)
        {
            if (target is { IsCanByTarget: true } && IsCanUsingByTarget(target))
            {
                var data = new PoisonData(poisonData, ModifierByLevel());

                var effect = EffectMaker.MakePoisonEffect(target, data);
                var effectContext = target.GetEffectContext();
                effect.SetEffect(effectContext);
            }
        }

        protected override bool IsCanUsingByTarget(ITarget target)
        {
            return true;
            var effectContext = target.GetEffectContext();
            return effectContext.EffectHandler.CountContainEffectByType(typeof(PoisonEffect)) < 0;
        }
    }
}