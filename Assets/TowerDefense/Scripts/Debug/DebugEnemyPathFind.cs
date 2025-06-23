
using System.Collections.Generic;
using TowerDefense.Scripts.Path;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Debug
{
    public class DebugEnemyPathFind : MonoBehaviour
    {
        private LevelController _levelController;
        private IReadOnlyList<Vector3Int> _findPath;
        private PathfinderToTileMap _pathfinderToTileMap;
        
        [Inject]
        public void Construct(LevelController levelController)
        {
            _levelController = levelController;
        }
        private void Update()
        {
            if(!Application.isEditor)
                return;
            
            if(!_levelController.IsMapLoaded)
                return;
            
            
            var tileMapLevel = _levelController.CurrentMap;
            _pathfinderToTileMap = new PathfinderToTileMap(tileMapLevel);
            FindPath();
        }
        
        private void FindPath()
        {
            HashSet<Vector3Int> map = new HashSet<Vector3Int>();

            var obsticles = _levelController.CurrentMap.LayerObstacles;

            var cel = obsticles.cellBounds;

            for (int x = cel.xMin; x < cel.xMax + 1; x++)
            {
                for (int y = cel.yMin; y < cel.yMax + 1; y++)
                {
                    var tile = obsticles.GetTile(new Vector3Int(x, y));
                    if (tile)
                    {
                        map.Add(new Vector3Int(x, y));
                        map.Add(new Vector3Int(x + 1, y));
                        map.Add(new Vector3Int(x + 1, y + 1));
                        map.Add(new Vector3Int(x, y + 1));
                    }
                }
            }

            findPath = Pathfinder4.FindPath(
                _levelController.FirstTargetTile.Position, 
                _levelController.PlayerBase.Position,
                cel.min, cel.max, map);
        }

        public List<Vector3Int> findPath { get; set; }

        private void OnDrawGizmos()
        {
            if (findPath == null)
                return;
            
            if(_levelController == null || !_levelController.IsMapLoaded)
                return;
            
            var tileMapLevel = _levelController.CurrentMap;
            var tilemap = tileMapLevel.LayerObstacles;
            var cellBounds = tileMapLevel.LayerObstacles.cellBounds;

            var offset = new Vector3(0.25f, 0.25f);
            
            Vector3? pointDraw = null;
            foreach (var point in findPath)
            {
                if (pointDraw == null)
                {
                    var worldPos = tilemap.GetCellCenterWorld(point);
                    worldPos += offset;
                    pointDraw = worldPos;

                    continue;
                }

                var newCell = tilemap.GetCellCenterWorld(point);
                newCell += offset;
                UnityEngine.Debug.DrawLine(pointDraw.Value, newCell, Color.red);
                pointDraw = newCell;
            }
        }

        /*
        private void OnDrawGizmos()
        {
            if (_findPath == null)
                return;
            
            if(!_levelController.IsMapLoaded)
                return;

            var tileMapLevel = _levelController.CurrentMap;
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
        */
    }
}