using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.TileMap;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Debug
{
    public class DebugEnemyPathFind : MonoBehaviour
    {
        private MapLoader _mapLoader;
        private IReadOnlyList<Vector3Int> _findPath;

        [Inject]
        public void Construct(MapLoader mapLoader)
        {
            _mapLoader = mapLoader;
        }
        
        private void Update()
        {
            if(!_mapLoader.IsMapLoaded)
                return;

            var tileMapLevel = _mapLoader.CurrentMap;
            
            var enemyPoint = tileMapLevel.LayerService.FindTilesByNameProperty("name", "enemy");
            var target = tileMapLevel.LayerService.FindTilesByNameProperty("name", "target");

            var startTile = enemyPoint.First();
            var endTile = target.First();
            
            var pathfinderToTileMap = new PathfinderToTileMap(tileMapLevel);
            _findPath = pathfinderToTileMap.FindPathFromTileToTile(startTile , endTile);
        }
        
        private void OnDrawGizmos()
        {
            if (_findPath == null)
                return;
            
            if(!_mapLoader.IsMapLoaded)
                return;

            var tileMapLevel = _mapLoader.CurrentMap;
            var tilemap = tileMapLevel.LayerObstacles;
            var cellBounds = tileMapLevel.LayerObstacles.cellBounds;

            var offset = new Vector3(0.25f, 0.25f);
            
            Vector3? pointDraw = null;
            foreach (var point in _findPath)
            {
                if (pointDraw == null)
                {
                    var worldPos = tilemap.GetCellCenterWorld(point + cellBounds.position );
                    worldPos += offset;
                    pointDraw = worldPos;

                    continue;
                }

                var newCell = tilemap.GetCellCenterWorld(point + cellBounds.position);
                newCell += offset;
                UnityEngine.Debug.DrawLine(pointDraw.Value, newCell, Color.red);
                pointDraw = newCell;
            }
        }
    }
}