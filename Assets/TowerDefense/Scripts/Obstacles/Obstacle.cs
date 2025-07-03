using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Game;
using TowerDefense.Scripts.Ui.DownMenu;
using UnityEngine;

namespace TowerDefense.Scripts.Obstacles
{
    public class Obstacle : MonoBehaviour, IRemoveLevelObject
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private Collider2D collider;
        public Collider2D Collider => collider;
        public Vector2Int Size => size;

        public void Remove(LevelObjects levelObjects)
        {
            levelObjects.Obstacles.Remove(this);
            gameObject.Hide();
            Destroy(gameObject);
        }
    }
}