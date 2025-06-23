using System;
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
        //public HashSet<Enemy> AllEnemies { get; set; } = new();

        private PathfinderToTileMap _pathfinderToTileMap;
        private EnemyMapPath _enemyMapPath;
        private LevelController _levelController;
        private Enemy.Factory _enemyFactory;
        private Vector3[] _spawnPoints;
        private GameStats _gameStats;
        private LevelObjects _levelObjects;
        
        public Action<Enemy> OnCreateNewEnemy;
        public Action<Enemy> OnRemoveEnemy;
        
        [Inject]
        public void Construct(
            EnemyMapPath enemyMapPath , 
            LevelController levelController, 
            Enemy.Factory factory,
            GameStats gameStats,
            LevelObjects  levelObjects)
        {
            _levelController = levelController;
            _enemyFactory = factory;
            _enemyMapPath = enemyMapPath;
            _gameStats = gameStats;
            _levelObjects = levelObjects;
        }

        public void AddEnemyToMap()
        {
            if(!_levelController.IsMapLoaded)
                return;

            if (_spawnPoints == null)
            {
                _spawnPoints = MapUtils.GetEnemySpawnPoints(_levelController.CurrentMap).
                    Select(x => x.WorldPosition).ToArray();
            }
            
            var movingPath = _enemyMapPath.GetPath(_levelController.CurrentMap);
            var enemy = _enemyFactory.Create();
            enemy.SetStartPosition(_spawnPoints[Random.Range(0, _spawnPoints.Length)]);
            enemy.SetMovingPath(_levelController.FirstTargetTile.WorldPosition , movingPath);
            enemy.Reset();

            CompositeDisposable composite = new();
            enemy.OnDead.Subscribe(_ => OnDead(enemy , composite)).AddTo(composite);
            
            OnCreateNewEnemy?.Invoke(enemy);
            _levelObjects.Enemies.Add(enemy);
            _gameStats.CountEnemy.Value = _levelObjects.Enemies.Count();
        }

        private void OnDead(Enemy enemy, CompositeDisposable composite)
        {
            composite.Dispose();
            
            enemy.RemoveToPool();
            _levelObjects.Enemies.Remove(enemy);
            //_levelController.EnemiesToMap.Remove(enemy);
            _levelObjects.Enemies.Remove(enemy);
            OnRemoveEnemy?.Invoke(enemy);
        }
        
        public void Tick()
        {
            //todo pool array
            var enemies = _levelObjects.Enemies.ToArray();
            foreach (var enemy in enemies)
            {
                enemy.CustomUpdate();
            }
        }
    }
}