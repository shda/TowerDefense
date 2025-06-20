using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TowerDefense.Scripts.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        
        public Action<bool> OnPress;
        public Action<Vector3> OnPointWorld;
        public Action<Vector2> OnPointMoving;
        
        private Vector2 _moveInput;
        private Vector2 _mouseWorldPosition;
        private bool _isPressButton;
        
        public void OnFire(InputAction.CallbackContext context)
        {
            _isPressButton = context.performed;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnMouseLook(InputAction.CallbackContext context)
        {
            var screenPosition = context.ReadValue<Vector2>();
            _mouseWorldPosition = camera.ScreenToWorldPoint(screenPosition);
        }

        private void Update()
        {
            OnPress?.Invoke(false);
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            OnPress?.Invoke(_isPressButton);
            OnPointWorld?.Invoke(_mouseWorldPosition);
            OnPointMoving?.Invoke(_moveInput);
        }
    }
}