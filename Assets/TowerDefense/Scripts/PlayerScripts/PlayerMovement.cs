using TowerDefense.Scripts.Input;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour, IPointListener
    {
        [SerializeField] private Transform rootSprites;

        private InputSwitcher _inputSwitcher;
        private Vector2 _moveDirection;
        private PlayerSettings _playerSettings;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _moveDirection * _playerSettings.MoveSpeed;

            if (_moveDirection.x > 0.5f) rootSprites.localScale = new Vector3(-1, 1, 1);

            if (_moveDirection.x < -0.5f) rootSprites.localScale = new Vector3(1, 1, 1);
        }

        void IPointListener.OnPointWorld(Vector3 worldPosition)
        {
        }

        void IPointListener.OnPointMoving(Vector2 direction)
        {
            _moveDirection = direction;
        }

        [Inject]
        public void Construct(InputSwitcher inputSwitcher, PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
            _inputSwitcher = inputSwitcher;
            _inputSwitcher.SetPlayerControl(this);
        }
    }
}