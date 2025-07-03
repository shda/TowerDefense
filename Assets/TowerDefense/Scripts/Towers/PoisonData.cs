using System;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    [Serializable]
    public class PoisonData
    {
        [field: SerializeField] public float DamageOnIneHit { get; set; } = 3;
        [field: SerializeField] public int MaxDamages { get; set; } = 5;
        [field: SerializeField] public float DelayBetweenDamages { get; set; } = 1;

        public PoisonData(PoisonData poisonData, float modifier)
        {
            DamageOnIneHit = poisonData.DamageOnIneHit * modifier;
        }
    }
}