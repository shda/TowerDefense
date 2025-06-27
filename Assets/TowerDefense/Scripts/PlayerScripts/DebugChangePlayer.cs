
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class DebugChangePlayer : MonoBehaviour
    {
        public float multiplierRadiusShot = 1.0f;
        public float multiplierDamageShot = 1.0f;
        public float multiplierHealth = 1.0f;
        public int countTargetOneShot = 3;
        
        public float downHealth = 5.0f;
        
        private PlayerStats _playerStats;
        private PlayerHealth _playerHealth;

        [Inject]
        public void Construct(PlayerStats playerStats , PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerStats = playerStats;
        }
        
        [Button]
        private void ChangeStats()
        {
            _playerStats.MultiplierRadiusShot.Value = multiplierRadiusShot;
            _playerStats.MultiplierDamageShot.Value = multiplierDamageShot;
            _playerStats.MultiplierHealth.Value = multiplierHealth;
            _playerStats.CountTargetOneShot.Value = countTargetOneShot;
        }

        [Button]
        private void DownHealth()
        {
            _playerHealth.Health.CurrentHealth.Value -= downHealth;
        }
    }
}