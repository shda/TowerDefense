using UnityEngine;

namespace TowerDefense.Scripts.Towers.Detectors
{
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private DetectorBase detector;

        public RaycastHit2D[] Detect()
        {
            return detector.Detect();
        }

        public bool CheckStillDetect(GameObject target)
        {
            return detector.CheckStillDetect(target);
        }

        public void ShowDetectRadius()
        {
            detector.ShowDetectRadius();
        }

        public virtual void HideDetectRadius()
        {
            detector.HideDetectRadius();
        }

        public void SetMultiplierRadius(float multiplier)
        {
            detector.RadiusMultiplier = multiplier;
        }
    }
}