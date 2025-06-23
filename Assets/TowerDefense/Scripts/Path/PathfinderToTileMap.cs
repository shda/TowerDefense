using System.Collections.Generic;
using TowerDefense.Scripts.TileMap;
using UnityEngine;

namespace TowerDefense.Scripts.Path
{
    public class PathfinderToTileMap
    {
        private readonly TileMapLevel _tileMapLevel;
        private readonly List<Vector3Int> _pathFound = new();

        private bool[,] _obstacles;
        private TileInfo[,] _tilesObstacles;

        public PathfinderToTileMap(TileMapLevel tileMapLevel)
        {
            var layerObstacles = tileMapLevel.LayerObstacles;
            _tilesObstacles = layerObstacles.GetAllTiles();
            _obstacles = new bool[_tilesObstacles.GetLength(0) + 1, _tilesObstacles.GetLength(1) + 1];
            _tileMapLevel = tileMapLevel;
        }

        public IReadOnlyList<Vector3Int> FindPathFromTileToTile(
            Vector3Int startTilePositon, 
            Vector3Int endTilePositon, 
            IEnumerable<Vector2Int> otherObstacles = null)
        {
            /*
            var layerObstacles = _tileMapLevel.LayerObstacles;
            var offset = layerObstacles.cellBounds.position;
            
            for (int x = 0; x < _tilesObstacles.GetLength(0); x++)
            {
                for (int y = 0; y < _tilesObstacles.GetLength(1); y++)
                {
                    _obstacles[x, y] = false;
                }
            }

            for (int x = 0; x < _tilesObstacles.GetLength(0); x++)
            {
                for (int y = 0; y < _tilesObstacles.GetLength(1); y++)
                {
                    var node = _tilesObstacles[x, y];
                    if (node != null)
                    {
                        _obstacles[x, y] = true;
                        _obstacles[x + 1, y] = true;
                        _obstacles[x, y + 1] = true;
                        _obstacles[x + 1, y + 1] = true;
                    }
                }
            }

            if (otherObstacles != null)
            {
                foreach (var obstacle in otherObstacles)
                {
                    _obstacles[obstacle.x, obstacle.y] = true;
                }
            }

            var startPoint = new Vector3Int(startTilePositon.x - offset.x, startTilePositon.y - offset.y);
            var endPoint = new Vector3Int(endTilePositon.x - offset.x, endTilePositon.y - offset.y);

            var pathfinder = new Pathfinder(_obstacles, false);
            var findPath =
                pathfinder.FindPath(_pathFound, startPoint, endPoint);

            return findPath ? _pathFound : null;
            */

            return null;
        }
    }
}