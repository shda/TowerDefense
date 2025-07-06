using TowerDefense.Scripts.Stats;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class LevelUpProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressBarImage;

        private PlayerLevelStats _playerLevelStats;

        private void Start()
        {
            _playerLevelStats.ProgressPoints.Subscribe(OnChangeProgress).AddTo(this);
        }

        [Inject]
        public void Construct(PlayerLevelStats playerLevelStats)
        {
            _playerLevelStats = playerLevelStats;
        }

        private void OnChangeProgress(int progress)
        {
            progressBarImage.fillAmount = _playerLevelStats.GetProgressToNextLevel01();
        }
    }
}