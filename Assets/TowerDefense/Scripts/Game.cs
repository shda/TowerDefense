using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Input;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TowerDefense.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Vector2 timeSpawn = new Vector2(2, 3);
        
        private EnemySpawner _enemySpawner;
        private CancellationTokenSource _tokenSource;
        private InputSwitcher _inputSwitcher;
        
        [Inject]
        public void Construct(EnemySpawner enemySpawner , InputSwitcher inputSwitcher)
        {
            _enemySpawner = enemySpawner;
            _inputSwitcher =  inputSwitcher;
        }
        
        private void Start()
        {
            _inputSwitcher.SwitchToPlayerControl();
            
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            DelayAddEnemyToMap(_tokenSource.Token).Forget();
        }
        
        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }

        private async UniTask DelayAddEnemyToMap(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _enemySpawner.AddEnemyToMap();
                await UniTask.Delay(TimeSpan.FromSeconds(
                    Random.Range(timeSpawn.x , timeSpawn.y)), cancellationToken: token);
            }
        }
    }
}