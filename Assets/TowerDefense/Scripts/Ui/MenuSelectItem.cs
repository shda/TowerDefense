using System;
using System.Linq;
using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Interactivity;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefense.Scripts.Ui
{
    public class MenuSelectItem : MonoBehaviour
    {
        [SerializeField] private AddTowerToMapController addTowerToMapController;
        [SerializeField] private ShowHideMenuAnimation menuAnimation;
        [SerializeField] private TowerCollection towerCollection;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private TowerItem itemPrefab;
        [SerializeField] private Transform itemsRoot;
        
        private void Start()
        {
            foreach (var ch in itemsRoot.Cast<Transform>().ToList())
            {
                DestroyImmediate(ch.gameObject);
            }
            
            foreach (var tower in towerCollection.TowersPrefs)       
            {
                var prefab = Instantiate(itemPrefab , itemsRoot);
                prefab.gameObject.SetActive(true);
                prefab.SetPrefab(tower , scrollRect);
                prefab.OnSelect = OnSelect;
            }
        }

        private void OnSelect(TowerItem item)
        {
            if(!menuAnimation.IsShow)
                return;
            
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
                menuAnimation.ShowMenu();
            }
        }
    }
}