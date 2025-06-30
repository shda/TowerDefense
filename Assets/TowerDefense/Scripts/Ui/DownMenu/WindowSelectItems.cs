using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.Interactivity;
using TowerDefense.Scripts.PlayerScripts;
using TowerDefense.Scripts.Towers;
using TowerDefense.Scripts.Towers.TowersTypes;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefense.Scripts.Ui.DownMenu
{
    public class WindowSelectItems : MonoBehaviour
    {
        [SerializeField] private AddTowerToMapController addTowerToMapController;
        [SerializeField] private ShowHideMenuAnimation menuAnimation;
        [SerializeField] private ItemsCollection itemsCollection;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private DragItem itemPrefab;
        [SerializeField] private Transform itemsRoot;

        [SerializeField] private SpriteResizeToCollider spriteResizeToCollider;
        [SerializeField] private Sprite spriteRemoveItem;
        private PlayerInventory _playerInventory;
        
        [Inject]
        public void Construct(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
        }
        private void Start()
        {
            spriteResizeToCollider.Hide();
            _playerInventory.OnChangeCollection.Subscribe(_ => CreateItems()).AddTo(this);
        }

        private void CreateItems()
        {
            itemsRoot.DestroyAllChildren();
            var items = _playerInventory.Items;
            foreach (var item in items)
            {
                if(item.Value == 0)
                    continue;
                if (item.Key == itemsCollection.IdRemoveCell)
                {
                    CreateRemoveItem(item.Value);
                    continue;
                }

                foreach (var towerBase in itemsCollection.TowersPrefs)
                {
                    if(towerBase.TowerComponents.TowerId == item.Key)
                    {
                        CreateTowerItems(towerBase , item.Value);
                        break;
                    }
                }
            }
        }

        private void CreateRemoveItem(int count)
        {
            var prefab = Instantiate(itemPrefab, itemsRoot);
            prefab.gameObject.SetActive(true);

            RemoverDragHandler mapDragHandler = new(spriteRemoveItem , spriteResizeToCollider);

            prefab.SetHandler(mapDragHandler, count , scrollRect);
            prefab.OnSelect = OnSelect;
            
            mapDragHandler.OnSuccess = _ =>
            {
                _playerInventory.RemoveItem(itemsCollection.IdRemoveCell);
            };
        }
        
        private void CreateTowerItems(TowerBase towerBase, int count)
        {
            var prefab = Instantiate(itemPrefab, itemsRoot);
            prefab.gameObject.SetActive(true);

            TowerDragHandler mapDragHandler = new(towerBase);

            prefab.SetHandler(mapDragHandler, count , scrollRect);
            prefab.OnSelect = OnSelect;
            
            mapDragHandler.OnSuccess = _ =>
            {
                _playerInventory.RemoveItem(towerBase.TowerComponents.TowerId);
            };
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
                CreateItems();
                menuAnimation.HideMenu();
            }
            else
            {
                menuAnimation.ShowMenu();
            }
        }
    }
}