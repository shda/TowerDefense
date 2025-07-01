using System;
using TowerDefense.Scripts.Common;
using UniRx;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerHealth : IDisposable
    {
        public Health Health { get; set; } = new();
        
        private readonly PlayerSettings _playerSettings;
        private readonly CompositeDisposable _disposables = new();
        
        public PlayerHealth(
            PlayerModifiers playerModifiers , 
            PlayerSettings playerSettings )
        {
            _playerSettings = playerSettings;
            
            playerModifiers.MultiplierHealth.Subscribe(OnMultiplierHealth).AddTo(_disposables);
            
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