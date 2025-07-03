using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Lua
{
    public class LevelEditorController : MonoBehaviour
    {
        public static string NameSave = "Editor7";
        [SerializeField] private SceneReference gameSceneReference;
        [SerializeField] private LuaCodeEditor inputField;
        [SerializeField] private GameObject editor;

        public void OnPressButtonOpenEditor()
        {
            if (editor.activeInHierarchy)
            {
                editor.SetActive(false);
                // Restart();
            }
            else
            {
                editor.SetActive(true);
                OpenEditor();
            }
        }

        private void OpenEditor()
        {
            var ed = PlayerPrefs.GetString(NameSave);
            if (!string.IsNullOrEmpty(ed)) inputField.text = ed;
        }

        public void Restart()
        {
            PlayerPrefs.SetString(NameSave, inputField.text);
            ReloadSceneAsync().Forget();
            //game.ResetLevelToDefault();
        }

        private async UniTask ReloadSceneAsync()
        {
            await SceneManager.UnloadSceneAsync(gameSceneReference.Name);
            await SceneManager.LoadSceneAsync(gameSceneReference.Name, LoadSceneMode.Additive);
        }
    }
}