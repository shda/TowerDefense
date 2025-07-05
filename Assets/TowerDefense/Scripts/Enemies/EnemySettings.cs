using NaughtyAttributes;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    [CreateAssetMenu(menuName = "Create TowerDefense/EnemySettings", fileName = "EnemySettings", order = 0)]
    public class EnemySettings : ScriptableObject
    {
        public string tzName;

        [ShowAssetPreview(100, 100)] public Sprite sprite;

        public EnemyType enemyType;
        public int health = 100;
        public float speedMoving = 1.0f;
        public int givesPointsAftetDeath;

        [Header("Attack to player")] 
        public float hitToPlayer = 5f;

        public float radiusAttack = 1.5f;
        public float timeBetweenShots = 1.0f;

        [Header("Effects")] 
        public AttackEffectType ignoryEffect = AttackEffectType.None;
    }
}