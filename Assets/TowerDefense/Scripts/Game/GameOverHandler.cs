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
        [SerializeField] private EnemyWavesHandler enemyWavesHandler;
        private PlayerHealth _playerHealth;

        private void Start()
        {
            enemyWavesHandler.OnEnemyHitToBase = OnEnemyHitToBase;
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