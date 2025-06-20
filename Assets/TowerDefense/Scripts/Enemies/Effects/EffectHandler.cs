using System;
using System.Collections.Generic;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public class EffectHandler
    {
        private readonly HashSet<EffectBase> _effects = new();
        private readonly HashSet<Type> _effectsTypes = new();

        public void AddEffect(EffectBase effect)
        {
            effect.OnCompleteEffect = RemoveEffect;
            _effects.Add(effect);
            _effectsTypes.Add(effect.GetType());
        }

        public void RemoveEffect(EffectBase effect)
        {
            _effects.Remove(effect);
            _effectsTypes.Remove(effect.GetType());
        }
        
        public void CustomUpdate()
        {
            var list = UnityEngine.Pool.ListPool<EffectBase>.Get();
            list.AddRange(_effects);
            
            foreach (var effect in list)
            {
                effect.CustomUpdate();
            }
            
            UnityEngine.Pool.ListPool<EffectBase>.Release(list);
        }

        public bool IsContainEffectByType(Type type)
        {
            return _effectsTypes.Contains(type);
        }
    }
}