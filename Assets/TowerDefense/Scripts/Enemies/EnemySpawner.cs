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
        private PathfinderToTileMap _pathfinderToTileMap;
        private LevelController _levelController;
        private Enemy.Factory _enemyFactory;
      //  private Vector3Int[] _spawnPoints;
        private GameStats _gameStats;
        private LevelObjects _levelObjects;
        
        public Action<Enemy> OnCreateNewEnemy;
        public Action<Enemy> OnRemoveEnemy;
        
        private PathCollections _pathCollections;

        [Inject]
        public void Construct(
            PathCollections pathCollections , 
            LevelController levelController, 
            Enemy.Factory factory,
            GameStats gameStats,
            LevelObjects  levelObjects)
        {
            _levelController = levelController;
            _enemyFactory = factory;
            _pathCollections = pathCollections;
            _gameStats = gameStats;
            _levelObjects = levelObjects;
        }

        public void AddEnemyToMap()
        {
            if(!_levelController.IsMapLoaded)
                return;
            
            var spawnPoint = _levelController.EnemySpawnPoints
                [Random.Range(0, _levelController.EnemySpawnPoints.Length)];
            var movingPath = _pathCollections.FindPath(spawnPoint.Position);
            
            var enemy = _enemyFactory.Create();
            enemy.SetStartPosition(spawnPoint.WorldPosition);
            
            enemy.SetMovingPath(_levelController.PlayerBase.WorldPosition);
            enemy.Reset();

            CompositeDisposable composite = new();
            enemy.OnDead.Subscribe(_ => OnDead(enemy , composite)).AddTo(composite);
            
            OnCreateNewEnemy?.Invoke(enemy);
            _levelObjects.Enemies.Add(enemy);
            _gameStats.CountEnemy.Value = _levelObjects.Enemies.Count();
        }

        private void OnDead(Enemy enemy, CompositeDisposable composite)
        {
            if (enemy.IsDead)
            {
                _gameStats.ProgressNextTower.Value += 0.02f;
            }
            
            composite.Dispose();
            enemy.RemoveToPool();
            
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