using System;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemyFactory
    {
        private EnemyPool _flyEnemy;
        private EnemyPool _groundEnemy;

        [Inject]
        public void Construct(
            [Inject(Id = EnemyType.Fly)] EnemyPool flyEnemy , 
            [Inject(Id = EnemyType.Ground)]EnemyPool groundEnemy)
        {
            _flyEnemy = flyEnemy;
            _groundEnemy = groundEnemy;
        }
        
        public Enemy Create(EnemyType enemyType)
        {
            return enemyType switch
            {
                EnemyType.Fly => _flyEnemy.Spawn(),
                EnemyType.Ground => _groundEnemy.Spawn(),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
            };
        }
        
        public void Relase(Enemy enemy)
        {
            switch (enemy.EnemyType)
            {
                case EnemyType.Fly:
                    _flyEnemy.Despawn(enemy);
                    break;
                case EnemyType.Ground:
                    _groundEnemy.Despawn(enemy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemy.EnemyType), enemy.EnemyType, null);
            }
        }
    }
}