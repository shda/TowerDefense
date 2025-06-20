using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public class ProjectileGunTowerBase : TowerBase
    {
        [SerializeField] protected WeaponSingleShot weapon;
        private ITarget _currentTarget;
        private readonly CompositeDisposable  _disposables = new();

        private void Start()
        {
            weapon.OnProjectileCompleteMove = OnHitToTarget;
        }

        protected virtual void OnHitToTarget(ITarget target)
        {
            
        }

        protected override void CallDetect()
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
                    
                    if(!IsCanUsingByTarget(target))
                        continue;
                    
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

        protected virtual bool IsCanUsingByTarget(ITarget target)
        {
            return true;
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