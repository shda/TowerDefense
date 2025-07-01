using System.Threading;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.PlayerScripts;
using TowerDefense.Scripts.Ui.GameSceneUi;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameOverWindow gameOverWindow;
        [SerializeField] private EnemyWavesHandler enemyWavesHandler;
        
        private CancellationTokenSource _tokenSource;
        private InputSwitcher _inputSwitcher;
        private PlayerHealth _playerHealth;

        [Inject]
        public void Construct(InputSwitcher inputSwitcher , PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _inputSwitcher =  inputSwitcher;
        }
        
        private void Start()
        {
            enemyWavesHandler.OnEnemyHitToBase = OnEnemyHitToBase;
            _inputSwitcher.SwitchToPlayerControl();
            
            _playerHealth.Health.CurrentHealth.Subscribe(OnPlayerChangeHp).AddTo(this);
        }

        private void OnPlayerChangeHp(float value)
        {
            if (value <= 0)
            {
                ShowGameOver();
            }
        }

        private void OnEnemyHitToBase()
        {
            ShowGameOver();
        }

        private void ShowGameOver()
        {
            gameOverWindow.ShowWindow();
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }
    }
}