using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

namespace TowerDefense.Scripts.Path
{
    public class Pathfinder : IPathfinder
    {
        private readonly bool[,] _grid;
        private readonly bool _allowDiagonal;

        public Pathfinder(bool[,] grid, bool allowDiagonal = false)
        {
            _grid = grid;
            _allowDiagonal = allowDiagonal;
        }

        public bool FindPath(List<Vector3Int> findPath , Vector3Int start, Vector3Int end)
        {
            if (!IsValid(start) || !IsValid(end))
                return false;
            
            findPath.Clear();
            
            var openSet = new  SimplePriorityQueue<Vector3Int, int>();
            openSet.Enqueue(start, 0);

            var cameFrom = new Dictionary<Vector3Int, Vector3Int>();
            var gScore = new Dictionary<Vector3Int, int> { [start] = 0 };
            var fScore = new Dictionary<Vector3Int, int> { [start] = Heuristic(start, end) };

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                if (current.Equals(end))
                {
                    ReconstructPath(findPath, cameFrom, current);
                    return true;
                }

                foreach (var neighbor in GetNeighbors(current))
                {
                    int tentativeGScore = gScore[current] + 1;

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = tentativeGScore + Heuristic(neighbor, end);
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }

            return false; // Путь не найден
        }

        private IEnumerable<Vector3Int> GetNeighbors(Vector3Int p)
        {
            // Основные направления (вверх, вправо, вниз, влево)
            var directions = new List<Vector3Int>
            {
                new(0, 1), new(1, 0),
                new(0, -1), new(-1, 0)
            };

            // Добавляем диагональные направления, если разрешено
            if (_allowDiagonal)
            {
                directions.AddRange(new[]
                {
                    new Vector3Int(1, 1), new Vector3Int(1, -1),
                    new Vector3Int(-1, -1), new Vector3Int(-1, 1)
                });
            }

            foreach (var dir in directions)
            {
                var neighbor = new Vector3Int(p.x + dir.x, p.y + dir.y);
                if (IsValid(neighbor) && !_grid[neighbor.x, neighbor.y])
                    yield return neighbor;
            }
        }

        private bool IsValid(Vector3Int p)
        {
            return p.x >= 0 && p.x < _grid.GetLength(0) &&
                   p.y >= 0 && p.y < _grid.GetLength(1);
        }

        private int Heuristic(Vector3Int a, Vector3Int b) => Distance(a , b, _allowDiagonal);

        private int Distance(Vector3Int a , Vector3Int b, bool allowDiagonal)
        {
            int dx = Mathf.Abs(a.x - b.x);
            int dy = Mathf.Abs(a.y - b.y);

            return allowDiagonal ? Mathf.Max(dx, dy) : dx + dy; // Манхэттенское расстояние или евклидово
        }

        private void ReconstructPath(List<Vector3Int> findPath, Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
        {
            findPath.Clear();
            findPath.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                findPath.Insert(0, current);
            }
        }
    }
}
