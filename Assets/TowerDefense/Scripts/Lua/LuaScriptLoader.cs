using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lua;
using Lua.Standard;
using Lua.Unity;
using TowerDefense.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Lua
{
    public class CreateMapObject : MonoBehaviour
    {
        [SerializeField] private ObjectsCollection obstacles;

        public void CreateObstacle(string nameObstacle, Vector3 position, Quaternion rotation)
        {
            var obstacle = obstacles.GetObject(nameObstacle);
            if (obstacle == null)
            {
                Debug.LogError($"Obstacle with name {nameObstacle} not found.");
                return;
            }

            var instance = Instantiate(obstacle, position, rotation);
            instance.transform.SetParent(transform);
        }
    }

    public class LuaScriptLoader : MonoBehaviour
    {
        [SerializeField] private LuaAsset luaAsset;
        [SerializeField] private LuaProxy luaProxy;

        private CancellationTokenSource _cancellation;

        private LuaState _luaState;

        private void Start()
        {
            StartLevel();
        }

        private void OnDestroy()
        {
            _cancellation?.Cancel();
        }

        [Inject]
        public void Construct(EnemySpawner enemySpawner)
        {
            luaProxy.Construct(enemySpawner);
        }

        public void StartLevel()
        {
            _cancellation?.Cancel();
            _cancellation = new CancellationTokenSource();

            // LoadLevelFromLuaLogic(luaAsset.Text , _cancellation.Token);


            var text = PlayerPrefs.GetString(LevelEditorController.NameSave);
            if (!string.IsNullOrEmpty(text))
            {
                LoadLevelFromLuaLogic(text, _cancellation.Token);
            }
            else
            {
                LoadLevelFromLuaLogic(luaAsset.Text, _cancellation.Token);
                PlayerPrefs.SetString(LevelEditorController.NameSave, luaAsset.Text);
            }
        }

        private async void LoadLevelFromLuaLogic(string luaCode, CancellationToken token)
        {
            await UniTask.Yield();

            try
            {
                _luaState = LuaState.Create();
                _luaState.OpenStandardLibraries();
                _luaState.Environment["level"] = luaProxy;
                await _luaState.DoStringAsync(luaCode, cancellationToken: token);
            }
            catch (LuaCanceledException luaCanceled)
            {
                Debug.Log(luaCanceled.Message);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void StopLevel()
        {
            _cancellation?.Cancel();
        }
    }
}