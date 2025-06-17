using System;
using System.Collections.Generic;
using TowerDefense.Scripts.Path;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemyController : ITickable
    {
        public HashSet<Enemy> AllEnemies { get; set; } = new();

        private PathfinderToTileMap _pathfinderToTileMap;
        private EnemyMapPath _enemyMapPath;
        private MapLoader _mapLoader;
        private EnemyPool _enemyPool;

        [Inject]
        public void Construct(EnemyMapPath enemyMapPath , MapLoader mapLoader, EnemyPool enemyPool)
        {
            _mapLoader = mapLoader;
            _enemyPool = enemyPool;
            _enemyMapPath = enemyMapPath;
        }

        public void AddEnemyToMap()
        {
            if(!_mapLoader.IsMapLoaded)
                return;
            
            var movingPath = _enemyMapPath.GetPath(_mapLoader.CurrentMap);
            var newEnemy = _enemyPool.Create();
            newEnemy.SetMovingPath(movingPath);

            AllEnemies.Add(newEnemy);
        }

        public void Tick()
        {
            foreach (var enemy in AllEnemies)
            {
                enemy.CustomUpdate();
            }
        }
    }
}