using TowerDefense.Scripts.TileMap;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Enemies
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField] private TileMapLevel tileMapLevel;

        public TileMapLevel CurrentMap => tileMapLevel;

        public bool IsMapLoaded { get; set; } = true;
        
        public bool Sillsta { get; set; }
    }
}
