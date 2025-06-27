using UniRx;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerStats
    {
        public FloatReactiveProperty MultiplierRadiusShot = new(1.0f);
        public FloatReactiveProperty MultiplierDamageShot = new(1.0f);
        public FloatReactiveProperty MultiplierHealth = new(1.0f);
        public IntReactiveProperty CountTargetOneShot = new(3);
    }
}