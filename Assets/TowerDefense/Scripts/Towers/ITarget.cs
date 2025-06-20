using System;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public interface ITarget
    {
        Transform Transform { get; }
        bool IsCanByTarget { get; }
        Health Health { get; }
        ReactiveCommand OnDead { get; }
        EffectContext GetEffectContext();
    }
}