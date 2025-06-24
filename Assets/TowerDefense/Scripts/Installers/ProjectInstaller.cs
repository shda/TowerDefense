using TowerDefense.Scripts.Stats;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStats>().AsSingle();
        }
    }
}
