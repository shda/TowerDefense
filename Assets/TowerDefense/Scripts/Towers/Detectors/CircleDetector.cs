using UnityEngine;

namespace TowerDefense.Scripts.Towers.Detectors
{
    public class CircleDetector : DetectorBase
    {
        [SerializeField] private CircleCollider2D collider;
        [SerializeField] private LayerMask enemyLayer;

        private RaycastHit2D[] _lastCircleCastAll;

        private void Awake()
        {
            if (enemyLayer.value == 0)
                Debug.LogError("Circle detector needs to be set to 0!");
        }

        private void OnDrawGizmos()
        {
            if (_lastCircleCastAll == null)
                return;

            foreach (var raycastHit2D in _lastCircleCastAll)
                Debug.DrawLine(transform.position, raycastHit2D.point, Color.brown);
        }

        public override RaycastHit2D[] Detect()
        {
            _lastCircleCastAll = Physics2D.CircleCastAll(
                transform.position, collider.radius * RadiusMultiplier, Vector2.zero, float.MaxValue, enemyLayer);
            return _lastCircleCastAll;
        }

        public override void ShowDetectRadius()
        {
            spriteShowDetectRadius.SetSpriteRenderToSizeCollider(collider);
            base.ShowDetectRadius();
        }

        public override bool CheckStillDetect(GameObject target)
        {
            return Vector3.Distance(collider.transform.position, target.transform.position) <= collider.radius;
        }
    }
}