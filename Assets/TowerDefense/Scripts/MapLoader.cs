using System.Linq;
using TowerDefense.Scripts.TileMap;
using UnityEngine;

namespace TowerDefense.Scripts
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField] private TileMapLevel tileMapLevel;

        public TileMapLevel CurrentMap => tileMapLevel;
        
        public Vector3 FirstTargetPosition { get; private set; }

        public bool IsMapLoaded { get; private set; } = true;
        
        private void Awake()
        {
            OnMapLoader();
        }
        
     

        private void OnMapLoader()
        {
            IsMapLoaded = true;
            var findTiles = tileMapLevel.LayerService.FindTilesByNameProperty("name" , "first_target");
            var firstTile = findTiles.First();
            FirstTargetPosition = firstTile.WorldPosition;
        }
    }
}
