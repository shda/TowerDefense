using TowerDefense.Scripts.Towers.TowersTypes;
using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    [CreateAssetMenu(menuName = "Create TowerDefense/TowerCollection", fileName = "TowerCollection", order = 0)]
    public class ItemsCollection : ScriptableObject
    {
        [SerializeField] private string idRemoveCell;
        [SerializeField] private TowerBase[] towersPrefs;
        public TowerBase[] TowersPrefs => towersPrefs;
        public string IdRemoveCell => idRemoveCell;
    }
}