using UnityEngine;

namespace TowerDefense.Scripts.Settings
{
    [CreateAssetMenu(menuName = "Create Settings/GameSettings", fileName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [filed: SerializeField] public Vector2Int FieldSize { get; set; } = new(14, 18);
        //[field:SerializeField] public float offsetOne
    }
}
