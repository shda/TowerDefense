using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lua;
using TowerDefense.Scripts.Enemies;
using UnityEngine;
using Random = System.Random;

namespace TowerDefense.Scripts.Lua
{
    [Serializable]
    [LuaObject]
    public partial class LuaProxy
    {
        [SerializeField] private CreateMapObject createMapObject;

        [SerializeField] private EnemySettingsCollection enemies;
        // [SerializeField] private ObjectsCollection obstacles;

        private Dictionary<string, EnemySettings> _enemySettingsDictionary = new();
        private EnemySpawner _enemySpawner;

        private Random _random = new();


        public Action OnEnemyHitToBase;

        public void Construct(EnemySpawner enemySpawner)
        {
            Debug.Log("LuaProxy.Construct");
            _enemySpawner = enemySpawner;

            CreateDictionary();
        }

        private void CreateDictionary()
        {
            _enemySettingsDictionary.Clear();
            foreach (var enemy in enemies.Enemies) _enemySettingsDictionary.TryAdd(enemy.tzName, enemy);
        }


        [LuaMember]
        private void CreateRandomEnemyCount(LuaTable enemyNames, int count)
        {
            var names = enemyNames.ToArray<string>();
            Debug.Log($"CreateRandomEnemyCount: {string.Join(", ", names)} Count: {count}");

            Debug.Log(enemyNames);
        }

        [LuaMember]
        private void CreateEnemies(string enemyName, int count)
        {
            Debug.Log($"CreateEnemy: {enemyName}, Count: {count}");

            var name = enemyName;
            if (_enemySettingsDictionary.TryGetValue(name, out var value))
                for (var i = 0; i < count; i++)
                    CreateEnemy(value);
            else
                Debug.LogError($"Enemy settings not found for name: {enemyName}");
        }

        [LuaMember]
        private async UniTask PauseSeconds(float seconds, CancellationToken cancellation)
        {
            Debug.Log($"PauseSeconds: {seconds}");

            if (cancellation.IsCancellationRequested)
                return;

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
            enemy.OnDestinationReached = OnHitToBase;

            return enemy;
        }

        private void OnHitToBase(Enemy enemy)
        {
            OnEnemyHitToBase?.Invoke();
        }

        /*
        [LuaMember]
        private void SetObstacle(int index, int angle , CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                return;

            Debug.Log("SetObstacle");

            var knife = listKnifes.GetKnife(); // Object.Instantiate(listKnifes.GetWithOverflow(index));
            knife.SetSkinIndex(index);
            knife.IsMoving = false;
            knife.SetStaticRigidbody2D();

            target.AddObject(knife, angle, 180);
        }
        */


        /*
        [LuaMember]
        private async UniTask<int> RotateAsync(string rotationString, CancellationToken cancellation)
        {
            await UniTask.Yield();
            Debug.Log("Rotate");

            if (cancellation.IsCancellationRequested)
                return 0;

            var result = RotationsParser.ParseLine(rotationString);

            if (cancellation.IsCancellationRequested)
                return 0;

            await rotatorHandler.PlayStepAsync(result, cancellation);

            return 0;
        }
        */
    }
}