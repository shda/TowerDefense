using TowerDefense.Scripts.Game;
using UnityEngine;

namespace TowerDefense.Scripts.Ui.DownMenu
{
    public interface IRemoveLevelObject
    {
        Collider2D Collider { get; }
        void Remove(LevelObjects levelObjects);
    }
}