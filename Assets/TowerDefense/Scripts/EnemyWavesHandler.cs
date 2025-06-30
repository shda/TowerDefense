using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using TowerDefense.Scripts.Enemies;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TowerDefense.Scripts
{
    public class EnemyWavesHandler : MonoBehaviour
    {
        [SerializeField] private Vector2 timeSpawn = new Vector2(2, 3);
        
        [SerializeField] private EnemySettings[] groupOfEnemies;
        [SerializeField] private EnemySettings[] flyEnemies;
        
        private CancellationTokenSource _tokenSource;
        private EnemySpawner _enemySpawner;
        
        [Inject]
        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }
        
        private void Start()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            DelayAddEnemyToMap(_tokenSource.Token).Forget();
        }
        
        private async UniTask DelayAddEnemyToMap(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                CreateEnemy(groupOfEnemies.First());
                await UniTask.Delay(TimeSpan.FromSeconds(
                    Random.Range(timeSpawn.x , timeSpawn.y)), cancellationToken: token);
            }
        }

        private void CreateEnemy(EnemySettings settings)
        {
            var enemy = _enemySpawner.AddEnemyToMap();
            enemy.SetInfo(settings);
        }
        
        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }
    }
}