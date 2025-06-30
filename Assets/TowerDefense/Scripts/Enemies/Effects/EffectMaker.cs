using TowerDefense.Scripts.Towers;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public static class EffectMaker
    {
        public static Effect MakeDownSpeedEffect(ITarget target , float modifier , float timeEffect)
        {
            return new SlowdownEffect(target , modifier  , timeEffect);
        }
        
        public static Effect MakePoisonEffect(ITarget target , PoisonData poisonData)
        {
            return new PoisonEffect(target , poisonData);
        }
    }
    
}