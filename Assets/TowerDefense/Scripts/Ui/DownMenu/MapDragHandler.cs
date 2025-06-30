using System;
using TowerDefense.Scripts.Game;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Scripts.Ui.DownMenu
{
    public abstract class MapDragHandler
    {
        protected LevelController _levelController;
        protected LayerMask _layerMask;
        protected LevelObjects _levelObjects;
        protected NavMeshPath _navMeshPath;
        public abstract Sprite Sprite { get;}
        
        public Action<MapDragHandler> OnSuccess { get; set; }

        public void Init(
            LevelController levelController,
            LayerMask layerMask,
            LevelObjects levelObjects,
            NavMeshPath navMeshPath)
        {
            _levelController = levelController;
            _layerMask = layerMask;
            _levelObjects = levelObjects;
            _navMeshPath = navMeshPath;
        }

        public abstract void OnUpdateWorldPosition(Vector3 worldPosition);
        public abstract void OnPress(bool isPressed);
    }
}