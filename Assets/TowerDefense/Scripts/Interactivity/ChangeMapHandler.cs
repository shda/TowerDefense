using System;
using System.Linq;
using TowerDefense.Scripts.Input;
using TowerDefense.Scripts.Towers;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Interactivity
{
    public class ChangeMapHandler : MonoBehaviour , IPressListener, IPointListener
    {
        [SerializeField] private TowerCollection towerCollection;
        
        private InputSwitcher _inputSwitcher;

        private TowerBase _tower;
        
        [Inject]
        public void Construct(InputSwitcher inputSwitcher)
        {
            _inputSwitcher = inputSwitcher;
            _inputSwitcher.SetChangeMap(this);
            
            _inputSwitcher.SwitchToChangeMapHandlerControl();
        }

        private void Start()
        {
            var towerPrefab = towerCollection.TowersPrefs.First();
            _tower = Instantiate(towerPrefab);
        }

        public void OnPress(bool isPressed)
        {
            
        }

        public void OnPointWorld(Vector3 worldPosition)
        {
           if(_tower == null)
               return;
           
           _tower.transform.position = worldPosition;
        }

        public void OnPointMoving(Vector2 direction)
        {
           
        }
    }
}