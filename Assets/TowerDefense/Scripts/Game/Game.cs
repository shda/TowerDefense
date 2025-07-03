using TowerDefense.Scripts.Input;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Game
{
    public class Game : MonoBehaviour
    {
        private InputSwitcher _inputSwitcher;

        private void Start()
        {
            _inputSwitcher.SwitchToPlayerControl();
        }

        [Inject]
        public void Construct(InputSwitcher inputSwitcher)
        {
            _inputSwitcher = inputSwitcher;
        }
    }
}