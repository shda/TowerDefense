using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Stats;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TowerDefense.Scripts.Game
{
    public class EnemyWavesHandler : MonoBehaviour
    {
        [SerializeField] private Vector2 timeSpawn = new(2, 3);
        [SerializeField] private EnemySettings[] groupOfEnemies;
        [SerializeField] private EnemySettings[] flyEnemies;
        private EnemySpawner _enemySpawner;
        private GameStats _gameStats;

        private CancellationTokenSource _tokenSource;

        public Action OnEnemyHitToBase { get; set; }

        private void Start()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            //  DelayAddEnemyToMap(_tokenSource.Token).Forget();
        }


        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }

        [Inject]
        public void Construct(EnemySpawner enemySpawner, GameStats gameStats)
        {
            _gameStats = gameStats;
            _enemySpawner = enemySpawner;
        }

        private async UniTask DelayAddEnemyToMap(CancellationToken token)
        {
            var enemies = new List<EnemySettings>();
            enemies.AddRange(groupOfEnemies);
            enemies.AddRange(flyEnemies);

            while (!token.IsCancellationRequested)
            {
                var level = _gameStats.Level.Value;
                var maxIndex = 3 + level;

                maxIndex = Mathf.Clamp(maxIndex, 0, enemies.Count - 1);
                var randomIndex = Random.Range(0, maxIndex + 1);

                CreateEnemy(enemies[randomIndex]);
                await UniTask.Delay(TimeSpan.FromSeconds(
                    Random.Range(timeSpawn.x, timeSpawn.y)), cancellationToken: token);

                timeSpawn.x -= 0.01f;
                if (timeSpawn.x < 0)
                    timeSpawn.x = 0.3f;

                timeSpawn.y -= 0.01f;
                if (timeSpawn.y < 0)
                    timeSpawn.y = 0.3f;
            }
        }

        private void CreateEnemy(EnemySettings settings)
        {
            var enemy = _enemySpawner.AddEnemyToMap(settings.enemyType);
            enemy.SetInfo(settings);
            enemy.OnDestinationReached = OnHitToBase;
        }

        private void OnHitToBase(Enemy enemy)
        {
            OnEnemyHitToBase?.Invoke();
        }
    }
}