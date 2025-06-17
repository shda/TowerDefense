using System;
using System.Collections.Generic;
using TowerDefense.Scripts.TileMap;
using UnityEngine;

namespace TowerDefense.Scripts.Path
{
    public class PathfinderToTileMap
    {
        private readonly TileMapLevel _tileMapLevel;
        private readonly List<Vector3Int> _pathFound = new();
        
        public PathfinderToTileMap(TileMapLevel tileMapLevel)
        {
            _tileMapLevel = tileMapLevel;
        }

        public IReadOnlyList<Vector3Int> FindPathFromTileToTile(TileInfo startTile, TileInfo endTile)
        {
            var map = _tileMapLevel.LayerObstacles;
            var offset = map.cellBounds.position;
            var tilesObstacles = _tileMapLevel.LayerObstacles.GetAllTiles();
            
            //One cell takes 64 by 64, but the obstacle 128 by 128
            bool[,] nodes = new bool[tilesObstacles.GetLength(0) + 1, tilesObstacles.GetLength(1) + 1];
            for (int x = 0; x < tilesObstacles.GetLength(0); x++)
            {
                for (int y = 0; y < tilesObstacles.GetLength(1); y++)
                {
                    var node = tilesObstacles[x, y];
                    if (node != null)
                    {
                        nodes[x, y] = true;
                        nodes[x + 1, y] = true;
                        nodes[x, y + 1] = true;
                        nodes[x + 1, y + 1] = true;
                    }
                }
            }
            
            var startPoint = new Vector3Int(startTile.Position.x - offset.x, startTile.Position.y - offset.y);
            var endPoint = new Vector3Int(endTile.Position.x - offset.x, endTile.Position.y - offset.y);

            var pathfinder = new Pathfinder(nodes , true);
            var findPath =
                pathfinder.FindPath(_pathFound , startPoint, endPoint);

            return  findPath ? _pathFound : null;
        }
    }
}