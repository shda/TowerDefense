using System;

namespace TowerDefense.Scripts.Effects
{
    public class EffectBase
    {
        public Action<EffectBase> OnCompleteEffect;


        public void CompleteEffect()
        {
            OnCompleteEffect?.Invoke(this);
        }

        public void CustomUpdate()
        {
            
        }
    }
}