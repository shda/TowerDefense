using System.Linq;
using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Interactivity;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Ui
{
    public class MenuSelectItem : MonoBehaviour
    {
        [SerializeField] private AddTowerToMapController addTowerToMapController;
        [SerializeField] private ShowHideMenuAnimation menuAnimation;
        [SerializeField] private TowerCollection towerCollection;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private DragItem itemPrefab;
        [SerializeField] private Transform itemsRoot;

        [SerializeField] private SpriteResizeToCollider spriteResizeToCollider;
        [SerializeField] private Sprite spriteRemoveItem;

        private void Start()
        {
            spriteResizeToCollider.Hide();
            foreach (var ch in itemsRoot.Cast<Transform>().ToList())
            {
                DestroyImmediate(ch.gameObject);
            }

            CreateRemoveItem();
            CreateTowerItems();
        }

        private void CreateRemoveItem()
        {
            var prefab = Instantiate(itemPrefab, itemsRoot);
            prefab.gameObject.SetActive(true);

            RemoverDragHandler mapDragHandler = new(spriteRemoveItem , spriteResizeToCollider);

            prefab.SetHandler(mapDragHandler, scrollRect);
            prefab.OnSelect = OnSelect;
        }

        private void CreateTowerItems()
        {
            foreach (var tower in towerCollection.TowersPrefs)
            {
                var prefab = Instantiate(itemPrefab, itemsRoot);
                prefab.gameObject.SetActive(true);

                TowerDragHandler mapDragHandler = new(tower);

                prefab.SetHandler(mapDragHandler, scrollRect);
                prefab.OnSelect = OnSelect;
            }
        }

        private void OnSelect(DragItem item)
        {
            if (!menuAnimation.IsShow)
                return;

            addTowerToMapController.StartDrag(item.MapDragHandler);
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