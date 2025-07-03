using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lua;
using Lua.Standard;
using Lua.Unity;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts.Lua
{
    public class LuaScriptLoader : MonoBehaviour
    {
        [SerializeField] private LuaAsset luaAsset;
        [SerializeField] private LuaProxy luaProxy;
        [SerializeField] private WindowError windowError;

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
        public void Construct(DiContainer container)
        {
            container.Inject(luaProxy);
        }

        private void StartLevel()
        {
            _cancellation?.Cancel();
            _cancellation = new CancellationTokenSource();
            
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
            catch (LuaCompileException compileException)
            {
                var message = "Ошибка в строке: " + compileException.Position.Line + "\n\n" + compileException.Message;
                windowError.ShowError("LuaCompileException", message);
            }
            catch (LuaRuntimeException runtime)
            {
                windowError.ShowError("LuaRuntimeException", runtime.Message);
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