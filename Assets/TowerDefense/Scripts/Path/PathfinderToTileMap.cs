using System.Collections.Generic;
using TowerDefense.Scripts.TileMap;
using TowerDefense.Scripts.Towers;
using UnityEngine;

namespace TowerDefense.Scripts.Path
{
    public class PathfinderToTileMap
    {
        private readonly TileMapLevel _tileMapLevel;
        public PathfinderToTileMap(TileMapLevel tileMapLevel)
        {
            _tileMapLevel = tileMapLevel;
        }
        
        public IReadOnlyList<Vector3Int> FindPathFromTileToTile(
            Vector3Int startTilePositon, 
            Vector3Int endTilePositon, 
            IEnumerable<Vector3Int> otherObstacles = null)
        {
            var map = new HashSet<Vector3Int>(otherObstacles);

            var obsticles = _tileMapLevel.LayerObstacles;

            var cel = obsticles.cellBounds;

            return Pathfinder.FindPath(
                startTilePositon, 
                endTilePositon,
                cel.min, cel.max, map);
        }
        
        
        private void GetTowerCoordinates(TowerBase tower , List<Vector3Int> list)
        {
            var point = tower.PointToGrid;
            //point -= new Vector3Int(1, 1);
            list.Add(new Vector3Int(point.x, point.y));
            list.Add(new Vector3Int(point.x + 1, point.y));
            list.Add(new Vector3Int(point.x + 1, point.y + 1));
            list.Add(new Vector3Int(point.x, point.y + 1));
        }
    }
}