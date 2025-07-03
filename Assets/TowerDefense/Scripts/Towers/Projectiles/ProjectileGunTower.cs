using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Towers.TowersTypes;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Towers.Projectiles
{
    public class ProjectileGunTowerBase : TowerBase
    {
        [SerializeField] protected WeaponSingleShot weapon;
        private readonly CompositeDisposable _disposables = new();
        private ITarget _currentTarget;

        private void Start()
        {
            weapon.OnProjectileCompleteMove = OnHitToTarget;
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        protected virtual void OnHitToTarget(ITarget target)
        {
        }

        protected override void CallDetect()
        {
            if (_currentTarget != null)
                return;

            var targets = towerComponents.EnemyDetector.Detect();
            if (targets is { Length: > 0 })
                foreach (var enemy in targets)
                {
                    var target = enemy.transform.GetComponent<ITarget>();

                    if (target == null)
                        continue;

                    if (!IsCanUsingByTarget(target))
                        continue;

                    if (target is { IsCanByTarget: true })
                    {
                        _currentTarget = target;
                        _disposables.Clear();
                        _currentTarget.Status.Subscribe(status =>
                        {
                            if (status == EnemyActionStatus.Dead) _currentTarget = null;
                        }).AddTo(_disposables);
                        break;
                    }
                }
        }

        protected virtual bool IsCanUsingByTarget(ITarget target)
        {
            return true;
        }

        protected override void CallFire()
        {
            if (_currentTarget != null &&
                !towerComponents.EnemyDetector.CheckStillDetect(_currentTarget.Transform.gameObject))
            {
                RemoveTarget();
                return;
            }

            if (_currentTarget is { IsCanByTarget: true }) weapon.ShotTo(_currentTarget);
        }

        private void RemoveTarget()
        {
            _disposables.Clear();
            _currentTarget = null;
        }
    }
}