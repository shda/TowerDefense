using System;
using TowerDefense.Scripts.Towers;

namespace TowerDefense.Scripts.Effects
{
    public abstract class EffectBase
    {
        protected EffectContext _effectContext;
        public Action<EffectBase> OnCompleteEffect;
        
        public virtual void SetEffect(EffectContext effectContext)
        {
            _effectContext = effectContext;
            effectContext.EffectHandler.AddEffect(this);
        }
        public void CompleteEffect()
        {
            OnCompleteEffect?.Invoke(this);
        }

        public abstract void CustomUpdate();
    }
}