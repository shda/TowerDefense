using System;
using BlockBlast.Scripts.Common;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private PoolGameObjectsComponent<ProjectilePhysics> pool;
        
        [SerializeField] private Transform pointShoot;
        [SerializeField] private float speedMovingFire = 10.0f;
        [SerializeField] private float maxDistance = 10.0f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float timeBetweenShots = 0.2f;

        private float _currentTime;
        private bool _isPressButton;

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_isPressButton && _currentTime >= timeBetweenShots)
            {
                Shot();
                _currentTime = 0;
            }
        }

        private void Shot()
        {
            var projectile = pool.Get();
            projectile.IsPool = false;
            projectile.transform.SetParent(null);
            projectile.transform.position = pointShoot.position;
            projectile.SetLinearVelocity(pointShoot.right * speedMovingFire , maxDistance);
            projectile.OnEndToDistance = ph =>
            {
                if(ph.IsPool)
                    return;
                    
                ph.IsPool = true;
                pool.Release(ph);
            };

            projectile.TriggerEnter2D = (ph, cd) =>
            {
                if(ph.IsPool)
                    return;
                    
                var target = cd.GetComponent<ITarget>();
                if (target != null)
                {
                    if (target is { IsCanByTarget: true })
                    {
                        target.Health.CurrentHealth.Value -= damage;
                        ph.IsPool = true;
                        pool.Release(ph);
                    }
                }
            };
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            _isPressButton = context.performed;
        }
    }
}