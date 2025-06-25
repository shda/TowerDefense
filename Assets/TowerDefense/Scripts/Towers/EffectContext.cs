using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Enemies.Effects;

namespace TowerDefense.Scripts.Towers
{
    public class EffectContext
    {
        public Health Health { get;}
        public EffectHandler EffectHandler { get;}
        public MovingToNavigationMash MovingToPathHandler { get;}

        public EffectContext(
            EffectHandler effectHandler, 
            Health health, 
            MovingToNavigationMash movingToPathHandler)
        {
            Health = health;
            EffectHandler = effectHandler;
            MovingToPathHandler = movingToPathHandler;
        }
    }
}