using System;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class GunTower : TowerBase
    {
        [SerializeField] protected WeaponSingleShot weapon;
        [SerializeField] private float damage;
        
        private IGunTarget _currentTarget;
        private readonly CompositeDisposable  _disposables = new();

        private void Start()
        {
            weapon.OnProjectileCompleteMove = pr =>
            {
                if (pr is { IsCanByTarget: true })
                {
                    pr.Health.CurrentHealth.Value -= damage;
                }
            };
        }

        protected override void CallDetect()
        {
            var targets = enemyDetector.Detect();
            if (targets is { Length: > 0 })
            {
                foreach (var enemy in targets)
                {
                    var target = enemy.transform.GetComponent<IGunTarget>();
                    if (target is { IsCanByTarget: true })
                    {
                        _currentTarget = target;
                        _disposables.Clear();
                        _currentTarget.OnDead.Subscribe(unit =>
                        {
                            _currentTarget = null;
                        }).AddTo(_disposables);
                        break;
                    }
                }
            }
        }
        
        protected override void CallFire()
        {
            if (_currentTarget is { IsCanByTarget: true })
            {
                weapon.ShotTo(_currentTarget);
            }
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}