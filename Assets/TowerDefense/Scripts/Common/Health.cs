using UniRx;

namespace TowerDefense.Scripts
{
    public class Health
    {
        public FloatReactiveProperty  CurrentHealth { get; set; } = new(100);
        public FloatReactiveProperty  MaxHealth { get; set; } = new(100);

        public bool IsDead()
        {
            return CurrentHealth.Value <= 0;
        }
    }
}