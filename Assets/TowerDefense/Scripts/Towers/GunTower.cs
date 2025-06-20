using System;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class GunTower : ProjectileGunTowerBase
    {
        [SerializeField] private float damage;
        protected override void OnHitToTarget(ITarget target)
        {
            if (target is { IsCanByTarget: true })
            {
                target.Health.CurrentHealth.Value -= damage;
            }
        }
    }
}