using TowerDefense.Scripts.Common;
using TowerDefense.Scripts.Enemies.Effects;
using TowerDefense.Scripts.Enemies.Movements;

namespace TowerDefense.Scripts.Towers
{
    public class EffectContext
    {
        public EffectContext(
            EffectHandler effectHandler,
            Health health,
            BaseMovement movingHandler)
        {
            Health = health;
            EffectHandler = effectHandler;
            this.movingHandler = movingHandler;
        }

        public Health Health { get; }
        public EffectHandler EffectHandler { get; }
        public BaseMovement movingHandler { get; }
    }
}