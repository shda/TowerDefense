using TowerDefense.Scripts.Enemies;
using UnityEngine;

namespace TowerDefense.Scripts.Lua
{
    [CreateAssetMenu(menuName = "Create TowerDefense/EnemySettingsCollection", fileName = "EnemySettingsCollection",
        order = 0)]
    public class EnemySettingsCollection : ScriptableObject
    {
        [SerializeField] private EnemySettings[] enemies;

        public EnemySettings[] Enemies => enemies;
    }
}