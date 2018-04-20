using System.Collections.Generic;

namespace Core
{
    public class PointEqualityComparer : IEqualityComparer<Point>
    {
        private static PointEqualityComparer _instance;

        private PointEqualityComparer() { }

        public static PointEqualityComparer Instance
        {
            get
            {
                return _instance ?? (_instance = new PointEqualityComparer());
            }
        }

        public bool Equals(Point first, Point second)
        {
            if (first == null || second == null) return false;

            return first.X == second.X && first.Y == second.Y;
        }

        public int GetHashCode(Point point)
        {
            int hash = 13;
            hash = (hash * 7) + point.X.GetHashCode();
            hash = (hash * 7) + point.Y.GetHashCode();
            return hash;
        }
    }
}
