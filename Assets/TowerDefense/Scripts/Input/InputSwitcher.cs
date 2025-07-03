using TowerDefense.Scripts.PlayerScripts;
using TowerDefense.Scripts.Ui.DownMenu;
using Zenject;

namespace TowerDefense.Scripts.Input
{
    public class InputSwitcher
    {
        private AddTowerToMapController _addTowerToMapController;

        private InputListenerSwitcher _inputListenerSwitcher;
        private PlayerMovement _playerMovement;

        public void SetPlayerControl(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
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
            _inputListenerSwitcher.SetListener(_playerMovement);
        }

        public void SwitchToChangeMapHandlerControl()
        {
            _inputListenerSwitcher.SetListener((IPressListener)_addTowerToMapController);
            _inputListenerSwitcher.SetListener((IPointListener)_addTowerToMapController);
        }
    }
}