using System.Collections.Generic;
using TowerDefense.Scripts.TileMap;
using UnityEngine;

namespace TowerDefense.Scripts.Debug
{
    public class GizmoHelper
    {
        public static void DrawPath(TileMapLevel map , IEnumerable<Vector3Int> findPath)
        {
            if (findPath == null)
                return;
            
            var tilemap = map.LayerObstacles;
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
    }
}