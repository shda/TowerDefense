using TriInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefense.Scripts.Utils
{
    public class FixedNavigationMash : MonoBehaviour
    {
        [SerializeField] public GameObject navMeshSurface;
        [SerializeField] public Tilemap superMap;

        [Button]
        public void FixedMash()
        {
            var bounds = superMap.cellBounds;
            var cellSize = superMap.cellSize; // Размер одной ячейки в юнитах
            var tilemapWorldSize = new Vector3(
                bounds.size.x * cellSize.x * 100,
                bounds.size.y * cellSize.y * 100,
                bounds.size.z * cellSize.z * 100
            );

            Debug.Log(cellSize);

            navMeshSurface.transform.localScale = new Vector3(
                tilemapWorldSize.x, tilemapWorldSize.y, 1);
        }
    }
}