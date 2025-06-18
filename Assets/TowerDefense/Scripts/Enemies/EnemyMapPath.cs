using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.TileMap;
using TowerDefense.Scripts.Utils;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    public class EnemyMapPath
    {
        private List<Vector3> _findPath = new();
        
        public IReadOnlyList<Vector3> GetPath(TileMapLevel tileMapLevel)
        {
            var enemyPoint = MapUtils.GetFirstTargetPoints(tileMapLevel);
            var target = MapUtils.GetTargetPoints(tileMapLevel);
            
            var startTile = enemyPoint.First();
            var endTile = target.First();

            var pathfinderToTileMap = new PathfinderToTileMap(tileMapLevel);
            var path = pathfinderToTileMap.FindPathFromTileToTile(startTile, endTile);
            var cellBounds = tileMapLevel.LayerObstacles.cellBounds;
            
            if (path != null)
            {
                var offset = new Vector3(0.25f, 0.25f);
                
                _findPath.Clear();
                foreach (var position in path)
                {
                     var worldPoint = tileMapLevel.LayerService.GetCellCenterWorld(
                        position + cellBounds.position);
                     worldPoint += offset;
                     
                     _findPath.Add(worldPoint);
                }
            }

            return _findPath;
        }
    }
}