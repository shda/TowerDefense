using TowerDefense.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings gameSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings);
        }
    }
}