using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Obstacles;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Towers.TowersTypes;
using TowerDefense.Scripts.Utils;

namespace TowerDefense.Scripts.Game
{
    public class LevelObjects
    { 
        public HashSetReactive<Enemy> Enemies { get; set; } = new();
        public HashSetReactive<TowerBase> Towers { get; set; }  = new();
        public HashSetReactive<Obstacle> Obstacles { get; set; }  = new();
    }
}