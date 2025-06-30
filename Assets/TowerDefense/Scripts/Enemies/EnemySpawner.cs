using System;
using System.Linq;
using BlockBlast.Scripts.Common.Extensions;
using Cysharp.Threading.Tasks;
using TowerDefense.Scripts.Game;
using TowerDefense.Scripts.Stats;
using UniRx;
using Zenject;
using Random = UnityEngine.Random;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemySpawner : ITickable
    {
        private LevelController _levelController;
        private EnemyFactory _enemyFactory;
        
        private GameStats _gameStats;
        private LevelObjects _levelObjects;
        
        public Action<Enemy> OnCreateNewEnemy;
        public Action<Enemy> OnRemoveEnemy;
        
        [Inject]
        public void Construct(
            LevelController levelController, 
            EnemyFactory factory,
            GameStats gameStats,
            LevelObjects  levelObjects)
        {
            _levelController = levelController;
            _enemyFactory = factory;
            _gameStats = gameStats;
            _levelObjects = levelObjects;
        }

        public Enemy AddEnemyToMap(EnemyType type)
        {
            if(!_levelController.IsMapLoaded)
                return null;
            
            var spawnPoint = _levelController.EnemySpawnPoints
                [Random.Range(0, _levelController.EnemySpawnPoints.Length)];
            
            var enemy = _enemyFactory.Create(type);
            enemy.Reset();
            enemy.SetStartPosition(spawnPoint.WorldPosition);
            enemy.SetMovingPath(_levelController.PlayerBase.WorldPosition);
            enemy.Show();

            CompositeDisposable composite = new();
            enemy.Status.Subscribe(_ => OnChangeEnemyStatus(enemy , composite)).AddTo(composite);
            
            OnCreateNewEnemy?.Invoke(enemy);
            _levelObjects.Enemies.Add(enemy);
            _gameStats.CountEnemy.Value = _levelObjects.Enemies.Count();

            return enemy;
        }

        private void OnChangeEnemyStatus(Enemy enemy, CompositeDisposable composite)
        {
            if (enemy.Status.Value == EnemyStatus.Dead)
            {
                _gameStats.ProgresscPoints.Value += enemy.Settings.givesPointsAftetDeath;
                ReturnEnemyToPool(enemy , composite);
            }
            else if (enemy.Status.Value == EnemyStatus.Complete)
            {
                ReturnEnemyToPool(enemy , composite);
            }
        }

        private void ReturnEnemyToPool(Enemy enemy, CompositeDisposable composite)
        {
            composite.Dispose();
            _enemyFactory.Relase(enemy);
            _levelObjects.Enemies.Remove(enemy);
            OnRemoveEnemy?.Invoke(enemy);
        }
        
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
    }
}