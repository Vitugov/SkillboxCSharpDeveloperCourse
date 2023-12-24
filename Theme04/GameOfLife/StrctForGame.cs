using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class StrctForGame
    {
        public Dictionary<Creature, Vector> CreaturesPlaces { get; set; }
        public int Height { get;}
        public int Width {  get;}
        public Creature[,] Cells { get; set; }
        public bool[,] IsProcessed { get; set; }

        public StrctForGame(int height, int width)
        {
            CreaturesPlaces = new Dictionary<Creature, Vector>();
            Height = height;
            Width = width;
            Cells = new Creature[Height, Width];
            IsProcessed = new bool[Height, Width];
        }

        public Vector GetVector(Creature creature)
        {
            if (!CreaturesPlaces.ContainsKey(creature))
                throw new ArgumentException();
            return CreaturesPlaces[creature];
        }

        public Creature GetCreature(Vector vector)
        {
            if (Cells[vector.X, vector.Y] == null)
                throw new ArgumentException();
            return Cells[vector.X, vector.Y];
        }

        public void SetCreature(Creature creature, Vector vector)
        {
            if (creature == null) 
                throw new ArgumentNullException();
            var tmp = Cells[vector.X, vector.Y];
            if (tmp == creature)
                throw new ArgumentException();
            if (tmp != null)
                CreaturesPlaces.Remove(tmp);
            Cells[vector.X, vector.Y] = creature;
            CreaturesPlaces[creature] = vector;
            IsProcessed[vector.X, vector.Y] = true;
        }


    }
}
