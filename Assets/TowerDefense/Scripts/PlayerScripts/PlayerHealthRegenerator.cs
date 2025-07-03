using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerHealthRegenerator : ITickable
    {
        private readonly PlayerHealth _playerHealth;
        private readonly PlayerSettings _settings;

        private float _currentTime;

        public PlayerHealthRegenerator(
            PlayerHealth playerHealth,
            PlayerSettings settings)
        {
            _playerHealth = playerHealth;
            _settings = settings;
            _currentTime = 0;
        }

        public void Tick()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _settings.DelayRecoveryHealth)
            {
                _currentTime = 0;
                if (_playerHealth.Health.CurrentHealth.Value < _playerHealth.Health.MaxHealth.Value)
                {
                    _playerHealth.Health.CurrentHealth.Value += _settings.UpHealth;
                    if (_playerHealth.Health.CurrentHealth.Value > _playerHealth.Health.MaxHealth.Value)
                    {
                        _playerHealth.Health.CurrentHealth.Value = _playerHealth.Health.MaxHealth.Value;
                    }
                }
            }
        }
    }
}