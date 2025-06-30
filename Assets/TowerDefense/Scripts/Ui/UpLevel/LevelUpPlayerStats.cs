using TowerDefense.Scripts.PlayerScripts;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    [CreateAssetMenu(menuName = "Create TowerDefense/UpPlayerStats", fileName = "UpPlayerStats", order = 0)]
    public class LevelUpPlayerStats : LevelUpItemSettings
    {
        [SerializeField] private float multiplierRadiusShot;
        [SerializeField] private float multiplierDamageShot;
        [SerializeField] private float multiplierHealth;
        [SerializeField] private int countTargetOneShot;
        
        [SerializeField] private bool updateRadius;
        [SerializeField] private bool updateDamage;
        [SerializeField] private bool updateHealth;
        [SerializeField] private bool updateCountTargetOneShot;

        public void UpdatePlayerStats(PlayerModifiers playerModifiers)
        {
            if (updateRadius)
            {
                playerModifiers.MultiplierRadiusShot.Value += multiplierRadiusShot;
            }
            if (updateDamage)
            {
                playerModifiers.MultiplierDamageShot.Value += multiplierDamageShot;
            }
            if (updateHealth)
            {
                playerModifiers.MultiplierHealth.Value += multiplierHealth;
            }
            if (updateCountTargetOneShot)
            {
                playerModifiers.CountTargetOneShot.Value += countTargetOneShot;
            }
        }
    }
}