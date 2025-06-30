using System;
using UnityEngine;

namespace TowerDefense.Scripts.Towers.Projectiles
{
    public class ProjectilePhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        private Vector2 _velocity;
        private Vector3 _startPosition;
        private float _maxDistance;

        public Action<ProjectilePhysics> OnEndToDistance;
        public Action<ProjectilePhysics , Collider2D> TriggerEnter2D;
        
        public bool IsPool { get; set; }
        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter2D?.Invoke(this , other);
        }

        public void SetLinearVelocity(Vector3 velocity, float maxDistance)
        {
            _maxDistance = maxDistance;
            _startPosition = transform.position;
            _velocity = velocity;
            rb.linearVelocity = _velocity;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _startPosition) > _maxDistance)
            {
                OnEndToDistance?.Invoke(this);
            }
        }
    }
}