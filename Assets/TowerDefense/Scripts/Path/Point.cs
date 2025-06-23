using System;
using UnityEngine;

namespace TowerDefense.Scripts.Path
{
    public class Point : IEquatable<Point>
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Point other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj) => obj is Point other && Equals(other);

        public override int GetHashCode() => (x, y).GetHashCode();

        public Vector2 ToVector2() => new Vector2(x, y);

        public static implicit operator Vector2(Point p) => p.ToVector2();

        public override string ToString() => $"({x}, {y})";
    }
}