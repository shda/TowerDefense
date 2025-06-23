using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Enemies.Effects;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Settings;
using TowerDefense.Scripts.Stats;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Ui;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private HealthLevel healthLevelPrefab;
        [SerializeField] private Transform rootHealthLevels;

        [Header("Settings")]
        [SerializeField] private GameSettings gameSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings);
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.Bind<EnemyMapPath>().AsSingle();
            Container.Bind<GameStats>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileSpawner>().AsSingle();
            Container.Bind<InputListenerSwitcher>().AsSingle();
            Container.Bind<InputSwitcher>().AsSingle();
            Container.Bind<LevelObjects>().AsSingle();
            
            Container.Bind<LevelController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle();
            
            
            Container.BindInterfacesAndSelfTo<EffectHandler>().AsTransient();
            Container.Bind<MovingToPathHandler>().AsTransient();
            Container.Bind<Health>().AsTransient();

            BindEnemyFactory();
            BindHealthLevelFactory();
        }

        private void BindEnemyFactory()
        {
            Container.BindFactory<Enemy, Enemy.Factory>().FromPoolableMemoryPool<Enemy, MemoryEnemyPool>(poolBinder =>
                poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(enemyPrefab)
                    .UnderTransformGroup("Enemy"));
        }
        
        private void BindHealthLevelFactory()
        {
            Container.BindFactory<HealthLevel, HealthLevel.Factory>()
                .FromPoolableMemoryPool<HealthLevel, HealthLevel.MemoryPool>(poolBinder =>
                    poolBinder
                        .WithInitialSize(20)
                        .FromComponentInNewPrefab(healthLevelPrefab)
                        .UnderTransform(rootHealthLevels));
        }

        private class MemoryEnemyPool : MonoPoolableMemoryPool<IMemoryPool, Enemy>
        {
        }
    }
}
