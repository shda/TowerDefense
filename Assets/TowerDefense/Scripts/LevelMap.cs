using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.TileMap;
using TowerDefense.Scripts.Towers;
using UnityEngine;

namespace TowerDefense.Scripts
{
    public class MapPath
    {
        private LevelMap _levelMap;
        
        public MapPath(LevelMap levelMap)
        {
            _levelMap =  levelMap;
        }
    }
    
    public class LevelMap : MonoBehaviour
    {
        [SerializeField] private TileMapLevel tileMapLevel;

        public MapPath MapPath { get; private set; }
        public TileMapLevel CurrentMap => tileMapLevel;
        public Vector3 FirstTargetPosition { get; private set; }
        public bool IsMapLoaded { get; private set; } = true;

        public HashSet<TowerBase> TowersToMap { get; set; } = new();
        
        private void Awake()
        {
            OnMapLoader();
        }
        
        private void OnMapLoader()
        {
            IsMapLoaded = true;
            
            MapPath =  new MapPath(this);
            FindTargetPoint();
        }

        private void FindTargetPoint()
        {
            var findTiles = tileMapLevel.LayerService.FindTilesByNameProperty("name" , "first_target");
            var firstTile = findTiles.First();
            FirstTargetPosition = firstTile.WorldPosition;
        }
    }
}
