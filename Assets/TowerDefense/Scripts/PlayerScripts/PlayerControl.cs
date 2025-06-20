using TowerDefense.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerControl : MonoBehaviour , IPointListener
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 5f;
        
        private Rigidbody2D _rb;
        private Vector2 _moveDirection;
        private Vector3 _mouseWorldPosition;

        private InputSwitcher _inputSwitcher;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        [Inject]
        public void Construct(InputSwitcher inputSwitcher)
        {
            _inputSwitcher = inputSwitcher;
            _inputSwitcher.SetPlayerControl(this);
        }

        /*
        [Inject]
        public void Construct(InputHandler handler)
        {
            handler.OnPointMoving = vector2 =>
            {
                _moveDirection =  vector2;
            };

            handler.OnPointWorld = vector2 =>
            {
                _mouseWorldPosition = vector2;
            };
        }
        */
        
        void IPointListener.OnPointWorld(Vector3 worldPosition)
        {
            _mouseWorldPosition = worldPosition;
        }

        void IPointListener.OnPointMoving(Vector2 direction)
        {
            _moveDirection =  direction;
        }
        
        private void FixedUpdate()
        {
            _rb.linearVelocity = _moveDirection * moveSpeed;
            
            var direction = (Vector2)_mouseWorldPosition - _rb.position;
            if (direction.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _rb.rotation = angle;
            }
        }
    }
}