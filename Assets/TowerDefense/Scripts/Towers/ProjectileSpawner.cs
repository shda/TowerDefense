using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace TowerDefense.Scripts.Towers
{
    public class ProjectileSpawner : ITickable
    {
        private Projectile.Factory _projectileFactory;
        private readonly HashSet<Projectile> _activeProjectiles = new();
        
        [Inject]
        public void Construct(Projectile.Factory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }
        
        public Projectile Create()
        {
            var projectile = _projectileFactory.Create();
            _activeProjectiles.Add(projectile);

            return projectile;
        }
        
        public void Tick()
        {
            foreach (var projectile in _activeProjectiles.ToArray())
            {
                projectile.CustomUpdate();
            }
        }

        public void ReturnToPool(Projectile projectile)
        {
            projectile.ReturnToPool();
        }
    }
}