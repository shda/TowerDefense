using UnityEngine;

namespace TowerDefense.Scripts.Enemies.Movements
{
    public class FlyMovement : BaseMovement
    {
        [SerializeField] public float speedMoving = 3.5f;
        private Vector3 _targetPoint;
        public override float SpeedMoving => speedMoving;

        public override void CustomUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, speedMoving * Time.deltaTime);
            if (Vector3.Distance(transform.position, _targetPoint) < 0.1f) OnMovingComplete?.Invoke();
        }

        public override void StartMoving(Vector3 movingPath)
        {
            _targetPoint = movingPath;
        }

        public override void SetStartPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }
    }
}