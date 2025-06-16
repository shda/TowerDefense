using System;
using TowerDefense.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace TowerDefense.Scripts
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private bool isDrawGizmos = true;
        private int[,] _field;

        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _field = new int[gameSettings.FieldSize.x, gameSettings.FieldSize.y];
        }

        private void OnDrawGizmos()
        {
            if (_field == null || !isDrawGizmos)
            {
                return;
            }

            Gizmos.color = Color.red;

            for (int i = 0; i < _field.GetLength(0); i++)
            {
                for (int j = 0; j < _field.GetLength(1); j++)
                {
                    Vector3 position = new Vector3(i, j, 0);
                    Gizmos.DrawWireCube(position, Vector3.one);
                }
            }
        }
    }
}
