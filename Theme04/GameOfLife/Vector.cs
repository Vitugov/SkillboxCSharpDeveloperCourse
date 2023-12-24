using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public List<Vector> GetNearbyVectors()
        {
            var result = new List<Vector>();
            for (int i = X - 1; i < X + 2; i++)
                for (int j = Y - 1; j < Y + 2; j++)
                     result.Add(new Vector(i, j));
            return result;
        }
    }
}
