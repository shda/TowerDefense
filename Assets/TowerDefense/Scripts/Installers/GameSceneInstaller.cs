using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] private Enemy enemyPrefab;

        [Header("Settings")]
        [SerializeField] private GameSettings gameSettings;

        [Header("Scene dependencies")]
        [SerializeField] private MapLoader mapLoader;

        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings);
            Container.BindInstance(mapLoader);

            Container.BindInterfacesAndSelfTo<EnemyController>().AsSingle();
            Container.Bind<EnemyMapPath>().AsSingle();

            BindEnemyFactory();
        }

        private void BindEnemyFactory()
        {
            Container.BindFactory<Enemy, EnemyPool>().FromPoolableMemoryPool<Enemy, MemoryEnemyPool>(poolBinder =>
                poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(enemyPrefab)
                    .UnderTransformGroup("EnemyPool"));
        }

        private class MemoryEnemyPool : MonoPoolableMemoryPool<IMemoryPool, Enemy>
        {
        }
    }
}
