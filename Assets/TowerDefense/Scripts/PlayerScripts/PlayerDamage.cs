using System;
using TowerDefense.Scripts.Enemies;
using UniRx;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerDamage : MonoBehaviour
    {
        [SerializeField] private DamageAnimation damage;
        private PlayerHealth _playerHealth;
        private float _lastHealth;

        [Inject]
        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void Start()
        {
            _lastHealth = _playerHealth.Health.CurrentHealth.Value;
            _playerHealth.Health.CurrentHealth.Subscribe(PlayDamage).AddTo(this);
        }

        private void PlayDamage(float health)
        {
            if(_lastHealth <= health)
            {
                _lastHealth = health;
                return;
            }
            _lastHealth = health;
            
            damage.Play();
        }
    }
}