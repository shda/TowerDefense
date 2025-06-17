using SuperTiled2Unity;
using UnityEngine;

namespace TowerDefense.Scripts.TileMap
{
    public class SuperTileCoord
    {
        public SuperTile Tile { get; set; }
        public Vector3Int Position { get; set; }

        public SuperTileCoord(SuperTile tile, Vector3Int position)
        {
            Tile = tile;
            Position = position;
        }

        public bool Equals(SuperTileCoord other)
        {
            return other.Position == Position;
        }
    }
}