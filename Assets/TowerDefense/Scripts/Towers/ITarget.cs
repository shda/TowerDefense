using TowerDefense.Scripts.Common;
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
        EnemyType EnemyType { get; }
        ReactiveProperty<EnemyActionStatus> Status { get; }
        EffectContext GetEffectContext();
    }
}