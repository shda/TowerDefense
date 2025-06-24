using System;
using TMPro;
using TowerDefense.Scripts.Stats;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class CountEnemyLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        private GameStats _gameStats;
        
        [Inject]
        public void Construct(GameStats gameStats)
        {
            _gameStats = gameStats;
        }

        private void Start()
        {
            _gameStats.CountEnemy.Subscribe(i =>
            {
                label.text = i.ToString();
            }).AddTo(this);
        }
    }
}