using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Input
{
    public class InputListenerSwitcher
    {
        private IPressListener _currentListener;
        private IPointListener _currentPointListener;

        public void SetListener(IPressListener listener)
        {
            _currentListener = listener;
        }

        public void SetListener(IPointListener listener)
        {
            _currentPointListener =  listener;
        }
        
        [Inject]
        public void Construct(InputHandler handler)
        {
            handler.OnPointMoving = OnPointMoving;
            handler.OnPointWorld = OnPointWorld;
            handler.OnPress = OnPress;
        }
        
        public void OnPress(bool isPressed)
        {
            _currentListener?.OnPress(isPressed);
        }

        public void OnPointWorld(Vector3 screenPosition)
        {
            _currentPointListener?.OnPointWorld(screenPosition);
        }

        public void OnPointMoving(Vector2 direction)
        {
            _currentPointListener?.OnPointMoving(direction);
        }
    }
}