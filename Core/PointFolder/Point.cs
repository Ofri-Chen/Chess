using System;
using System.Collections.Generic;

namespace Core
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return PointEqualityComparer.Instance.Equals(this, obj as Point);
        }

        public override int GetHashCode()
        {
            return PointEqualityComparer.Instance.GetHashCode(this);
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}
