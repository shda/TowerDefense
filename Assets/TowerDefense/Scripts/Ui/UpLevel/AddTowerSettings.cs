using TowerDefense.Scripts.Towers;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    [CreateAssetMenu(menuName = "Create TowerDefense/AddTowerSettings", fileName = "AddTowerSettings", order = 0)]
    public class AddTowerSettings : LevelUpItemSettings
    {
        [SerializeField] private TowerBase towerPrefab;
        public override Sprite Icon => towerPrefab.TowerComponents.SpriteRenderer.sprite;
        public override string Description => towerPrefab.TowerComponents.TowerId;
        
        public TowerBase TowerPrefab => towerPrefab;
    }
}