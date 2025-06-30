using System;
using BlockBlast.Scripts.Common.Extensions;
using TowerDefense.Scripts.PlayerScripts;
using UnityEngine;
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
        
        private PlayerStats _playerStats;

        [Inject]
        public void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
        
        private void Start()
        {
            RemoveOldItems();
            
            CreateUpPlayerItem(upPlayerRadius);
            CreateUpPlayerItem(upPlayerDamage);
            CreateUpPlayerItem(upPlayerHealth);
            CreateUpPlayerItem(upPlayerCountTargetOneShot); 
        }

        private void RemoveOldItems()
        {
            itemParent.DestroyAllChildren();
        }

        private void CreateUpPlayerItem(UpPlayerStats playerStats)
        {
            CreateItem(playerStats.Icon, playerStats.Description, item =>
            {
                playerStats.UpdatePlayerStats(_playerStats);
                transform.Hide();
            });
        }

        private void CreateItem(Sprite sprite, string text, Action<UpLevelItem> pressItem)
        {
            var item = Instantiate(itemPrefab, itemParent);
            item.SetInfo(sprite, text, pressItem);
        }
    }
}