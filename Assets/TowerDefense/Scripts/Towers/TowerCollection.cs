using UnityEngine;

namespace TowerDefense.Scripts.Towers
{
    [CreateAssetMenu(menuName = "Create TowerDefense/TowerCollection", fileName = "TowerCollection", order = 0)]
    public class TowerCollection : ScriptableObject
    {
        [SerializeField] TowerBase[]  towersPrefs;
        public TowerBase[] TowersPrefs => towersPrefs;
    }
}