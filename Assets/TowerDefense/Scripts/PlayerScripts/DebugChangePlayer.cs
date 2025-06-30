
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
        
        private PlayerModifiers _playerModifiers;
        private PlayerHealth _playerHealth;

        [Inject]
        public void Construct(PlayerModifiers playerModifiers , PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerModifiers = playerModifiers;
        }
        
        [Button]
        private void ChangeStats()
        {
            _playerModifiers.MultiplierRadiusShot.Value = multiplierRadiusShot;
            _playerModifiers.MultiplierDamageShot.Value = multiplierDamageShot;
            _playerModifiers.MultiplierHealth.Value = multiplierHealth;
            _playerModifiers.CountTargetOneShot.Value = countTargetOneShot;
        }

        [Button]
        private void DownHealth()
        {
            _playerHealth.Health.CurrentHealth.Value -= downHealth;
        }
    }
}