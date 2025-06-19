using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    public class MovingToPathHandler
    {
        private IReadOnlyList<Vector3> _movingPath;
        private int _indexPointToPath;
        private Vector3 _targetPosition;
        
        private float _speedMoving;
        private Transform _movingTransform;
        
        public Action OnCompleteMove;

        public void Init(Transform movingTransform , float speedMoving)
        {
            _movingTransform =  movingTransform;
            _speedMoving = speedMoving;
        }
        
        public void SetStartPosition(Vector3 worldPosition)
        {
            _movingTransform.position = worldPosition;
        }

        public void SetMovingToPath(Vector3 firstTargetPoint, IReadOnlyList<Vector3> movingPath)
        {
            _movingPath = movingPath;
            _indexPointToPath = 0;
            _targetPosition = firstTargetPoint;
        }

        public void CustomUpdate()
        {
            StepMovingToPath();
        }

        private void StepMovingToPath()
        {
            if(_indexPointToPath >= _movingPath.Count)
            {
                return;
            }
            
            _movingTransform.position = Vector3.MoveTowards(
                _movingTransform.position , 
                _targetPosition ,  
                _speedMoving * Time.deltaTime);
            
            var direction = _targetPosition - _movingTransform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _movingTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            if (Vector3.Distance(_movingTransform.position, _targetPosition) <= 0.1f)
            {
                _indexPointToPath++;
                if (_indexPointToPath >= _movingPath.Count)
                {
                    OnCompleteMove?.Invoke();
                    return;
                }
                
                _targetPosition = _movingPath[_indexPointToPath];
            }
        }
    }
}