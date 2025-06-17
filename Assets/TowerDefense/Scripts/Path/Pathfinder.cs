using System.Collections.Generic;
using Priority_Queue;

namespace TowerDefense.Scripts.Path
{
    public class Pathfinder
    {
        private readonly bool[,] _grid;
        private readonly bool _allowDiagonal;

        public Pathfinder(bool[,] grid, bool allowDiagonal = false)
        {
            _grid = grid;
            _allowDiagonal = allowDiagonal;
        }

        public List<Point> FindPath(Point start, Point end)
        {
            if (!IsValid(start) || !IsValid(end))
                return null;

            var openSet = new  SimplePriorityQueue<Point, int>();
            openSet.Enqueue(start, 0);

            var cameFrom = new Dictionary<Point, Point>();
            var gScore = new Dictionary<Point, int> { [start] = 0 };
            var fScore = new Dictionary<Point, int> { [start] = Heuristic(start, end) };

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                if (current.Equals(end))
                    return ReconstructPath(cameFrom, current);

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

            return null; // Путь не найден
        }

        private IEnumerable<Point> GetNeighbors(Point p)
        {
            // Основные направления (вверх, вправо, вниз, влево)
            var directions = new List<Point>
            {
                new Point(0, 1), new Point(1, 0),
                new Point(0, -1), new Point(-1, 0)
            };

            // Добавляем диагональные направления, если разрешено
            if (_allowDiagonal)
            {
                directions.AddRange(new[]
                {
                    new Point(1, 1), new Point(1, -1),
                    new Point(-1, -1), new Point(-1, 1)
                });
            }

            foreach (var dir in directions)
            {
                var neighbor = new Point(p.X + dir.X, p.Y + dir.Y);
                if (IsValid(neighbor) && !_grid[neighbor.X, neighbor.Y])
                    yield return neighbor;
            }
        }

        private bool IsValid(Point p)
        {
            return p.X >= 0 && p.X < _grid.GetLength(0) &&
                   p.Y >= 0 && p.Y < _grid.GetLength(1);
        }

        private int Heuristic(Point a, Point b) => a.DistanceTo(b, _allowDiagonal);

        private List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            var path = new List<Point> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
            }
            return path;
        }
    }
}
