using TMPro;
using TowerDefense.Scripts.Stats;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class PlayerLevelLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;

        private PlayerLevelStats _playerLevelStats;

        private void Start()
        {
            _playerLevelStats.Level.Subscribe(OnChangeLevel).AddTo(this);
        }

        [Inject]
        public void Construct(PlayerLevelStats playerLevelStats)
        {
            _playerLevelStats = playerLevelStats;
        }

        private void OnChangeLevel(int level)
        {
            levelText.text = level.ToString();
        }
    }
}