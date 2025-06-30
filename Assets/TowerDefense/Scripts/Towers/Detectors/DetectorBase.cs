using TowerDefense.Scripts.Ui;
using TowerDefense.Scripts.Ui.DownMenu;
using UnityEngine;

namespace TowerDefense.Scripts.Towers.Detectors
{
    public abstract class DetectorBase : MonoBehaviour
    {
        [SerializeField] protected SpriteResizeToCollider spriteShowDetectRadius;
        public float RadiusMultiplier { get; set; } = 1.0f;
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