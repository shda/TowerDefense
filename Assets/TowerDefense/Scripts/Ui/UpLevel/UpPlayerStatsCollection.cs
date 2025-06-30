using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    [CreateAssetMenu(menuName = "Create TowerDefense/UpPlayerStatsCollection", fileName = "UpPlayerStatsCollection", order = 0)]
    public class UpPlayerStatsCollection : ScriptableObject
    {
        [SerializeField] private LevelUpPlayerStats levelUpPlayerRadius;
        [SerializeField] private LevelUpPlayerStats levelUpPlayerDamage;
        [SerializeField] private LevelUpPlayerStats levelUpPlayerHealth;
        [SerializeField] private LevelUpPlayerStats levelUpPlayerCountTargetOneShot;
        
        public LevelUpPlayerStats levelUpPlayerRadiusShot => levelUpPlayerRadius;
        public LevelUpPlayerStats levelUpPlayerDamageShot => levelUpPlayerDamage;
        public LevelUpPlayerStats levelUpPlayerHealthShot => levelUpPlayerHealth;
        public LevelUpPlayerStats levelUpPlayerCountTargetOneShotShot => levelUpPlayerCountTargetOneShot;

        public LevelUpPlayerStats GetRandomUpPlayerStats()
        {
            int random = Random.Range(0, 4);
            return random switch
            {
                0 => levelUpPlayerRadius,
                1 => levelUpPlayerDamage,
                2 => levelUpPlayerHealth,
                3 => levelUpPlayerCountTargetOneShot,
                _ => levelUpPlayerRadius
            };
        }
    }
}