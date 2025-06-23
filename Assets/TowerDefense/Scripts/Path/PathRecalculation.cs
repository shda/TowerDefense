using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Path
{
    public class PathRecalculation : ITickable
    {
        private LevelObjects _levelObjects;
        private LevelController _levelController;

        [Inject]
        public void Construct(
            LevelController levelController , 
            LevelObjects levelObjects)
        {
            _levelController = levelController;
            _levelObjects = levelObjects;
        }

        public void Tick()
        {
            
        }
    }
}