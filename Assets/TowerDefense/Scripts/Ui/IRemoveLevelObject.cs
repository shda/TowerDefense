using UnityEngine;

namespace TowerDefense.Scripts.Ui
{
    public interface IRemoveLevelObject
    {
        Collider2D Collider { get; }
        void Remove(LevelObjects levelObjects);
    }
}