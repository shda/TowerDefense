using TowerDefense.Scripts.Ui;
using UnityEngine;

namespace TowerDefense.Scripts.Towers.Detectors
{
    public abstract class DetectorBase : MonoBehaviour
    {
        [SerializeField] protected SpriteResizeToCollider spriteShowDetectRadius;
        public abstract RaycastHit2D[] Detect();
        
        public abstract bool CheckStillDetect(GameObject target);

        public virtual void ShowDetectRadius()
        {
            spriteShowDetectRadius.ShowSelectRect();
        }

        public virtual void HideDetectRadius()
        {
            spriteShowDetectRadius.HideSelectRect();
        }
    }
}