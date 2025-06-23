using UnityEngine;

namespace TowerDefense.Scripts.Towers.Detectors
{
    public abstract class DetectorBase : MonoBehaviour
    {
        public abstract RaycastHit2D[] Detect();
        
        public abstract bool CheckStillDetect(GameObject target);
    }
}