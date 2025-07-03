using BlockBlast.Scripts.Common.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class GameOverWindow : MonoBehaviour
    {
        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        public void OnPressedRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowWindow()
        {
            gameObject.Show();
        }
    }
}