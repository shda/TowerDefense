using System;
using TowerDefense.Scripts.Enemies;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class GunTower : ProjectileGunTowerBase
    {
        [SerializeField] private float damage;
        [SerializeField] private EnemyType onlyEnemy = EnemyType.None;
        protected override void OnHitToTarget(ITarget target)
        {
            if (target is { IsCanByTarget: true })
            {
                target.Health.CurrentHealth.Value -= damage;
            }
        }
        
        protected override bool IsCanUsingByTarget(ITarget target)
        {
            if (onlyEnemy == EnemyType.None)
                return true;
            
            return target.EnemyType == onlyEnemy;
        }
    }
}