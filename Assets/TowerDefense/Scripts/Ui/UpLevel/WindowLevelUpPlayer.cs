using System;
using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.PlayerScripts;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    public class WindowLevelUpPlayer : MonoBehaviour
    {
        [SerializeField] private LevelUpItem itemPrefab;
        [SerializeField] private Transform itemParent;
        
        [SerializeField] private UpPlayerStatsCollection upPlayerStatsCollection;
        [SerializeField] private TowerSettingsCollection towerSettingsCollection;
        [SerializeField] private ClearCellItemSettings clearCellItemSettings;

        public Action OnSelectItem { get; set; }
        
        private PlayerModifiers _playerModifiers;
        private PlayerInventory _playerInventory;

        [Inject]
        public void Construct(PlayerModifiers playerModifiers , PlayerInventory playerInventory)
        {
            _playerModifiers = playerModifiers;
            _playerInventory = playerInventory;
        }

        private void OnEnable()
        {
            Time.timeScale = 0.0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;
        }

        private void HideWindow()
        {
            transform.Hide();
        }

        public void RemoveOldItems()
        {
            itemParent.DestroyAllChildren();
        }

        public void CreateClearCellItem()
        {
            CreateItem(clearCellItemSettings.Icon, clearCellItemSettings.Description, item =>
            {
                _playerInventory.AddItem(clearCellItemSettings.Description);
                HideWindow();
                
                OnSelectItem?.Invoke();
            });
        }

        public void CreateTowerItem(AddTowerSettings towerSettings)
        {
            CreateItem(towerSettings.Icon,towerSettings.Description, item =>
            {
                _playerInventory.AddItem(towerSettings.TowerPrefab.TowerComponents.TowerId);
                HideWindow();
                
                OnSelectItem?.Invoke();
            });
        }

        public void CreatePlayerUpItem(LevelUpPlayerStats playerStats)
        {
            CreateItem(playerStats.Icon, playerStats.Description, item =>
            {
                playerStats.UpdatePlayerStats(_playerModifiers);
                HideWindow();
                
                OnSelectItem?.Invoke();
            });
        }

        private void CreateItem(Sprite sprite, string text, Action<LevelUpItem> pressItem)
        {
            var item = Instantiate(itemPrefab, itemParent);
            item.SetInfo(sprite, text, pressItem);
        }
    }
}