using TowerDefense.Scripts.Towers;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public abstract class Effect
    {
        protected EffectContext _effectContext;
        public float Duration { get; protected set; }
        public bool IsExpired => Duration <= 0;

        public virtual void SetEffect(EffectContext effectContext)
        {
            _effectContext = effectContext;
            effectContext.EffectHandler.AddEffect(this);
        }

        public virtual void Update()
        {
            Duration -= Time.deltaTime;
            if (IsExpired)
            {
                ResetEffect();
            }
        }

        protected abstract void ResetEffect();
    }
}