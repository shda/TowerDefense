using Eflatun.SceneReference;
using TowerDefense.Scripts.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Game
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneReference gameScene;
        [SerializeField] private SceneReference selectLevelScene;

        public void OnPressSelectLislLevels()
        {
            SceneManager.LoadScene(selectLevelScene.Name);
        }
        
        public void OnPressStartGame()
        {
            LevelSelector.SelectedLevelIndex = 0; // Default level index
            SceneManager.LoadScene(gameScene.Name);
        }
    }
}