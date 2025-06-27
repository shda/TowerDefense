using TowerDefense.Scripts.Interactivity;
using TowerDefense.Scripts.PlayerScripts;
using Zenject;

namespace TowerDefense.Scripts.Input
{
    public class InputSwitcher
    {
        private PlayerControl _playerControl;
        private AddTowerToMapController _addTowerToMapController;
        
        private InputListenerSwitcher  _inputListenerSwitcher;

        public void SetPlayerControl(PlayerControl playerControl)
        {
            _playerControl = playerControl;
        }
        
        public void SetChangeMap(AddTowerToMapController addTowerToMapController)
        {
            _addTowerToMapController = addTowerToMapController;
        }
        
        [Inject]
        public void Construct(InputListenerSwitcher inputListenerSwitcher)
        {
            _inputListenerSwitcher = inputListenerSwitcher;
        }

        public void SwitchToPlayerControl()
        {
            _inputListenerSwitcher.SetListener(_playerControl);
        }

        public void SwitchToChangeMapHandlerControl()
        {
            _inputListenerSwitcher.SetListener((IPressListener)_addTowerToMapController);
            _inputListenerSwitcher.SetListener((IPointListener)_addTowerToMapController);
        }
    }
}