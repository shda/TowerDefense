using TowerDefense.Scripts.Towers;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public static class EffectMake
    {
        public static Effect MakeDownSpeedEffect(ITarget target , float modifier , float timeEffect)
        {
            return new DownSpeedEffect(target , modifier  , timeEffect);
        }
        
        public static Effect MakePoisonEffect(ITarget target , PoisonData poisonData)
        {
            return new PoisonEffect(target , poisonData);
        }
    }
    
}