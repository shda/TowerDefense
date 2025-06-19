using System.Collections.Generic;
using System.Linq;
using BlockBlast.Scripts.Common;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Towers
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private PoolGameObjectsComponent<Projectile> projectilePool;
        private readonly HashSet<Projectile> _activeProjectiles = new();
        
        public Projectile Get()
        {
            var projectile = projectilePool.Get();
            _activeProjectiles.Add(projectile);

            return projectile;
        }
        
        public void ReturnToPool(Projectile projectile)
        {
            projectilePool.Release(projectile);
            _activeProjectiles.Remove(projectile);
        }
        
        public void Update()
        {
            foreach (var projectile in _activeProjectiles.ToArray())
            {
                projectile.CustomUpdate();
            }
        }
    }
}