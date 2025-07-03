using System;
using UniRx;

namespace TowerDefense.Scripts.Stats
{
    public class GameStats : IDisposable
    {
        private const int MaxPointToChangeLevel = 30;
        private readonly CompositeDisposable disposable = new();
        private int _targetPointsChangeLevel;

        public IntReactiveProperty CountEnemy = new();
        public IntReactiveProperty Level = new(1);
        public ReactiveCommand<int> LevelUp = new();
        public IntReactiveProperty ProgressPoints = new();

        public GameStats()
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
            return (float)ProgressPoints.Value / MaxPointToChangeLevel;
        }
    }
}