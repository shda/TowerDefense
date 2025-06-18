using TowerDefense.Scripts.Enemies;
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

        [Header("Scene dependencies")]
        [SerializeField] private MapLoader mapLoader;

        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings);
            Container.BindInstance(mapLoader);

            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.Bind<EnemyMapPath>().AsSingle();
            Container.Bind<GameStats>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileSpawner>().AsSingle();

            Container.Bind<MovingHandler>().AsTransient();
            Container.Bind<Health>().AsTransient();

            BindEnemyFactory();
            BindProjectileFactory();
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
        
        private void BindProjectileFactory()
        {
            Container.BindFactory<Projectile, Projectile.Factory>().FromPoolableMemoryPool<Projectile, MemoryProjectilePool>(poolBinder =>
                poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(projectilePrefab)
                    .UnderTransformGroup("Projectile"));
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
        
        private class MemoryProjectilePool : MonoPoolableMemoryPool<IMemoryPool, Projectile>
        {
        }
    }
}
