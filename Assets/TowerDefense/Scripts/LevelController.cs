using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.TileMap;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Utils;
using UnityEngine;

namespace TowerDefense.Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private TileMapLevel tileMapLevel;
        private PathfinderToTileMap _pathfinderToTileMap;

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
        
        private void OnMapLoader()
        {
            IsMapLoaded = true;
            FindAllPoints();
            
            _pathfinderToTileMap = new PathfinderToTileMap(tileMapLevel);
        }

        private void FindAllPoints()
        {
            AllTiles = tileMapLevel.LayerObstacles.GetAllTiles();
            
            FirstTargetTile = MapUtils.GetFirstTargetPoints(tileMapLevel).First();
            EnemySpawnPoints = MapUtils.GetEnemySpawnPoints(tileMapLevel);
            PlayerBase = MapUtils.GetTargetPoints(tileMapLevel).First();
        }
        
        public IReadOnlyList<Vector3Int> FindPathTo(
            Vector3Int startTileMapTilePosition, 
            Vector3Int endTileTileMapTilePosition)
        {
            var list = UnityEngine.Pool.ListPool<Vector2Int>.Get();

            //_pathfinderToTileMap.FindPathFromTileToTile(startTileMapTilePosition, endTileTileMapTilePosition,);

            return null;
        }
        
        /*
        private bool IsCanFindPath()
        {
            var tileMapLevel = levelController.CurrentMap;
            var enemyPoint = MapUtils.GetFirstTargetPoints(tileMapLevel);
            var target = MapUtils.GetTargetPoints(tileMapLevel);

            var startTile = enemyPoint.First();
            var endTile = target.First();

            var list = UnityEngine.Pool.ListPool<Vector2Int>.Get();
            OtherPoints(list);
            var pathfinderToTileMap = new PathfinderToTileMap(tileMapLevel);
            var findPath = pathfinderToTileMap.FindPathFromTileToTile(startTile.Position, endTile.Position , list);

            UnityEngine.Pool.ListPool<Vector2Int>.Release(list);
            return findPath != null;
        }

        private void OtherPoints(List<Vector2Int> list)
        {
            GetTowerCoordinates(_tower , list);
            foreach (var tower in _levelObjects.Towers)      
            {
                GetTowerCoordinates(tower , list);
            }
        }
        
        private void GetTowerCoordinates(TowerBase tower , List<Vector2Int> list)
        {
            var layerObstacles = levelController.CurrentMap.LayerObstacles;
            var point = layerObstacles.ConvertWorldToIndexZeroLeftBottomCorner(tower.transform.position);
            point -= new Vector3Int(1, 1);
            list.Add(new Vector2Int(point.x, point.y));
            list.Add(new Vector2Int(point.x + 1, point.y));
            list.Add(new Vector2Int(point.x + 1, point.y + 1));
            list.Add(new Vector2Int(point.x, point.y + 1));
        }
        */
    }
}
