using System;
using TowerDefense.Scripts.Common;
using TowerDefense.Scripts.Stats;
using UniRx;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerHealth : IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly PlayerSettings _playerSettings;
        private readonly GameStates _gameStates;
        public Health Health { get; set; } = new();
        public PlayerHealth(
            PlayerModifiers playerModifiers,
            PlayerSettings playerSettings,
            GameStates gameStates)
        {
            _gameStates = gameStates;
            _playerSettings = playerSettings;
            playerModifiers.MultiplierHealth.Subscribe(OnMultiplierHealth).AddTo(_disposables);
            Health.CurrentHealth.Subscribe(OnChangeHealth).AddTo(_disposables);
            
            Health.CurrentHealth.Value = playerSettings.StartHealth;
            Health.MaxHealth.Value = playerSettings.StartHealth;
        }

        private void OnChangeHealth(float count)
        {
            if (Health.CurrentHealth.Value <= 0)
            {
                _gameStates.CallPlayerDead();
            }
        }

        public void Dispose()
        {
            _disposables.Clear();
        }

        private void OnMultiplierHealth(float multiplier)
        {
            Health.MaxHealth.Value = _playerSettings.StartHealth * multiplier;
            Health.CurrentHealth.Value = Health.MaxHealth.Value;
        }
    }
}