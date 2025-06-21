using System;
using System.Collections.Generic;
using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefense.Scripts.TileMap
{
    public static class TileMapExtensions
    {
        public static TileInfo[,] GetAllTiles(this Tilemap tilemap)
        {
            var bounds = tilemap.cellBounds;
            
            var mapTiles = new TileInfo[bounds.size.x, bounds.size.y];

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    var pos = new Vector3Int(x + bounds.x, y + bounds.y, 0);
                    
                    var tile = tilemap.GetTile(pos);
                    if (tile != null)
                    {
                        if(tile is SuperTile superTile)
                        {
                            var worldPos = tilemap.GetCellCenterWorld(pos);
                            mapTiles[x, y] = new TileInfo
                            {
                                Tile = superTile,
                                Position = pos,
                                WorldPosition = worldPos
                            };
                        }
                    }
                }
            }

            return mapTiles;
        }

        public static Vector3 GetLeftBottomCorner(this Tilemap tilemap)
        {
            return tilemap.transform.position + tilemap.localBounds.min;
        }

        public static void HideLayerRender(this Tilemap tileMap)
        {
            var render = tileMap.GetComponent<TilemapRenderer>();
            render.enabled = false;
        }
        
        public static bool IsTileExisttoWorldPosition(this Tilemap tileMap , Vector3 worldPosition)
        {
            var pos = tileMap.WorldToCell(worldPosition);
            var tile = tileMap.GetTile(pos);
            return tile;
        }
        
        public static SuperTileCoord GetByTileWorldOrNull(this Tilemap tileMap , Vector3 position)
        {
            var pos = tileMap.WorldToCell(position);
            var tile = tileMap.GetTile(pos);

            if (tile is SuperTile superTile)
            {
                return new SuperTileCoord(superTile , pos);
            }

            return null;
        }
        
        private static Vector3 FindPositionTypeByName(this Tilemap tilemap, string valueByName)
        {
            var tiles = FindTilesByNameProperty(tilemap ,"name");

            foreach (var tile in tiles)
            {
                var nameValue = tile.Tile.GetPropertyValueAsString("name");
                if (string.Equals(nameValue, valueByName, StringComparison.Ordinal))
                {
                    return  tilemap.GetCellCenterWorld(tile.Position);
                }
            }
            
            throw new Exception("Could not find tile map tile named " + valueByName);
        }

        public static TileInfo[] FindTilesByNameProperty(this Tilemap tilemap,  string propName , string valueByName)
        {
            var result = new List<TileInfo>();
            
            var tiles = FindTilesByNameProperty(tilemap ,propName);

            foreach (var tile in tiles)
            {
                var nameValue = tile.Tile.GetPropertyValueAsString(propName);
                if (string.Equals(nameValue, valueByName, StringComparison.Ordinal))
                {
                    result.Add(tile);
                }
            }

            return result.ToArray();
        }
        
        public static TileInfo[] FindTilesByNameProperty(this Tilemap tilemap, string propName)
        {
            var tiles = new List<TileInfo>();
            
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                var tile = tilemap.GetTile(pos);
                
                if (tile)
                {
                    if (tile is SuperTile superTile)
                    {
                        if (superTile.m_CustomProperties != null)
                        {
                            if (superTile.TryGetProperty(propName,  out CustomProperty property))
                            {
                                var worldPos = tilemap.GetCellCenterWorld(pos);
                                tiles.Add(new TileInfo
                                {
                                    Tile = superTile,
                                    Position = pos,
                                    WorldPosition = worldPos
                                });
                            }
                        }
                    }
                }
            }
            return tiles.ToArray();
        }
        
        public static Bounds GetTilemapWorldBounds(this Tilemap tilemap)
        {
            tilemap.CompressBounds(); // Обновляем границы
            var cellBounds = tilemap.cellBounds;
            
            if (cellBounds.size == Vector3Int.zero)
                return new Bounds(Vector3.zero, Vector3.zero);
            
            var minWorld = tilemap.CellToWorld(cellBounds.min);
            var maxWorld = tilemap.CellToWorld(cellBounds.max);
            
            var tileSize = tilemap.cellSize;
            maxWorld += tileSize;
            
            var worldBounds = new Bounds();
            worldBounds.SetMinMax(minWorld, maxWorld);

            return worldBounds;
        }
        
        public static bool IsPointInTileMap(this Tilemap tilemap, Vector3 point)
        {
            var bounds = tilemap.GetTilemapWorldBounds();
            return bounds.Contains(point);
        }
    }
}