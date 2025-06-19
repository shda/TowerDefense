using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerControl : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 5f;
        
        private Rigidbody2D _rb;
        private Vector2 _moveInput;
        private Vector2 _mouseWorldPosition;

        private Camera _mainCamera;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _mainCamera = Camera.main;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnMouseLook(InputAction.CallbackContext context)
        {
            var screenPosition = context.ReadValue<Vector2>();
            _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _moveInput * moveSpeed;
            
            var direction = _mouseWorldPosition - _rb.position;
            if (direction.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _rb.rotation = angle;
            }
        }
    }
}