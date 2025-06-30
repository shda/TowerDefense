using System;
using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace TowerDefense.Scripts.Ui.UpLevel
{
    public class WindowUpLevelPlayer : MonoBehaviour
    {
        [SerializeField] private UpLevelItem itemPrefab;
        [SerializeField] private Transform itemParent;

        [Header("Up Player Stats")]
        [SerializeField] private UpPlayerStats upPlayerRadius;
        [SerializeField] private UpPlayerStats upPlayerDamage;
        [SerializeField] private UpPlayerStats upPlayerHealth;
        [SerializeField] private UpPlayerStats upPlayerCountTargetOneShot;

        [Header("Towers")]
        [SerializeField] private TowerCollection towerCollection;
        
        [Header("Other")]
        [SerializeField] private ClearCellItemSettings clearCellItemSettings;

        private PlayerStats _playerStats;
        private PlayerInventory _playerInventory;

        [Inject]
        public void Construct(PlayerStats playerStats , PlayerInventory playerInventory)
        {
            _playerStats = playerStats;
            _playerInventory = playerInventory;
        }

        private void Start()
        {
            RemoveOldItems();

            CreateUpPlayerItem(upPlayerRadius);
            CreateUpPlayerItem(upPlayerDamage);
            CreateUpPlayerItem(upPlayerHealth);
            CreateUpPlayerItem(upPlayerCountTargetOneShot);

            CreateTowerItem(towerCollection.ArtilleryTower);
            CreateTowerItem(towerCollection.DownSpeedTower);
            CreateTowerItem(towerCollection.FlyTower);
            CreateTowerItem(towerCollection.GunTower);
            CreateTowerItem(towerCollection.PoisonTower);

            CreateClearCellItem();
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
           // transform.Hide();
        }

        private void RemoveOldItems()
        {
            itemParent.DestroyAllChildren();
        }

        private void CreateClearCellItem()
        {
            CreateItem(clearCellItemSettings.Icon, clearCellItemSettings.Description, item =>
            {
                _playerInventory.AddItem(clearCellItemSettings.Description);
                HideWindow();
            });
        }

        private void CreateTowerItem(AddTowerSettings towerSettings)
        {
            CreateItem(towerSettings.Icon,towerSettings.Description, item =>
            {
                _playerInventory.AddItem(towerSettings.TowerPrefab.TowerComponents.TowerId);
                HideWindow();
            });
        }

        private void CreateUpPlayerItem(UpPlayerStats playerStats)
        {
            CreateItem(playerStats.Icon, playerStats.Description, item =>
            {
                playerStats.UpdatePlayerStats(_playerStats);
                HideWindow();
            });
        }

        private void CreateItem(Sprite sprite, string text, Action<UpLevelItem> pressItem)
        {
            var item = Instantiate(itemPrefab, itemParent);
            item.SetInfo(sprite, text, pressItem);
        }
    }
}