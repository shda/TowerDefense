using System.Threading;
using TowerDefense.Scripts.Input;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts
{
    public class Game : MonoBehaviour
    {
        private CancellationTokenSource _tokenSource;
        private InputSwitcher _inputSwitcher;
        
        [Inject]
        public void Construct(InputSwitcher inputSwitcher)
        {
            _inputSwitcher =  inputSwitcher;
        }
        
        private void Start()
        {
            _inputSwitcher.SwitchToPlayerControl();
        }
        
        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }
    }
}