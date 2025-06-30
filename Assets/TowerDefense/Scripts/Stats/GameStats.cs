using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Stats
{
    public class GameStats
    {
        public IntReactiveProperty CountEnemy = new();
        public IntReactiveProperty ProgressNextTower = new();

        public float GetProgressToNextLevel01()
        {
            return ProgressNextTower.Value / 30f;
        }
    }
}
