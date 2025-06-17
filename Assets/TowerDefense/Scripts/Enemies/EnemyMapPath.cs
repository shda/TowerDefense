using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.TileMap;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemyMapPath
    {
        private List<Vector3> _findPath = new();
        
        public IReadOnlyList<Vector3> GetPath(TileMapLevel tileMapLevel)
        {
            var enemyPoint = tileMapLevel.LayerService.FindTilesByNameProperty("name", "enemy");
            var target = tileMapLevel.LayerService.FindTilesByNameProperty("name", "target");

            var startTile = enemyPoint.First();
            var endTile = target.First();

            var pathfinderToTileMap = new PathfinderToTileMap(tileMapLevel);
            var path = pathfinderToTileMap.FindPathFromTileToTile(startTile, endTile);
            var cellBounds = tileMapLevel.LayerObstacles.cellBounds;
            
            if (path != null)
            {
                _findPath.Clear();
                foreach (var position in path)
                {
                     var worldPoint = tileMapLevel.LayerService.GetCellCenterWorld(
                        position + cellBounds.position);
                     
                     _findPath.Add(worldPoint);
                }
            }

            return _findPath;
        }
    }
}