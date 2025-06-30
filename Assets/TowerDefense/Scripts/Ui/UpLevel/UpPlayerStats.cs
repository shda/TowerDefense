using TowerDefense.Scripts.PlayerScripts;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    [CreateAssetMenu(menuName = "Create TowerDefense/UpPlayerStats", fileName = "UpPlayerStats", order = 0)]
    public class UpPlayerStats : UpLevelSettings
    {
        [SerializeField] private float multiplierRadiusShot;
        [SerializeField] private float multiplierDamageShot;
        [SerializeField] private float multiplierHealth;
        [SerializeField] private int countTargetOneShot;
        
        [SerializeField] private bool updateRadius;
        [SerializeField] private bool updateDamage;
        [SerializeField] private bool updateHealth;
        [SerializeField] private bool updateCountTargetOneShot;

        public void UpdatePlayerStats(PlayerStats playerStats)
        {
            if (updateRadius)
            {
                playerStats.MultiplierRadiusShot.Value += multiplierRadiusShot;
            }
            if (updateDamage)
            {
                playerStats.MultiplierDamageShot.Value += multiplierDamageShot;
            }
            if (updateHealth)
            {
                playerStats.MultiplierHealth.Value += multiplierHealth;
            }
            if (updateCountTargetOneShot)
            {
                playerStats.CountTargetOneShot.Value += countTargetOneShot;
            }
        }
    }
}