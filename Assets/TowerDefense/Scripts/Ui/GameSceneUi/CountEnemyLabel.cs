using TMPro;
using TowerDefense.Scripts.Stats;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class CountEnemyLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        private GameStats _gameStats;

        private void Start()
        {
            _gameStats.CountEnemy.Subscribe(i => { label.text = i.ToString(); }).AddTo(this);
        }

        [Inject]
        public void Construct(GameStats gameStats)
        {
            _gameStats = gameStats;
        }
    }
}