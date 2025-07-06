using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lua;
using TowerDefense.Scripts.Enemies;
using TowerDefense.Scripts.Game;
using TowerDefense.Scripts.Stats;
using TowerDefense.Scripts.TileMap;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = System.Random;

namespace TowerDefense.Scripts.Lua
{
    [Serializable]
    [LuaObject]
    public partial class LuaProxy
    {
        [SerializeField] private TileMapLevel tileMapLevel;
        [SerializeField] private WindowError windowError;
        [SerializeField] private ObstacleOnMapMaker obstacleOnMapMaker;
        [SerializeField] private EnemySettingsCollection enemies;
        [SerializeField] private AddFirstTowersToStart addFirstTowersToStart;
        [SerializeField] private Transform rootObstacles;

        private Dictionary<string, EnemySettings> _enemySettingsDictionary = new();
        private EnemySpawner _enemySpawner;
        private LevelObjects _levelObjects;
        private GameStates _gameStates;
        private Random _random = new();
        

        [Inject]
        public void Construct(
            LevelObjects levelObjects, 
            EnemySpawner enemySpawner,
            GameStates gameStates)
        {
            _gameStates = gameStates;
            _levelObjects = levelObjects;
            _enemySpawner = enemySpawner;
            CreateDictionary();
        }

        private void CreateDictionary()
        {
            _enemySettingsDictionary.Clear();
            foreach (var enemy in enemies.Enemies)
            {
                _enemySettingsDictionary.TryAdd(enemy.tzName, enemy);
            }
        }

        [LuaMember]
        private async UniTask WaitDestroyAllEnemies(CancellationToken cancellation)
        {
            Debug.Log("WaitDestroyAllEnemies");
            
            while (_levelObjects.Enemies.Count > 0 && !cancellation.IsCancellationRequested)
            {
                await UniTask.Yield();
            }
        }

        [LuaMember]
        private void CompleteLevel()
        {
            Debug.Log("CompleteLevel");
            _gameStates.CallCompleteLevel();
        }

        [LuaMember]
        private void StartLevel()
        {
            Debug.Log("StartLevel");
            addFirstTowersToStart.StartLevel();
        }

        [LuaMember]
        private void SetCountStartTowersToPlayer(int count)
        {
            Debug.Log($"SetCountStartTowersToPlayer: {count}");
            addFirstTowersToStart.CountStartTowers = count;
        }

        [LuaMember]
        private void CreateObstacle(string enemyName, int x, int y, float rotation = 0f)
        {
            Debug.Log($"CreateObstacle: {enemyName}, Position: ({x}, {y}), Rotation: {rotation}");

            var obstacle = obstacleOnMapMaker.CreateObstacle(enemyName);

            var tilemap = tileMapLevel.LayerObstacles;
            var bounds = tilemap.localBounds;
            var bottomLeftLocal = bounds.min;
            var bottomLeftWorld = tilemap.transform.TransformPoint(bottomLeftLocal);

            Debug.Log(bottomLeftWorld);

            var size = obstacle.Size;


            var position = new Vector3(
                size.x * 0.5f + bottomLeftWorld.x + x * 0.64f,
                size.y * 0.5f + bottomLeftWorld.y + y * 0.64f, 0);
            var quaternion = Quaternion.Euler(0, 0, rotation);

            Debug.Log(position);
            obstacle.transform.SetParent(rootObstacles);
            obstacle.transform.position = position;
            obstacle.transform.rotation = quaternion;
        }

        [LuaMember]
        private void CreateEnemies(string enemyName, int count)
        {
            Debug.Log($"CreateEnemy: {enemyName}, Count: {count}");

            var name = enemyName;
            if (_enemySettingsDictionary.TryGetValue(name, out var value))
            {
                for (var i = 0; i < count; i++)
                {
                    CreateEnemy(value);
                }
            }
            else
            {
                var errorMessage = $"Enemy settings not found for name: {enemyName}";
                windowError.ShowError(" Lua CreateEnemies", errorMessage);
                Debug.LogError(errorMessage);
            }
        }

        [LuaMember]
        private async UniTask PauseSeconds(float seconds, CancellationToken cancellation)
        {
            Debug.Log($"PauseSeconds: {seconds}");

            if (cancellation.IsCancellationRequested)
            {
                return;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken: cancellation);
        }

        [LuaMember]
        //This is because math.random Return Double
        public int GetRandom(int min, int max)
        {
            var random = _random.Next(min, max + 1);
            Debug.Log($"GetRandom: {min} - {max} = {random}");
            return random;
        }

        [LuaMember]
        public void Print(LuaValue message)
        {
            Debug.Log($"LuaProxy.Print: {message}");
        }

        private Enemy CreateEnemy(EnemySettings settings)
        {
            var enemy = _enemySpawner.AddEnemyToMap(settings.enemyType);
            enemy.SetInfo(settings);
            return enemy;
        }
    }
}