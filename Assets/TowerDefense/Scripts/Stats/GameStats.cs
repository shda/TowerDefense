using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Stats
{
    public class GameStats
    {
        public IntReactiveProperty CountEnemy = new();
        
        public FloatReactiveProperty ProgressNextTower = new();
    }
}
