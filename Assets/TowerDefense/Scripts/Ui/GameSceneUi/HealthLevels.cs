using System.Collections.Generic;
using TowerDefense.Scripts.Common;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Game;
using TowerDefense.Scripts.PlayerScripts;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class HealthLevels : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;

        private readonly Dictionary<Enemy, HealthLevel> _healthLevels = new();
        private EnemySpawner _enemySpawner;
        private HealthLevelPool _healthLevelFactory;

        private HealthLevel _platerHealthLevel;
        private Player _player;
        private PlayerHealth _playerHealth;

        private void Update()
        {
            UpdateValuesAndPositions();
            UpdatePlayerHealth();
        }

        [Inject]
        public void Construct(
            EnemySpawner enemySpawner,
            LevelObjects levelObjects,
            HealthLevelPool healthLevelFactory,
            PlayerHealth playerHealth,
            Player player)
        {
            _player = player;
            _enemySpawner = enemySpawner;
            _healthLevelFactory = healthLevelFactory;
            _playerHealth = playerHealth;

            _enemySpawner.OnCreateNewEnemy = CreateHealthLevelIfNeed;
            _enemySpawner.OnRemoveEnemy = RemoveHealthLevel;

            foreach (var enemy in levelObjects.Enemies) CreateHealthLevelIfNeed(enemy);
        }

        private void CreateHealthLevelIfNeed(Enemy enemy)
        {
            if (_healthLevels.ContainsKey(enemy))
                return;

            _healthLevels[enemy] = _healthLevelFactory.Spawn();
        }

        private void RemoveHealthLevel(Enemy enemy)
        {
            if (_healthLevels.TryGetValue(enemy, out var level))
            {
                _healthLevelFactory.Despawn(level);
                _healthLevels.Remove(enemy);
            }
        }

        private void UpdatePlayerHealth()
        {
            if (_platerHealthLevel == null) _platerHealthLevel = _healthLevelFactory.Spawn();

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

        private void SetLife(Health health, HealthLevel healthLevel, Vector3 position)
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