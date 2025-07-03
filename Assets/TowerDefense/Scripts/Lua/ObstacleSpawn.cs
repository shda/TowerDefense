using TowerDefense.Scripts.Obstacles;
using UnityEngine;

namespace TowerDefense.Scripts.Lua
{
    public class ObstacleSpawn : MonoBehaviour
    {
        [SerializeField] private ObstacleCollection obstacles;

        public Obstacle CreateObstacle(string nameObstacle)
        {
            var obstacle = obstacles.GetObject(nameObstacle);
            if (obstacle == null)
            {
                Debug.LogError($"Obstacle with name {nameObstacle} not found.");
                return null;
            }

            var instance = Instantiate(obstacle, transform, true);
            return instance;
        }
    }
}