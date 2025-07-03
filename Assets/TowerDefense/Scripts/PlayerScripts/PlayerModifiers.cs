using UniRx;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerModifiers
    {
        public IntReactiveProperty CountTargetOneShot = new(3);
        public FloatReactiveProperty MultiplierDamageShot = new(1.0f);
        public FloatReactiveProperty MultiplierHealth = new(1.0f);
        public FloatReactiveProperty MultiplierRadiusShot = new(1.0f);
    }
}