using System.Linq;
using TowerDefense.Scripts.TileMap;

namespace TowerDefense.Scripts.Utils
{
    public static class MapUtils
    {
        public static TileInfo[] GetEnemySpawnPoints(TileMapLevel tileMapLevel)
        {
            return tileMapLevel.LayerService.FindTilesByNameProperty("name", "enemy_spawns");
        }

        public static TileInfo[] GetFirstTargetPoints(TileMapLevel tileMapLevel)
        {
            return tileMapLevel.LayerService.FindTilesByNameProperty("name", "first_target");
        }

        public static TileInfo[] GetTargetPoints(TileMapLevel tileMapLevel)
        {
            return tileMapLevel.LayerService.FindTilesByNameProperty("name", "player_base");
        }
    }
}