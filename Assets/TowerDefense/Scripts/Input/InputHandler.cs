using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TowerDefense.Scripts.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        private bool _isPressButton;

        private bool _isSendPress;
        private Vector2 _mouseWorldPosition;

        private Vector2 _moveInput;
        public Action<Vector2> OnPointMoving;
        public Action<Vector3> OnPointWorld;

        public Action<bool> OnPress;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (!_isSendPress)
            {
                _isSendPress = true;
                OnPress?.Invoke(_isPressButton);
            }

            OnPointWorld?.Invoke(_mouseWorldPosition);
            OnPointMoving?.Invoke(_moveInput);
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            _isSendPress = false;
            _isPressButton = context.performed;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnMouseLook(InputAction.CallbackContext context)
        {
            if (camera == null)
                return;
            var screenPosition = context.ReadValue<Vector2>();
            _mouseWorldPosition = camera.ScreenToWorldPoint(screenPosition);
        }
    }
}