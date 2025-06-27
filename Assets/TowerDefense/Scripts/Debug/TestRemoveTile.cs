using TowerDefense.Scripts.TileMap;
using TriInspector;
using UnityEngine;

namespace TowerDefense.Scripts.Debug
{
    public class TestRemoveTile : MonoBehaviour
    {
        public TileMapLevel tileMapLevel;
        public Vector3Int removeTile;
        
        [Button]
        public void RemoveTile()
        {
            var tile = tileMapLevel.LayerObstacles.GetTile(removeTile);
            if (tile != null)
            {
                tileMapLevel.LayerObstacles.SetTile(removeTile, null);
            }
            
            UnityEngine.Debug.Log(tile);
        }
    }
}