using SuperTiled2Unity;
using UnityEngine;

namespace TowerDefense.Scripts.TileMap
{
    public class TileInfo 
    {
        public SuperTile Tile { get; set; }
        public Vector3Int Position { get; set; }
        public Vector3 WorldPosition { get; set; }
    }
}