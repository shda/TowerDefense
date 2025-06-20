using UnityEngine;

namespace TowerDefense.Scripts.Input
{
    public interface IPressListener
    {
        public void OnPress(bool isPressed);
    }
    
    public interface IPointListener
    {
        public void OnPointWorld(Vector3 worldPosition);
        public void OnPointMoving(Vector2 direction);
    }
}