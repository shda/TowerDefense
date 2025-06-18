using System;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class GunTower : TowerBase
    {
        [SerializeField] protected Weapon weapon;
        private readonly CompositeDisposable  _disposables = new();
        
        protected override void CallDetect()
        {
            if(_currentTarget is { IsCanByTarget: true })
                return;
            
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