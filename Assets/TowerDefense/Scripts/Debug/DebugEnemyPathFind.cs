
using System.Collections.Generic;
using TowerDefense.Scripts.Path;
using TowerDefense.Scripts.TileMap;
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
            return;
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

            var findPath = Pathfinder.FindPath(
                _levelController.FirstTargetTile.Position, 
                _levelController.PlayerBase.Position,
                cel.min, cel.max, map);
        }
    }
}