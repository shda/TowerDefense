using System;
using System.Threading;
using BlockBlast.Scripts.Common.Extensions;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using TowerDefense.Scripts.Stats;
using TowerDefense.Scripts.Ui.UpLevel;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TowerDefense.Scripts.Game
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField] private int countStartTowers = 2;
        [SerializeField] private int countTowersInMenu = 3;
        [SerializeField] private UpPlayerStatsCollection upPlayerStatsCollection;
        [SerializeField] private TowerSettingsCollection towerSettingsCollection;
        [SerializeField] private WindowUpLevelPlayer windowUpLevelPlayer;

        private CancellationTokenSource _tokenSource;
        private GameStats _gameStats;

        [Inject]
        public void Construct(GameStats gameStats)
        {
            _gameStats = gameStats;
        }
        
        private void Start()
        {
            _gameStats.LevelUp.Subscribe(OnLevelUp).AddTo(this);
            windowUpLevelPlayer.Hide();
            StartGame().Forget();
        }

        private void OnLevelUp(int level)
        {
            if (level % 2 == 0)
            {
                CreateRandomTowers(3);
            }
            else
            {
                CreateRandomTowers(2);
                
                var random = Random.Range(0f, 100f);
                if (random > 50)
                {
                    windowUpLevelPlayer.CreateClearCellItem();
                }
                else
                {
                    windowUpLevelPlayer.CreatePlayerUpItem(upPlayerStatsCollection.GetRandomUpPlayerStats());
                }
            }
        }

        private async UniTask StartGame()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            await ShowWindowAddTowers(_tokenSource);
        }

        private async UniTask ShowWindowAddTowers(CancellationTokenSource tokenSource)
        {
            for (int i = 0; i < countStartTowers; i++)
            {
                CreateRandomTowers(countTowersInMenu);
                await UniTask.WaitUntil(() => windowUpLevelPlayer.gameObject.activeSelf);
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f) , cancellationToken: tokenSource.Token);
            
                if(tokenSource.IsCancellationRequested)
                    return;
            }
        }

        private void CreateRandomTowers(int count)
        {
            windowUpLevelPlayer.RemoveOldItems();
            var getShuffleTowers = towerSettingsCollection.GetShuffleTowers();
            for (int i = 0; i < count; i++)
            {
                windowUpLevelPlayer.CreateTowerItem(getShuffleTowers[i]);
            }
            windowUpLevelPlayer.Show();
        }
        

        private void OnDestroy()
        {
            _tokenSource.Cancel();
        }

#if UNITY_EDITOR
        private int _debugLevel = 1;
        [Button]
        private void DebugUpLevel()
        {
            OnLevelUp(++_debugLevel);
        }
#endif
    }
}