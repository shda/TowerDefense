using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    public class MovingToPathHandler
    {
        private Vector3[] _movingPath;
        private int _indexPointToPath;
        private Vector3 _targetPosition;
        private Transform _movingTransform;
        public float SpeedMoving { get; set; }
        public Action OnCompleteMove;

        public void Init(Transform movingTransform , float speedMoving)
        {
            _movingTransform =  movingTransform;
            SpeedMoving = speedMoving;
        }
        
        public void SetStartPosition(Vector3 worldPosition)
        {
            _movingTransform.position = worldPosition;
        }

        public void SetMovingToPath(IEnumerable<Vector3> movingPath)
        {
            _movingPath = movingPath.ToArray();
            _indexPointToPath = 0;
            _targetPosition = _movingPath[0];
        }

        public void CustomUpdate()
        {
            StepMovingToPath();
        }

        private void StepMovingToPath()
        {
            if(_indexPointToPath >= _movingPath.Length)
            {
                return;
            }
            
            _movingTransform.position = Vector3.MoveTowards(
                _movingTransform.position , 
                _targetPosition ,  
                SpeedMoving * Time.deltaTime);
            
            var direction = _targetPosition - _movingTransform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _movingTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            if (Vector3.Distance(_movingTransform.position, _targetPosition) <= 0.1f)
            {
                _indexPointToPath++;
                if (_indexPointToPath >= _movingPath.Length)
                {
                    OnCompleteMove?.Invoke();
                    return;
                }
                
                _targetPosition = _movingPath[_indexPointToPath];
            }
        }
    }
}