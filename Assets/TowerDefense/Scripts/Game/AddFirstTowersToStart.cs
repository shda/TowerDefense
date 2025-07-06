using System;
using System.Threading;
using BlockBlast.Scripts.Common.Extensions;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using TowerDefense.Scripts.Ui.UpLevel;
using UnityEngine;

namespace TowerDefense.Scripts.Game
{
    public class AddFirstTowersToStart : MonoBehaviour
    {
        [SerializeField] private int countTowersInMenu = 3;
        [SerializeField] private UpPlayerStatsCollection upPlayerStatsCollection;
        [SerializeField] private TowerSettingsCollection towerSettingsCollection;
        [SerializeField] private WindowLevelUpPlayer windowLevelUpPlayer;

        private CancellationTokenSource _tokenSource;

        public int CountStartTowers { get; set; }

        private void Start()
        {
            windowLevelUpPlayer.Hide();
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }

        public void StartLevel()
        {
            StartGame().Forget();
        }

        private async UniTask StartGame()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            await ShowWindowAddTowers(_tokenSource);
        }

        private async UniTask ShowWindowAddTowers(CancellationTokenSource tokenSource)
        {
            for (var i = 0; i < CountStartTowers; i++)
            {
                windowLevelUpPlayer.CreateRandomTowers(countTowersInMenu);
                await UniTask.WaitUntil(() => windowLevelUpPlayer.gameObject.activeSelf);
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: tokenSource.Token);

                if (tokenSource.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}