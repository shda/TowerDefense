using System;

namespace TowerDefense.Scripts.Path
{
    public class Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point other) => other != null && X == other.X && Y == other.Y;
        public override bool Equals(object obj) => Equals(obj as Point);
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public override string ToString() => $"({X}, {Y})";

        // Расстояние до другой точки (манхэттенское или евклидово)
        public int DistanceTo(Point other, bool allowDiagonal)
        {
            int dx = Math.Abs(X - other.X);
            int dy = Math.Abs(Y - other.Y);
        
            return allowDiagonal ? 
                Math.Max(dx, dy) :  // Для диагоналей считаем максимум
                dx + dy;            // Манхэттенское расстояние
        }
    }
}