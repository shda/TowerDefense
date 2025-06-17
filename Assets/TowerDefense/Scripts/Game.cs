using System;
using TowerDefense.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts
{
    public class Game : MonoBehaviour
    {
        private EnemyController _enemyController;
        
        [Inject]
        public void Construct(EnemyController enemyController)
        {
            _enemyController = enemyController;
        }


        private void Start()
        {
            _enemyController.AddEnemyToMap();
        }
    }
}