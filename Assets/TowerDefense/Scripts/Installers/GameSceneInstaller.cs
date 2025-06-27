using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Enemies.Effects;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Settings;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Ui;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Header("Prefabs")] 
        [SerializeField] private Enemy enemyGroundPrefab;
        [SerializeField] private Enemy enemyFlyPrefab;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private HealthLevel healthLevelPrefab;
        [SerializeField] private Transform rootHealthLevels;

        [Header("Settings")]
        [SerializeField] private GameSettings gameSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings);
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileSpawner>().AsSingle();
            Container.Bind<InputListenerSwitcher>().AsSingle();
            Container.Bind<InputSwitcher>().AsSingle();
            Container.Bind<LevelObjects>().AsSingle();
            
            
            Container.Bind<LevelController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle();
            
            
            Container.BindInterfacesAndSelfTo<EffectHandler>().AsTransient();
            //Container.Bind<movingHandler>().AsTransient();
            Container.Bind<Health>().AsTransient();

            BindEnemyFactory();
            BindHealthLevelFactory();
        }

        private void BindEnemyFactory()
        {
            Container.Bind<EnemyFactory>().AsSingle();;
            
            Container.BindMemoryPool<Enemy, EnemyPool>()
                .WithId(EnemyType.Ground)
                .FromComponentInNewPrefab(enemyGroundPrefab)
                .UnderTransformGroup("Enemy");
            
            Container.BindMemoryPool<Enemy, EnemyPool>()
                .WithId(EnemyType.Fly)
                .FromComponentInNewPrefab(enemyFlyPrefab)
                .UnderTransformGroup("Enemy");
            
            /*
            Container.BindFactory<Enemy, Enemy.Factory>().FromPoolableMemoryPool<Enemy, MemoryEnemyPool>(poolBinder =>
                poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(enemyPrefab)
                    .UnderTransformGroup("Enemy"));
                    */
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
