using TowerDefense.Scripts.Towers;

namespace TowerDefense.Scripts.Effects
{
    public static class EffectMake
    {
        public static EffectBase MakeDownSpeedEffect(ITarget target , float modifier , float timeEffect)
        {
            return new DownSpeedEffect(target , modifier  , timeEffect);
        }
        
        public static EffectBase MakePoisonEffect(ITarget target , PoisonData poisonData)
        {
            return new PoisonEffect(target , poisonData);
        }
    }
    
}