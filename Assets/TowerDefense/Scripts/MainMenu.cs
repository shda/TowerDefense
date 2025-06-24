using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneReference gameScene;
        
        public void OnPressStartGame()
        {
            SceneManager.LoadScene(gameScene.Name);
        }
    }
}