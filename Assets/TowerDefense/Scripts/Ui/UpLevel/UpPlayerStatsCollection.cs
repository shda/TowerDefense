using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    [CreateAssetMenu(menuName = "Create TowerDefense/UpPlayerStatsCollection", fileName = "UpPlayerStatsCollection", order = 0)]
    public class UpPlayerStatsCollection : ScriptableObject
    {
        [SerializeField] private UpPlayerStats upPlayerRadius;
        [SerializeField] private UpPlayerStats upPlayerDamage;
        [SerializeField] private UpPlayerStats upPlayerHealth;
        [SerializeField] private UpPlayerStats upPlayerCountTargetOneShot;
        
        public UpPlayerStats upPlayerRadiusShot => upPlayerRadius;
        public UpPlayerStats upPlayerDamageShot => upPlayerDamage;
        public UpPlayerStats upPlayerHealthShot => upPlayerHealth;
        public UpPlayerStats upPlayerCountTargetOneShotShot => upPlayerCountTargetOneShot;

        public UpPlayerStats GetRandomUpPlayerStats()
        {
            int random = Random.Range(0, 4);
            return random switch
            {
                0 => upPlayerRadius,
                1 => upPlayerDamage,
                2 => upPlayerHealth,
                3 => upPlayerCountTargetOneShot,
                _ => upPlayerRadius
            };
        }
    }
}