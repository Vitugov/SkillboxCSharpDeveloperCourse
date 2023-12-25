
namespace GameOfLife
{
    public class Vector
    {
        public int X { get; }
        public int Y { get; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Vector a, Vector b) { return a.Equals(b); }
        public static bool operator !=(Vector a, Vector b) { return !a.Equals(b); }

        public List<Vector> GetNearbyVectors()
        {
            var result = new List<Vector>();
            for (int i = X - 1; i < X + 2; i++)
                for (int j = Y - 1; j < Y + 2; j++)
                     result.Add(new Vector(i, j));
            return result;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Vector))
                return false;
            return ((Vector)obj).X == this.X && ((Vector)obj).Y == this.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }


    }
}
