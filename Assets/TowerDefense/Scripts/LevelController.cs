using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Obstacles;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.TileMap;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private TileMapLevel tileMapLevel;
        private LevelObjects _levelObjects;
        public TileMapLevel CurrentMap => tileMapLevel;
        public bool IsMapLoaded { get; private set; } = true;
        public TileInfo[,] AllTiles { get; set; }
        public TileInfo[] EnemySpawnPoints { get; set; }
        public TileInfo PlayerBase { get; set; }
        public TileInfo FirstTargetTile { get; set; }
        
        private void Awake()
        {
            OnMapLoader();
        }

        [Inject]
        public void Construct(LevelObjects levelObjects)
        {
            _levelObjects = levelObjects;
        }
        
        private void OnMapLoader()
        {
            IsMapLoaded = true;
            FindAllPoints();
            FindAndAddObstacles();
        }

        private void FindAndAddObstacles()
        {
            var obstacles = tileMapLevel.GetComponentsInChildren<Obstacle>();
            foreach (var obstacle in obstacles)
            {
                _levelObjects.Obstacles.Add(obstacle);
            }
        }

        private void FindAllPoints()
        {
            AllTiles = tileMapLevel.LayerObstacles.GetAllTiles();
            
            FirstTargetTile = MapUtils.GetFirstTargetPoints(tileMapLevel).First();
            EnemySpawnPoints = MapUtils.GetEnemySpawnPoints(tileMapLevel);
            PlayerBase = MapUtils.GetTargetPoints(tileMapLevel).First();
        }
    }
}
