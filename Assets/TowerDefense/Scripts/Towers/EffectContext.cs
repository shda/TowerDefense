using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Enemies.Effects;

namespace TowerDefense.Scripts.Towers
{
    public class EffectContext
    {
        public Health Health { get;}
        public EffectHandler EffectHandler { get;}
        public MovingHandlerBase movingHandler { get;}

        public EffectContext(
            EffectHandler effectHandler, 
            Health health, 
            MovingHandlerBase movingHandler)
        {
            Health = health;
            EffectHandler = effectHandler;
            this.movingHandler = movingHandler;
        }
    }
}