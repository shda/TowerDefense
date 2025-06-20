using TowerDefense.Scripts.Effects;
using TowerDefense.Scripts.Enemies;

namespace TowerDefense.Scripts.Towers
{
    public class EffectContext
    {
        public Health Health { get;}
        public EffectHandler EffectHandler { get;}
        public MovingToPathHandler MovingToPathHandler { get;}

        public EffectContext(
            EffectHandler effectHandler, 
            Health health, 
            MovingToPathHandler movingToPathHandler)
        {
            Health = health;
            EffectHandler = effectHandler;
            MovingToPathHandler = movingToPathHandler;
        }
    }
}