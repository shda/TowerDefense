using TowerDefense.Scripts.Enemies;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class ArtilleryTower : ProjectileGunTowerBase
    {
        [SerializeField] private float damage = 2.0f;
        [SerializeField] private float radiusDamage = 1.0f;
        protected override void OnHitToTarget(ITarget target)
        {
            if (target is { IsCanByTarget: true })
            {
                var  hits = Physics2D.CircleCastAll(
                    target.Transform.position, radiusDamage , Vector2.zero);
                foreach (var hit2D in hits)
                {
                    //Todo need optimize
                    var targetHit = hit2D.transform.GetComponent<ITarget>();
                    if (targetHit != null && targetHit.EnemyType == EnemyType.Ground)
                    {
                        targetHit.Health.CurrentHealth.Value -= damage;
                    }
                }
            }
        }
    }
}