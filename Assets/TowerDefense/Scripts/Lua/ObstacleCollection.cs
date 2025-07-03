using TowerDefense.Scripts.Obstacles;
using UnityEngine;

namespace TowerDefense.Scripts.Lua
{
    [CreateAssetMenu(menuName = "Create TowerDefense/ObstacleCollection", fileName = "ObstacleCollection", order = 0)]
    public class ObstacleCollection : ObjectsCollection<Obstacle>
    {
        
    }
}