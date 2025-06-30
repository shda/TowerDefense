using System.Collections.Generic;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Game;
using TowerDefense.Scripts.PlayerScripts;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class HealthLevels : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;

        private HealthLevel _platerHealthLevel;
        private EnemySpawner _enemySpawner;
        private HealthLevel.Factory _healthLevelFactory;
        private PlayerHealth _playerHealth;
        private Player _player;
        
        private readonly Dictionary<Enemy, HealthLevel> _healthLevels = new();

        [Inject]
        public void Construct(
            EnemySpawner enemySpawner, 
            LevelObjects levelObjects,
            HealthLevel.Factory healthLevelFactory,
            PlayerHealth playerHealth,
            Player player)
        {
            _player = player;
            _enemySpawner = enemySpawner;
            _healthLevelFactory = healthLevelFactory;
            _playerHealth = playerHealth;

            _enemySpawner.OnCreateNewEnemy = CreateHealthLevelIfNeed;
            _enemySpawner.OnRemoveEnemy = RemoveHealthLevel;

            foreach (var enemy in levelObjects.Enemies)
            {
                CreateHealthLevelIfNeed(enemy);
            }
        }

        private void CreateHealthLevelIfNeed(Enemy enemy)
        {
            if (_healthLevels.ContainsKey(enemy))
                return;

            _healthLevels[enemy] = _healthLevelFactory.Create();
        }

        private void RemoveHealthLevel(Enemy enemy)
        {
            if (_healthLevels.TryGetValue(enemy, out HealthLevel level))
            {
                level.RemoveToPool();
                _healthLevels.Remove(enemy);
            }
        }

        private void Update()
        {
            UpdateValuesAndPositions();
            UpdatePlayerHealth();
        }

        private void UpdatePlayerHealth()
        {
            if (_platerHealthLevel == null)
            {
                _platerHealthLevel = _healthLevelFactory.Create();
            }

            SetLife(_playerHealth.Health, _platerHealthLevel, _player.transform.position);
        }

        private void UpdateValuesAndPositions()
        {
            foreach (var healthLevel in _healthLevels)
            {
                var enemy = healthLevel.Key;
                var hl = healthLevel.Value;
                SetLife(enemy.Health, hl, enemy.transform.position);
            }
        }

        private void SetLife(Health health , HealthLevel healthLevel , Vector3 position)
        {
            if (health.CurrentHealth.Value >= health.MaxHealth.Value)
            {
                healthLevel.gameObject.SetActive(false);
                return;
            }
            
            healthLevel.gameObject.SetActive(true);
            healthLevel.SetLife(
                health.CurrentHealth.Value,
                health.MaxHealth.Value);
            healthLevel.UpdatePositon(canvasRectTransform, position);
        }
    }
}