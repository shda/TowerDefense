using TMPro;
using TowerDefense.Scripts.Stats;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class LevelLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText; 
        
        private GameStats _gameStats;
        
        [Inject]
        public void Construct(GameStats gameStats)
        {
            _gameStats = gameStats;
        }

        private void Start()
        {
            _gameStats.Level.Subscribe(OnChangeLevel).AddTo(this);
        }

        private void OnChangeLevel(int level)
        {
            levelText.text = level.ToString();
        }
    }
}