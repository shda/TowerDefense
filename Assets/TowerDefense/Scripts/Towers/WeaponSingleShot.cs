using System;
using TowerDefense.Scripts.Towers.Projectiles;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class WeaponSingleShot : MonoBehaviour
    {
        [SerializeField] private ProjectileSpawner projectilePool;
        [SerializeField] private float speedMovingProjectile = 5.0f;
        
        public Action<ITarget> OnProjectileCompleteMove;
        
        public void ShotTo(ITarget target)
        {
            var projectile =  projectilePool.Get();
            projectile.MoveToTarget(transform.position, target , speedMovingProjectile);
            projectile.OnCompleteMove = pr =>
            {
                OnProjectileCompleteMove?.Invoke(target);
                projectilePool.ReturnToPool(pr);
            };
        }
    }
}