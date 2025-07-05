using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Common
{
    public class LuaEditorSceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneReference editorSceneReference;

        private void Start()
        {
            if (LevelSelector.LoadeEditorScene)
            {
                LoadSceneAsync();
            }
        }

        private async UniTask LoadSceneAsync()
        {
            var editorScene = SceneManager.GetSceneByName(editorSceneReference.Name);
            if (!editorScene.isLoaded)
            {
                await SceneManager.LoadSceneAsync(editorSceneReference.Name, LoadSceneMode.Additive);
                editorScene = SceneManager.GetSceneByName(editorSceneReference.Name);
                SceneManager.SetActiveScene(editorScene);
            }
        }
    }
}