
using System.Reflection;

namespace GameOfLife
{
    public class GameOfLifeStruct
    {
        private Dictionary<Creature, Vector> CreaturesPlaces { get; }
        private int Height { get;}
        private int Width {  get;}
        private Creature[,] Cells { get; }
        private bool[,] IsProcessed { get; }
        private Type VoidType { get; }

        public GameOfLifeStruct(LifeSimulationStartParameters startParameters)
        {
            CreaturesPlaces = new Dictionary<Creature, Vector>();
            Height = startParameters.Height;
            Width = startParameters.Width;
            Cells = new Creature[Height, Width];
            IsProcessed = new bool[Height, Width];
            VoidType = typeof(Ground);
        }

        public Vector GetVector(Creature creature)
        {
            if (!CreaturesPlaces.ContainsKey(creature))
                throw new ArgumentException();
            return CreaturesPlaces[creature];
        }

        public bool Contains(Creature creature)
        {
            return CreaturesPlaces.ContainsKey(creature);
        }

        public Creature GetCreature(Vector vector)
        {
            if (Cells[vector.X, vector.Y] == null)
                throw new ArgumentException();
            return Cells[vector.X, vector.Y];
        }

        public void SetCreature(Creature creature, Vector vector)
        {
            Vector creatureVector = new Vector(-1, -1);
            if (creature == null) 
                throw new ArgumentNullException();
            IsProcessed[vector.X, vector.Y] = true;
            if (CreaturesPlaces.ContainsKey(creature))
            {
                creatureVector = CreaturesPlaces[creature];
                IsProcessed[creatureVector.X, creatureVector.Y] = true;
            }            
            var tmp = Cells[vector.X, vector.Y];
            if (tmp == creature)
                throw new ArgumentException();
            Cells[vector.X, vector.Y] = creature;
            if (creatureVector.X >= 0)
                Cells[creatureVector.X, creatureVector.Y] = null;
            CreaturesPlaces[creature] = vector;

            if (tmp != null)
                CreaturesPlaces.Remove(tmp);
        }

        public bool IsUsed(int x, int y) => IsProcessed[x, y];

        public void ReSet()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    IsProcessed[i, j] = false;   
        }

        public List<Creature> GetNeighborsOfType(Type type, Vector v)
        {
            var result = v.GetNearbyVectors()
                .Where(v => v.X >= 0 && v.Y >= 0 && v.X < Height && v.Y < Width)
                .Select(vector => Cells[vector.X, vector.Y])
                .Where(creature => creature.GetType() == type)
                .ToList();
            return result;
        }

        public void Cycle(LifeSimulation lifeSimulation, params Action<LifeSimulation, int, int>[] act )
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    act[0](lifeSimulation, i, j);
                if (act.Length > 1)
                    act[1](lifeSimulation, i, 0);
            }
        }

        private void Check()
        {
            if (CreaturesPlaces.Count < 24)
                throw new Exception();
        }
    }
}
