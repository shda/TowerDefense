using System;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class WeaponSingleShot : MonoBehaviour
    {
        [SerializeField] private ProjectileSpawner projectilePool;
        [SerializeField] private float speedMovingProjectile = 5.0f;
        [SerializeField] private int damage = 10;
        
        public Action<IGunTarget> OnProjectileCompleteMove;
        
        public void ShotTo(IGunTarget target)
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