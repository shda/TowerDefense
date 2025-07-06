using System;
using System.Linq;
using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Game;
using TowerDefense.Scripts.Stats;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemySpawner : ITickable
    {
        private EnemyFactory _enemyFactory;

        private PlayerLevelStats _playerLevelStats;
        private LevelController _levelController;
        private LevelObjects _levelObjects;
        private GameStates _gameStates;

        public Action<Enemy> OnCreateNewEnemy;
        public Action<Enemy> OnRemoveEnemy;
        public void Tick()
        {
            var tempList = UnityEngine.Pool.ListPool<Enemy>.Get();
            tempList.AddRange(_levelObjects.Enemies);
            foreach (var enemy in tempList)
            {
                enemy.CustomUpdate();
            }

            UnityEngine.Pool.ListPool<Enemy>.Release(tempList);
        }

        [Inject]
        public void Construct(
            LevelController levelController,
            EnemyFactory factory,
            PlayerLevelStats playerLevelStats,
            LevelObjects levelObjects ,
            GameStates gameStates)
        {
            _gameStates = gameStates;
            _levelController = levelController;
            _enemyFactory = factory;
            _playerLevelStats = playerLevelStats;
            _levelObjects = levelObjects;
        }

        public Enemy AddEnemyToMap(EnemyType type)
        {
            if (!_levelController.IsMapLoaded)
            {
                return null;
            }

            var spawnPoint = _levelController.EnemySpawnPoints
                [Random.Range(0, _levelController.EnemySpawnPoints.Length)];

            var onePoint = _levelController.EnemySpawnPoints[0].WorldPosition;
            var twoPoint = _levelController.EnemySpawnPoints[1].WorldPosition;
            var randomX = Random.Range(onePoint.x, twoPoint.x);
            var randomY = Random.Range(0, 0.5f);
            var spawnPosition = new Vector3(randomX, spawnPoint.WorldPosition.y + randomY, 0);

            var enemy = _enemyFactory.Create(type);
            enemy.Reset();
            enemy.SetStartPosition(spawnPosition);
            enemy.SetMovingPath(_levelController.PlayerBase.WorldPosition);
            enemy.Show();

            CompositeDisposable composite = new();
            enemy.Status.Subscribe(_ => OnChangeEnemyStatus(enemy, composite)).AddTo(composite);

            OnCreateNewEnemy?.Invoke(enemy);
            _levelObjects.Enemies.Add(enemy);
            _gameStates.CountEnemy.Value = _levelObjects.Enemies.Count();

            return enemy;
        }

        private void OnChangeEnemyStatus(Enemy enemy, CompositeDisposable composite)
        {
            if (enemy.Status.Value == EnemyActionStatus.Dead)
            {
                _playerLevelStats.ProgressPoints.Value += enemy.Settings.givesPointsAftetDeath;
                ReturnEnemyToPool(enemy, composite);
            }
            else if (enemy.Status.Value == EnemyActionStatus.Complete)
            {
                _gameStates.CallEnemyReachedPlayerBase();
                ReturnEnemyToPool(enemy, composite);
            }
        }

        private void ReturnEnemyToPool(Enemy enemy, CompositeDisposable composite)
        {
            composite.Dispose();
            _enemyFactory.Release(enemy);
            _levelObjects.Enemies.Remove(enemy);
            OnRemoveEnemy?.Invoke(enemy);
        }
    }
}