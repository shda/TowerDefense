using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense.Scripts.Path;
using UnityEngine;

namespace TowerDefense.Scripts.TileMap
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private TileMapLevel tileMapLevel;
        private List<Point> _findPath;
        
        private void Update()
        {
            _findPath = FindPath();
        }

        private List<Point> FindPath()
        {
            var map = tileMapLevel.LayerObstacles;
            var offset = map.cellBounds.position;

            var tilesObstacles = tileMapLevel.LayerObstacles.GetAllTiles();

            
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

            var enemyPoint = tileMapLevel.LayerService.FindTilesByNameProperty("name", "enemy");
            var target = tileMapLevel.LayerService.FindTilesByNameProperty("name", "target");

            var startTile = enemyPoint.First();
            var endTile = target.First();

            var startPoint = new Point(startTile.Position.x - offset.x, startTile.Position.y - offset.y);
            var endPoint = new Point(endTile.Position.x - offset.x, endTile.Position.y - offset.y);

            var pathfinder = new Pathfinder(nodes);
            List<Point> findPath =
                pathfinder.FindPath(new Point(startPoint.X, startPoint.Y), new Point(endPoint.X, endPoint.Y));

            return findPath;
        }

        private void OnDrawGizmos()
        {
            if (_findPath == null)
                return;

            // var tilesObstacles = tileMapLevel.LayerObstacles.GetAllTiles();
            var tilemap = tileMapLevel.LayerObstacles;
            var cellBounds = tileMapLevel.LayerObstacles.cellBounds;

            Vector3? pointDraw = null;
            foreach (var point in _findPath)
            {
                if (pointDraw == null)
                {
                    var worldPos = tilemap.GetCellCenterWorld(new Vector3Int(point.X, point.Y, 0) + cellBounds.position);
                    pointDraw = worldPos;

                    continue;
                }

                var newCell = tilemap.GetCellCenterWorld(new Vector3Int(point.X, point.Y, 0) + cellBounds.position);

                Debug.DrawLine(pointDraw.Value, newCell, Color.red);
                pointDraw = newCell;
            }
        }
    }
}