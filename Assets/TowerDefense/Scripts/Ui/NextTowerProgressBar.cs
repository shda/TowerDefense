using TowerDefense.Scripts.Stats;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class NextTowerProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressBarImage; 
        
        private GameStats _gameStats;
        
        [Inject]
        public void Construct(GameStats gameStats)
        {
            _gameStats = gameStats;
        }

        private void Start()
        {
            _gameStats.ProgressNextTower.Subscribe(OnChangeProgress).AddTo(this);
        }

        private void OnChangeProgress(int progress)
        {
            progressBarImage.fillAmount = _gameStats.GetProgressToNextLevel01();
        }
    }
}