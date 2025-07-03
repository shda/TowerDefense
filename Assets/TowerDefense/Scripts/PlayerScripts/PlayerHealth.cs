using System;
using TowerDefense.Scripts.Common;
using UniRx;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerHealth : IDisposable
    {
        private readonly CompositeDisposable _disposables = new();

        private readonly PlayerSettings _playerSettings;

        public PlayerHealth(
            PlayerModifiers playerModifiers,
            PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;

            playerModifiers.MultiplierHealth.Subscribe(OnMultiplierHealth).AddTo(_disposables);

            Health.CurrentHealth.Value = playerSettings.StartHealth;
            Health.MaxHealth.Value = playerSettings.StartHealth;
        }

        public Health Health { get; set; } = new();

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