using TowerDefense.Scripts.Stats;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class ButtonPause : MonoBehaviour
    {
        private GameStates _gameStates;

        [Inject]
        public void Construct(GameStates gameStates)
        {
            _gameStates = gameStates;
        }
        
        public void OnPressButtonPause()
        {
            _gameStates.CallPause();
        }
    }
}