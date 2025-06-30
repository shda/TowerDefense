using System;
using BlockBlast.Scripts.Common.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Game
{
    public class GameOverWindow : MonoBehaviour
    {
        public void OnPressedRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowWindow()
        {
            gameObject.Show();
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }
        
        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}