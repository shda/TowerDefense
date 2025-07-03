using System;
using UnityEngine;

namespace TowerDefense.Scripts.Obstacles
{
    [ExecuteInEditMode]
    public class EditorGridAdjustment : MonoBehaviour
    {
        [SerializeField] private Vector3 size = new(0.64f, 0.64f);
        private Vector3 lastPosition;

//#if UNITY_EDITOR
        private void Update()
        {
            if (Vector3.Distance(lastPosition, transform.position) > 0.1f)
            {
                var newPos = FindNearestPointToGrid(transform.position);
                transform.position = newPos;
                lastPosition = transform.position;
            }
        }

        public Vector3 FindNearestPointToGrid(Vector3 worldPoint)
        {
            var pointX = (float)Math.Round(worldPoint.x / size.x) * size.x;
            var pointY = (float)Math.Round(worldPoint.y / size.y) * size.y;
            return new Vector3(pointX, pointY, 0);
        }

//#endif
    }
}