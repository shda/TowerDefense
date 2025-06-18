using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Towers
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float speedMovingProjectile = 5.0f;
        [SerializeField] private int damage = 10;
        private ProjectileSpawner _projectileSpawner;
        
        [Inject]
        public void Construct(ProjectileSpawner projectileSpawner)
        {
            _projectileSpawner = projectileSpawner;
        }

        public void ShotTo(IGunTarget target)
        {
            var projectile = _projectileSpawner.Create();
            projectile.MoveToTarget(transform.position, target , speedMovingProjectile);
            projectile.OnCompleteMove = pr =>
            {
                if (target is { IsCanByTarget: true })
                {
                    target.Health.CurrentHealth.Value -= damage;
                }
                
                _projectileSpawner.ReturnToPool(pr);
            };
        }
    }
}