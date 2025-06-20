using TowerDefense.Scripts.Interactivity;
using TowerDefense.Scripts.PlayerScripts;
using Zenject;

namespace TowerDefense.Scripts.Input
{
    public class InputSwitcher
    {
        private PlayerControl _playerControl;
        private PlayerShooting _playerShooting;
        private ChangeMapHandler _changeMapHandler;
        
        private InputListenerSwitcher  _inputListenerSwitcher;

        public void SetPlayerControl(PlayerControl playerControl)
        {
            _playerControl = playerControl;
        }

        public void SetShooting(PlayerShooting playerShooting)
        {
            _playerShooting = playerShooting;
        }

        public void SetChangeMap(ChangeMapHandler changeMapHandler)
        {
            _changeMapHandler = changeMapHandler;
        }
        
        [Inject]
        public void Construct(InputListenerSwitcher inputListenerSwitcher)
        {
            _inputListenerSwitcher = inputListenerSwitcher;
        }

        public void SwitchToPlayerControl()
        {
            _inputListenerSwitcher.SetListener(_playerControl);
            _inputListenerSwitcher.SetListener(_playerShooting);
        }

        public void SwitchToChangeMapHandlerControl()
        {
            _inputListenerSwitcher.SetListener((IPressListener)_changeMapHandler);
            _inputListenerSwitcher.SetListener((IPointListener)_changeMapHandler);
        }
    }
}