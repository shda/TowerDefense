using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace TowerDefense.Scripts.Effects
{
    public class EffectHandler : MonoBehaviour
    {
        private readonly HashSet<EffectBase> _effects = new();

        public void AddEffect(EffectBase effect)
        {
            effect.OnCompleteEffect = RemoveEffect;
            _effects.Add(effect);
        }

        public void RemoveEffect(EffectBase effect)
        {
            _effects.Remove(effect);
        }
        
        private void Update()
        {
            var list = ListPool<EffectBase>.Get();
            list.AddRange(_effects);
            
            foreach (var effect in list)
            {
                effect.CustomUpdate();
            }
            
            ListPool<EffectBase>.Release(list);
        }
    }
}