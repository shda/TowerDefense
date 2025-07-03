using TowerDefense.Scripts.PlayerScripts;
using TowerDefense.Scripts.Ui.GameSceneUi;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Game
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private GameOverWindow gameOverWindow;
        private PlayerHealth _playerHealth;

        private void Start()
        {
            _playerHealth.Health.CurrentHealth.Subscribe(OnPlayerChangeHp).AddTo(this);
        }

        [Inject]
        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void OnPlayerChangeHp(float value)
        {
            if (value <= 0) ShowGameOver();
        }

        private void OnEnemyHitToBase()
        {
            ShowGameOver();
        }

        private void ShowGameOver()
        {
            gameOverWindow.ShowWindow();
        }
    }
}