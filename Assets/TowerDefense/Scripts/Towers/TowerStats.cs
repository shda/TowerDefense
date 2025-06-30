using UniRx;

namespace TowerDefense.Scripts.Towers
{
    public class TowerSettings
    {
        
    }
    
    public class TowerStats
    {
        public FloatReactiveProperty MultiplierRadiusShot = new(1.0f);
        public FloatReactiveProperty MultiplierDamageShot = new(1.0f);
        public FloatReactiveProperty MultiplierDelayShot = new(1.0f);
    }
}