using BlockBlast.Scripts.Common.Extensions;
using Eflatun.SceneReference;
using TMPro;
using TowerDefense.Scripts.Common;
using TowerDefense.Scripts.Lua;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using GameStates = TowerDefense.Scripts.Stats.GameStates;

namespace TowerDefense.Scripts.Ui.GameSceneUi
{
    public class WindowPauseAndGameOver : MonoBehaviour
    {
        [SerializeField] private Transform rootWindow;
        [SerializeField] private LuaScriptsCollection levelsList;
        [SerializeField] private SceneReference mainMenuScene;
        
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private Button buttonNextLevel;
        [SerializeField] private Button buttonClose;
        
        private GameStates _gameStates;

        [Inject]
        public void Construct(GameStates gameStates)
        {
            _gameStates = gameStates;
        }

        private void Awake()
        {
            _gameStates.PlayerDied.Subscribe(_=> ShowGameOver()).AddTo(this);
            _gameStates.EnemyReachedPlayerBase.Subscribe(_ => ShowGameOver()).AddTo(this);
            _gameStates.CompleteLevel.Subscribe(_ => ShowCompleteLevel()).AddTo(this);
            _gameStates.Pause.Subscribe(_ => ShowPause()).AddTo(this);
        }
        
        private void ShowGameOver()
        {
            header.text = "Конец игры";
            buttonNextLevel.Hide();
            buttonClose.Hide();
            
            rootWindow.Show();
        }

        private void ShowCompleteLevel()
        {
            header.text = "Уровень завершён";
            if (IsCanShowNextLevel())
            {
                buttonNextLevel.Show();
            }
            
            buttonClose.Hide();
            rootWindow.Show();
        }

        private void ShowPause()
        {
            header.text = "Пауза";
            buttonNextLevel.Hide();
            rootWindow.Show();
            buttonClose.Show();
        }

        public void OnPressRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnPressNextLevel()
        {
            LevelSelector.SelectedLevelIndex++;
            OnPressRestart();
        }

        public void OnPressMainMenu()
        {
            SceneManager.LoadScene(mainMenuScene.Name);
        }

        public void OnPressButtonClose()
        {
            rootWindow.Hide();
        }

        private bool IsCanShowNextLevel()
        {
            return levelsList.LuaScripts.Length > LevelSelector.SelectedLevelIndex + 1;
        }
    }
}