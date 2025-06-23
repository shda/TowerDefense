using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinder4
{
    private class Node
    {
        public Vector3Int Position;
        public Node Parent;
        public int G; // путь от старта
        public int H; // эвристика до цели
        public int F => G + H;
    }

    public static List<Vector3Int> FindPath(
        Vector3Int start,
        Vector3Int goal,
        Vector3Int minBounds,
        Vector3Int maxBounds,
        HashSet<Vector3Int> obstacles,
        bool allowDiagonal = false)
    {
        var openList = new List<Node>();
        var closedSet = new HashSet<Vector3Int>();

        openList.Add(new Node
        {
            Position = start,
            G = 0,
            H = Heuristic(start, goal)
        });

        while (openList.Count > 0)
        {
            var current = openList.OrderBy(n => n.F).First();

            if (current.Position == goal)
                return ReconstructPath(current);

            openList.Remove(current);
            closedSet.Add(current.Position);

            foreach (var neighbor in GetNeighbors(current.Position, allowDiagonal))
            {
                if (closedSet.Contains(neighbor) || obstacles.Contains(neighbor) || !IsInsideBounds(neighbor, minBounds, maxBounds))
                    continue;

                int moveCost = (neighbor - current.Position).sqrMagnitude == 2 ? 14 : 10;
                int tentativeG = current.G + moveCost;

                var existing = openList.FirstOrDefault(n => n.Position == neighbor);
                if (existing == null)
                {
                    openList.Add(new Node
                    {
                        Position = neighbor,
                        Parent = current,
                        G = tentativeG,
                        H = Heuristic(neighbor, goal)
                    });
                }
                else if (tentativeG < existing.G)
                {
                    existing.G = tentativeG;
                    existing.Parent = current;
                }
            }
        }

        return null; // путь не найден
    }

    private static List<Vector3Int> ReconstructPath(Node node)
    {
        var path = new List<Vector3Int>();
        while (node != null)
        {
            path.Add(node.Position);
            node = node.Parent;
        }
        path.Reverse();
        return path;
    }

    private static int Heuristic(Vector3Int a, Vector3Int b)
    {
        return 10 * (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
    }

    private static IEnumerable<Vector3Int> GetNeighbors(Vector3Int pos, bool allowDiagonal)
    {
        var dirs = allowDiagonal ? Directions8 : Directions4;
        foreach (var dir in dirs)
            yield return pos + dir;
    }

    private static bool IsInsideBounds(Vector3Int point, Vector3Int min, Vector3Int max)
    {
        return point.x >= min.x && point.y >= min.y && point.x <= max.x && point.y <= max.y;
    }

    private static readonly Vector3Int[] Directions4 = new[]
    {
        new Vector3Int(0, 1),
        new Vector3Int(1, 0),
        new Vector3Int(0, -1),
        new Vector3Int(-1, 0),
    };

    private static readonly Vector3Int[] Directions8 = new[]
    {
        new Vector3Int(0, 1),
        new Vector3Int(1, 0),
        new Vector3Int(0, -1),
        new Vector3Int(-1, 0),
        new Vector3Int(1, 1),
        new Vector3Int(-1, 1),
        new Vector3Int(1, -1),
        new Vector3Int(-1, -1),
    };
}