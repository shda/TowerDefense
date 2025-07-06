using UniRx;

namespace TowerDefense.Scripts.Stats
{
    public class GameStates
    {
        public IntReactiveProperty CountEnemy { get; set; }= new();
        public ReactiveCommand EnemyReachedPlayerBase { get; set; }= new();
        public ReactiveCommand PlayerDied { get; set; } = new();
        public ReactiveCommand CompleteLevel { get; set; } = new();
        public ReactiveCommand Pause { get; set; } = new();
        
        public void CallPause()
        {
            Pause?.Execute();
        }

        public void CallCompleteLevel()
        {
            CompleteLevel?.Execute();
        }
        
        public void CallEnemyReachedPlayerBase()
        {
            EnemyReachedPlayerBase?.Execute();
        }
        
        public void CallPlayerDead()
        {
            PlayerDied?.Execute();
        }
    }
}