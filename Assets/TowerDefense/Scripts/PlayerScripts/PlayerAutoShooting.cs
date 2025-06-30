using System;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Towers.Detectors;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerAutoShooting : MonoBehaviour
    {
        [SerializeField] protected EnemyDetector enemyDetector;
        [SerializeField] private float speedMovingProjectile = 10.0f;
        [SerializeField] private float damage = 10;
        [SerializeField] private float timeBetweenShots = 0.2f;
        [SerializeField] private float startDetectTime = 0.2f;
        [SerializeField] private ProjectileSpawner projectilePool;

        public Action OnFire;
        
        private float _currentTime;
        private float _currentDetectTime;
        private int _countTargetOneShot = 3;
        private float _damageMultiplier = 1.0f;

        [Inject]
        public void Construct(PlayerStats playerStats)
        {
            playerStats.CountTargetOneShot.Subscribe(count =>
            {
                _countTargetOneShot = count;
            }).AddTo(this);
            
            playerStats.MultiplierRadiusShot.Subscribe(multiplier =>
            {
                enemyDetector.SetMultiplierRadius(multiplier);
            }).AddTo(this);
            
            playerStats.MultiplierDamageShot.Subscribe(multiplier =>
            {
                _damageMultiplier = multiplier;
            }).AddTo(this);
        }

        private void Awake()
        {
            _currentTime = timeBetweenShots;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= timeBetweenShots)
            {
                if (ShotTo())
                {
                    _currentTime = 0;
                }
                else
                {
                    _currentTime = timeBetweenShots - startDetectTime;
                }
            }
        }
        
        private void ShotToTarget(ITarget target)
        {
            var projectile =  projectilePool.Get();
            projectile.MoveToTarget(transform.position, target , speedMovingProjectile);
            projectile.OnCompleteMove = pr =>
            {
                if (target is { IsCanByTarget: true })
                {
                    var damageComplete = damage * _damageMultiplier;
               //     UnityEngine.Debug.Log($"Player shot to target: {target.Transform.name}, damage: {damageComplete}");
                    target.Health.CurrentHealth.Value -= damageComplete;
                }
                
                projectilePool.ReturnToPool(pr);
            };
        }

        private bool ShotTo()
        {
            OnFire?.Invoke();
            bool isShot = false;
            
            var targets = enemyDetector.Detect();
            if (targets is { Length: > 0 })
            {
                var targetCountShot = _countTargetOneShot; 
                foreach (var enemy in targets)
                {
                    var target = enemy.transform.GetComponent<ITarget>();
                    
                    if(target == null)
                        continue;
                    
                    if (target is { IsCanByTarget: true })
                    {
                        isShot = true;
                        targetCountShot--;
                        ShotToTarget(target);
                        
                        if (targetCountShot <= 0)
                        {
                            break;
                        }
                    }
                }
            }

            return isShot;
        }
    }
}