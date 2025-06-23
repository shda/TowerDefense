using System.Collections.Generic;
using TowerDefense.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class EnemiesHealthLevel : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;

        private EnemySpawner _enemySpawner;
        private HealthLevel.Factory _healthLevelFactory;
        private readonly Dictionary<Enemy, HealthLevel> _healthLevels = new();

        [Inject]
        public void Construct(
            EnemySpawner enemySpawner, 
            LevelObjects levelObjects,
            HealthLevel.Factory healthLevelFactory)
        {
            _enemySpawner = enemySpawner;
            _healthLevelFactory = healthLevelFactory;

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
        }

        private void UpdateValuesAndPositions()
        {
            foreach (var healthLevel in _healthLevels)
            {
                var enemy = healthLevel.Key;
                var hl = healthLevel.Value;

                if (enemy.Health.CurrentHealth.Value >= enemy.Health.MaxHealth.Value)
                {
                    hl.gameObject.SetActive(false);
                    continue;
                }

                hl.gameObject.SetActive(true);
                hl.SetLife(
                    enemy.Health.CurrentHealth.Value,
                    enemy.Health.MaxHealth.Value);
                hl.UpdatePositon(canvasRectTransform, enemy.transform.position);
            }
        }
    }
}