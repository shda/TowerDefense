using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.Stats;
using TowerDefense.Scripts.Utils;
using UniRx;
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

        public Action<Enemy> OnCreateNewEnemy;
        public Action<Enemy> OnRemoveEnemy;
        
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
            enemy.Reset();

            CompositeDisposable composite = new();
            enemy.OnDead.Subscribe(_ => OnDead(enemy , composite)).AddTo(composite);
            
            OnCreateNewEnemy?.Invoke(enemy);
            AllEnemies.Add(enemy);
            _gameStats.CountEnemy.Value = AllEnemies.Count;
        }

        private void OnDead(Enemy enemy, CompositeDisposable composite)
        {
            composite.Dispose();
            
            enemy.RemoveToPool();
            AllEnemies.Remove(enemy);
            OnRemoveEnemy?.Invoke(enemy);
        }
        
        public void Tick()
        {
            //todo pool array
            var enemies = AllEnemies.ToArray();
            foreach (var enemy in enemies)
            {
                enemy.CustomUpdate();
            }
        }
    }
}