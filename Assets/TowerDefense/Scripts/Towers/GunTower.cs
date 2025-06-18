using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Towers
{
    public class GunTower : TowerBase
    {
        [SerializeField] protected Weapon weapon;
        
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
    }
}