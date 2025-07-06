using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Stats;
using TowerDefense.Scripts.Ui.UpLevel;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Game
{
    public class PlayerUpLevelHandler : MonoBehaviour
    {
        [SerializeField] private UpPlayerStatsCollection upPlayerStatsCollection;
        [SerializeField] private WindowLevelUpPlayer windowLevelUpPlayer;
        private PlayerLevelStats _playerLevelStats;

        [Inject]
        public void Construct(PlayerLevelStats playerLevelStats)
        {
            _playerLevelStats = playerLevelStats;
        }
        
        private void Start()
        {
            _playerLevelStats.LevelUp.Subscribe(OnLevelUp).AddTo(this);
            windowLevelUpPlayer.Hide();
        }
        
        private void OnLevelUp(int level)
        {
            if (level % 2 == 0)
            {
                windowLevelUpPlayer.CreateRandomTowers(3);
            }
            else
            {
                windowLevelUpPlayer. CreateRandomTowers(2);

                var random = Random.Range(0f, 100f);
                if (random > 50)
                {
                    windowLevelUpPlayer.CreateClearCellItem();
                }
                else
                {
                    windowLevelUpPlayer.CreatePlayerUpItem(upPlayerStatsCollection.GetRandomUpPlayerStats());
                }
            }
        }
    }
}