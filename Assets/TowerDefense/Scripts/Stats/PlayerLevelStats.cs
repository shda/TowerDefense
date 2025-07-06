using System;
using TowerDefense.Scripts.PlayerScripts;
using UniRx;

namespace TowerDefense.Scripts.Stats
{
    public class PlayerLevelStats : IDisposable
    {
        private const int MaxPointToChangeLevel = 30;
        private readonly CompositeDisposable disposable = new();
        private int _targetPointsChangeLevel;
        
        public IntReactiveProperty Level = new(1);
        public ReactiveCommand<int> LevelUp = new();
        public IntReactiveProperty ProgressPoints = new();

        public PlayerLevelStats()
        {
            _targetPointsChangeLevel = MaxPointToChangeLevel;
            ProgressPoints.Subscribe(OnChangePoints).AddTo(disposable);
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }

        private void OnChangePoints(int points)
        {
            if (ProgressPoints.Value >= _targetPointsChangeLevel)
            {
                ProgressPoints.Value -= _targetPointsChangeLevel;
                _targetPointsChangeLevel += MaxPointToChangeLevel;
                Level.Value += 1;
                LevelUp?.Execute(Level.Value);
            }
        }

        public float GetProgressToNextLevel01()
        {
            return (float)ProgressPoints.Value / _targetPointsChangeLevel;
        }
    }
}