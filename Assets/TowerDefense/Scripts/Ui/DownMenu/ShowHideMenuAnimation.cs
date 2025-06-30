using UnityEngine;

namespace TowerDefense.Scripts.Ui.DownMenu
{
    public class ShowHideMenuAnimation : MonoBehaviour
    {
        private static readonly int Show = Animator.StringToHash("show");
        
        [SerializeField] private Animator animator;

        public bool IsShow => animator.GetBool(Show);
        
        public void ShowMenu()
        {
            animator.SetBool(Show , true);
        }

        public void HideMenu()
        {
            animator.SetBool(Show , false);
        }

        public void PressButtonShowHide()
        {
            animator.SetBool(Show, !IsShow);
        }
    }
}