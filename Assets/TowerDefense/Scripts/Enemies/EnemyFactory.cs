using System;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemyFactory
    {
        private EnemyPool _flyEnemiesPool;
        private EnemyPool _groundEnemiesPool;

        [Inject]
        public void Construct(
            [Inject(Id = EnemyType.Fly)] EnemyPool flyEnemiesPool , 
            [Inject(Id = EnemyType.Ground)]EnemyPool groundEnemiesPool)
        {
            _flyEnemiesPool = flyEnemiesPool;
            _groundEnemiesPool = groundEnemiesPool;
        }
        
        public Enemy Create(EnemyType enemyType)
        {
            return enemyType switch
            {
                EnemyType.Fly => _flyEnemiesPool.Spawn(),
                EnemyType.Ground => _groundEnemiesPool.Spawn(),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
            };
        }
        
        public void Release(Enemy enemy)
        {
            switch (enemy.EnemyType)
            {
                case EnemyType.Fly:
                    _flyEnemiesPool.Despawn(enemy);
                    break;
                case EnemyType.Ground:
                    _groundEnemiesPool.Despawn(enemy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemy.EnemyType), enemy.EnemyType, null);
            }
        }
    }
}