using TowerDefense.Scripts.PlayerScripts;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class FindAndAttackPlayer : MonoBehaviour
    {
        private float _currentTime;
        private Player _player;
        private PlayerHealth _playerHealth;
        private EnemySettings _settings;

        public bool IsCanHitPlayer =>
            Vector3.Distance(transform.position, _player.transform.position) < _settings.radiusAttack;

        [Inject]
        public void Construct(Player player, PlayerHealth playerHealth)
        {
            _player = player;
            _playerHealth = playerHealth;
        }

        public void SetInfo(EnemySettings settings)
        {
            _settings = settings;
        }

        public void CustomUpdate()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime < _settings.timeBetweenShots)
            {
                return;
            }

            if (IsCanHitPlayer)
            {
                _currentTime = 0;
                HitPlayer();
            }
        }

        private void HitPlayer()
        {
            _playerHealth.Health.CurrentHealth.Value -= _settings.hitToPlayer;
        }
    }
}