using System;
using UnityEngine;

namespace TowerDefense.Scripts.Enemies
{
    public class BaseMovement : MonoBehaviour
    {
        public virtual float SpeedMoving { get; set; }
        public Action OnMovingComplete { get; set; }

        public virtual void Stop()
        {
            
        }

        public virtual void Repath()
        {
        }

        public virtual void CustomUpdate()
        {
        }

        public virtual void StartMoving(Vector3 movingPath)
        {
        }
        public virtual void SetStartPosition(Vector3 worldPosition)
        {
        }
    }
}