using System;
using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Interactivity;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class MenuSelectItem : MonoBehaviour
    {
        [SerializeField] private AddTowerToMapController addTowerToMapController;
        [SerializeField] private ShowHideMenuAnimation menuAnimation;
        [SerializeField] private TowerItem selectItem;
        
        private InputSwitcher _inputSwitcher;

        [Inject]
        public void Construct(InputSwitcher inputSwitcher)
        {
            _inputSwitcher = inputSwitcher;
        }
        
        private void OnSelect(TowerItem item)
        {
            selectItem.OnSelect = null;
            addTowerToMapController.StartDrag(item.SelectTower);
            menuAnimation.HideMenu();
        }

        public void OnPressShowMenu()
        {
            var isShow = menuAnimation.IsShow;
            if (isShow)
            {
                menuAnimation.HideMenu();
            }
            else
            {
                selectItem.OnSelect = OnSelect;
                menuAnimation.ShowMenu();
            }
        }
    }
}