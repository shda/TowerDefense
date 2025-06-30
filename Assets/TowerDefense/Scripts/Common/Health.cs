using UniRx;

namespace TowerDefense.Scripts
{
    public class Health
    {
        public FloatReactiveProperty  CurrentHealth { get; set; } = new();
        public FloatReactiveProperty  MaxHealth { get; set; } = new();

        public bool IsDead()
        {
            return CurrentHealth.Value <= 0;
        }
    }
}