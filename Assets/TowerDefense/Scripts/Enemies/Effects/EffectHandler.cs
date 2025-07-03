using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;

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
            if (_effects == null)
                return;

            _effects.Remove(effect);
            _effectsTypes.Remove(effect.GetType());
        }

        public void CustomUpdate()
        {
            var list = ListPool<Effect>.Get();
            list.AddRange(_effects);

            foreach (var effect in list)
            {
                effect.Update();
                if (effect.IsExpired) RemoveEffect(effect);
            }

            ListPool<Effect>.Release(list);
        }

        public int CountContainEffectByType(Type type)
        {
            return _effectsTypes.Count(x => x == type);
        }

        public void RemoveAllEffects()
        {
            if (_effects is { Count: 0 })
                return;

            foreach (var effect in _effects.ToArray()) RemoveEffect(effect);
        }
    }
}