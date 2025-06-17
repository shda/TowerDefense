using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Scripts.Path
{
    public interface IPathfinder
    {
        bool FindPath(List<Vector3Int> findPath, Vector3Int start, Vector3Int end);
    }
}