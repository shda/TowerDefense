using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    public class MovingToLine : MovingHandlerBase
    {
        [SerializeField] public float speedMoving = 3.5f;
        public override float SpeedMoving => speedMoving;
        private Vector3 _targetPoint;
  
        public override void CustomUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, speedMoving * Time.deltaTime);
            if (Vector3.Distance(transform.position, _targetPoint) < 0.1f)
            {
                OnMovingComplete?.Invoke();
            }
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