using System.Linq;
using Eflatun.SceneReference;
using TowerDefense.Scripts.Common;
using TowerDefense.Scripts.Lua;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense.Scripts.SelectLevel
{
    public class LevelSelectGrid : MonoBehaviour
    {
        [SerializeField] private LevelButton levelButtonPrefab;
        [SerializeField] private Transform rootButtons;
        [SerializeField] private LuaScriptsCollection levelsList;
        [SerializeField] private SceneReference gameScene;
        [SerializeField] private SceneReference sceneEditor;
        [SerializeField] private Toggle toggleEditor;

        private void Awake()
        {
            toggleEditor.isOn = LevelSelector.LoadeEditorScene;
            
            var childrens = rootButtons.Cast<Transform>().ToArray();
            foreach (var children in childrens)
            {
                DestroyImmediate(children.gameObject);
            }
            
            /*
            var editorButton = Instantiate(levelButtonPrefab, rootButtons);
            editorButton.SetLevel(0 , "Editor");
            editorButton.OnClick = () =>
            {
                LevelSelector.SelectedLevelIndex = 0;
                LevelSelector.LoadeEditorScene = toggleEditor.isOn;
                SceneManager.LoadScene(sceneEditor.Name);
            };
            */
            
            for (var i = 0; i < levelsList.LuaScripts.Length; i++)
            {
                var newButton = Instantiate(levelButtonPrefab, rootButtons);
                newButton.SetLevel(i , $"{i + 1}");

                var i1 = i;
                newButton.OnClick = () =>
                {
                    LevelSelector.SelectedLevelIndex = i1;
                    LevelSelector.LoadeEditorScene = toggleEditor.isOn;;
                    SceneManager.LoadScene(gameScene.Name);
                };
            }
        }
    }
}