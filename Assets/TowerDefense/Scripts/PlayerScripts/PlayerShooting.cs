using BlockBlast.Scripts.Common;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Towers.Detectors;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerShooting : MonoBehaviour ,  IPressListener
    {
        [SerializeField] protected EnemyDetector enemyDetector;
        [SerializeField] private Transform pointShoot;
        [SerializeField] private float speedMovingFire = 10.0f;
        [SerializeField] private float maxDistance = 10.0f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float timeBetweenShots = 0.2f;
        
        [SerializeField] private PoolGameObjectsComponent<ProjectilePhysics> pool;
        
        private float _currentTime;
        private bool _isPressButton;
        private InputSwitcher _inputSwitcher;

        [Inject]
        public void Construct(InputSwitcher inputSwitcher)
        {
            _inputSwitcher = inputSwitcher;
            _inputSwitcher.SetShooting(this);
        }
        
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

        public void OnPress(bool isPressed)
        {
            _isPressButton = isPressed;
        }
    }
}