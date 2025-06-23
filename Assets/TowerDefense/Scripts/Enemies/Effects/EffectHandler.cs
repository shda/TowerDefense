using System;
using System.Collections.Generic;

namespace TowerDefense.Scripts.Enemies.Effects
{
    public class EffectHandler
    {
        private readonly HashSet<Effect> _effects = new();
        private readonly HashSet<Type> _effectsTypes = new();

        public void AddEffect(Effect effect)
        {
            _effects.Add(effect);
            _effectsTypes.Add(effect.GetType());
        }

        private void RemoveEffect(Effect effect)
        {
            _effects.Remove(effect);
            _effectsTypes.Remove(effect.GetType());
        }
        
        public void CustomUpdate()
        {
            var list = UnityEngine.Pool.ListPool<Effect>.Get();
            list.AddRange(_effects);
            
            foreach (var effect in list)
            {
                effect.Update();
                if (effect.IsExpired)
                {
                    RemoveEffect(effect);
                }
            }
            
            UnityEngine.Pool.ListPool<Effect>.Release(list);
        }
        
        public bool IsContainEffectByType(Type type)
        {
            return _effectsTypes.Contains(type);
        }
    }
}