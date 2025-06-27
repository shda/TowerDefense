using System;
using UniRx;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerHealth : IDisposable
    {
        public Health Health { get; set; } = new();
        
        private readonly PlayerSettings _playerSettings;
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerHealth(
            PlayerStats playerStats , 
            PlayerSettings playerSettings )
        {
            _playerSettings = playerSettings;
            
            playerStats.MultiplierHealth.Subscribe(OnMultiplierHealth).AddTo(_disposables);
            
            Health.CurrentHealth.Value = playerSettings.StartHealth;
            Health.MaxHealth.Value = playerSettings.StartHealth;
        }

        private void OnMultiplierHealth(float multiplier)
        {
            Health.MaxHealth.Value = _playerSettings.StartHealth * multiplier;
            Health.CurrentHealth.Value = Health.MaxHealth.Value;
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}