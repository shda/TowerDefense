using System;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Towers.Detectors;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerAutoShooting : MonoBehaviour
    {
        [SerializeField] protected EnemyDetector enemyDetector;
        [SerializeField] private float speedMovingProjectile = 10.0f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float timeBetweenShots = 0.2f;
        [SerializeField] private float delayDetectTarget = 0.3f;
        [SerializeField] private ProjectileSpawner projectilePool;

        public Action OnFire;
        
        private readonly CompositeDisposable  _disposables = new();
        private ITarget _currentTarget;
        private float _currentTime;
        private float _currentDetectTime;
        private void Update()
        {
            if (_currentTarget != null && !enemyDetector.CheckStillDetect(_currentTarget.Transform.gameObject))
            {
                _currentTarget = null;
            }
            
            _currentDetectTime += Time.deltaTime;
            if (_currentDetectTime >= timeBetweenShots)
            {
                _currentDetectTime = 0;
                DetectEnemy();
            }
            
            _currentTime += Time.deltaTime;
            if (_currentTime >= timeBetweenShots)
            {
                _currentTime = 0;
                ShotTo();
            }
        }

        private void ShotTo()
        {
            if (_currentTarget == null || !_currentTarget.IsCanByTarget)
            {
                return;
            }
            
            OnFire?.Invoke();
            
            var projectile =  projectilePool.Get();
            projectile.MoveToTarget(transform.position, _currentTarget , speedMovingProjectile);
            projectile.OnCompleteMove = pr =>
            {
                if (_currentTarget != null)
                {
                    _currentTarget.Health.CurrentHealth.Value -= damage;
                }
                
                projectilePool.ReturnToPool(pr);
            };
        }
        
        private void DetectEnemy()
        {
            _currentTarget = null;
            
            var targets = enemyDetector.Detect();
            if (targets is { Length: > 0 })
            {
                foreach (var enemy in targets)
                {
                    var target = enemy.transform.GetComponent<ITarget>();
                    
                    if(target == null)
                        continue;
                    
                    if (target is { IsCanByTarget: true })
                    {
                        _currentTarget = target;
                        _disposables.Clear();
                        _currentTarget.Status.Subscribe(status =>
                        {
                            if (status == EnemyStatus.Dead || 
                                status == EnemyStatus.Complete)
                            {
                                _currentTarget = null;
                            }
                            
                        }).AddTo(_disposables);
                        break;
                    }
                }
            }
        }
    }
}