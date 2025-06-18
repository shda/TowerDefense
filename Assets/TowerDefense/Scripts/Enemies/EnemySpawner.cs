using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.Stats;
using TowerDefense.Scripts.Utils;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemySpawner : ITickable
    {
        public HashSet<Enemy> AllEnemies { get; set; } = new();

        private PathfinderToTileMap _pathfinderToTileMap;
        private EnemyMapPath _enemyMapPath;
        private MapLoader _mapLoader;
        private Enemy.Factory _enemyFactory;
        private Vector3[] _spawnPoints;
        private GameStats _gameStats;

        [Inject]
        public void Construct(
            EnemyMapPath enemyMapPath , 
            MapLoader mapLoader, 
            Enemy.Factory factory,
            GameStats gameStats)
        {
            _mapLoader = mapLoader;
            _enemyFactory = factory;
            _enemyMapPath = enemyMapPath;
            _gameStats = gameStats;
        }

        public void AddEnemyToMap()
        {
            if(!_mapLoader.IsMapLoaded)
                return;

            if (_spawnPoints == null)
            {
                _spawnPoints = MapUtils.GetEnemySpawnPoints(_mapLoader.CurrentMap).
                    Select(x => x.WorldPosition).ToArray();
            }
            
            var movingPath = _enemyMapPath.GetPath(_mapLoader.CurrentMap);
            var enemy = _enemyFactory.Create();
            enemy.SetStartPosition(_spawnPoints[Random.Range(0, _spawnPoints.Length)]);
            enemy.SetMovingPath(_mapLoader.FirstTargetPosition , movingPath);
            enemy.OnCompleteMove = () =>
            {
                enemy.RemoveToPool();
                AllEnemies.Remove(enemy);
            };
            
            AllEnemies.Add(enemy);
            _gameStats.CountEnemy.Value = AllEnemies.Count;
        }

        public void Tick()
        {
            //todo pool
            var enemys = AllEnemies.ToArray();
            foreach (var enemy in enemys)
            {
                enemy.CustomUpdate();
            }
        }
    }
}