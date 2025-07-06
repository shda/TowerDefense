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
        private GameStates _gameStates;

        private void Start()
        {
            _gameStates.CountEnemy.Subscribe(i => { label.text = i.ToString(); }).AddTo(this);
        }

        [Inject]
        public void Construct(GameStates gameStates)
        {
            _gameStates = gameStates;
        }
    }
}