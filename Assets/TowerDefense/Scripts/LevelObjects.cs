using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Other;
using TowerDefense.Scripts.Towers;

namespace TowerDefense.Scripts
{
    public class LevelObjects
    { 
        public HashSetReactive<Enemy> Enemies { get; set; } = new();
        public HashSetReactive<TowerBase> Towers { get; set; }  = new();
    }
}