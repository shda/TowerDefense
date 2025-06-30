using TowerDefense.Scripts.Towers;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    [CreateAssetMenu(menuName = "Create TowerDefense/ClearCellItemSettings", fileName = "ClearCellItemSettings", order = 0)]
    public class ClearCellItemSettings : LevelUpItemSettings
    {
        [SerializeField] private ItemsCollection itemsCollection;
        public override string Description => itemsCollection.IdRemoveCell;
    }
}