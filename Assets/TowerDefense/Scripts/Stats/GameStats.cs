using System;
using System.Threading;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Stats
{
    public class GameStats : IDisposable
    {
        private const int MaxPointToChangeLevel = 30;
        
        public IntReactiveProperty CountEnemy = new();
        public IntReactiveProperty ProgresscPoints = new();
        public IntReactiveProperty Level = new(1);
        public ReactiveCommand<int> LevelUp = new();
        private CompositeDisposable disposable = new();
        public GameStats()
        {
            ProgresscPoints.Subscribe(OnChangePoints).AddTo(disposable);
        }

        private void OnChangePoints(int points)
        {
            if (points >= MaxPointToChangeLevel)
            {
                Level.Value += points / MaxPointToChangeLevel;
                ProgresscPoints.Value = points % MaxPointToChangeLevel;
                
                LevelUp?.Execute(Level.Value);
            }
        }

        public float GetProgressToNextLevel01()
        {
            return (float) ProgresscPoints.Value / MaxPointToChangeLevel;
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}
