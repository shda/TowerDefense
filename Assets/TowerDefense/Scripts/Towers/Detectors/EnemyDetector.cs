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
    }
}