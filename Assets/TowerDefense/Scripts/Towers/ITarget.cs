using System;
using TowerDefense.Scripts.Enemies;
using UniRx;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    public interface ITarget
    {
        Transform Transform { get; }
        bool IsCanByTarget { get; }
        Health Health { get; }
        ReactiveProperty<EnemyStatus> Status { get; }
        EffectContext GetEffectContext();
    }
}