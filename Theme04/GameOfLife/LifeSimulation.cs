
using System.Numerics;

namespace GameOfLife
{
    public class LifeSimulation
    {
        public Dictionary<Creature, Vector> CreaturesPlaces { get; set; }
        public LifeSimulationStartParameters StartParameters { get; set; }
        public int Height => StartParameters.Height;
        public int Width => StartParameters.Width;
        public Creature[,] Cells { get; set; }
        public bool[,] IsProcessed { get; set; }

        public LifeSimulation(LifeSimulationStartParameters startParameters)
        {
            StartParameters = startParameters;
            CreaturesPlaces = new Dictionary<Creature, Vector>();
            Cells = new Creature[Height, Width];
            IsProcessed = new bool[Height, Width];
            GenerateField();
        }

        private void SetValue(Vector vector, Creature creature)
        {

        }

        public Creature this[Creature creature]
        {
            get
            {
                if (!CreaturesPlaces.ContainsKey(creature))
                    throw new ArgumentException();
                var vector = CreaturesPlaces[creature];
                return Cells[vector.X, vector.Y];
            }
        }

        public Creature this[Vector vector]
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                var temp = Cells[vector.X, vector.Y];
                if (temp == value)
                    throw new NullReferenceException();
                Cells[vector.X, vector.Y] = value;
                IsProcessed[vector.X, vector.Y] = true;
                if (temp != null && CreaturesPlaces.ContainsKey(temp) && CreaturesPlaces[temp].X == vector.X && CreaturesPlaces[temp].Y == vector.Y)
                    CreaturesPlaces.Remove(temp);
                CreaturesPlaces[value] = vector;
            }
        }

        public void MoveAndEat(Creature current, Creature victim)
        {
         //   this[victim.] = current;
        }

        public void BornAndEat()
        {

        }

        public void Die(Creature creature)
        {
            var vector = CreaturesPlaces[creature];
            if (creature != null && CreaturesPlaces.ContainsKey(creature))
                CreaturesPlaces.Remove(creature);
            Cells[vector.X, vector.Y] = creature.NewNullClassAtThisPlace;

        }

        public void Born()
        {

        }

        public void DrawAndGrow()
        {
            DrawGame();
            Grow();
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
        
        private ConsoleColor SetlColour(int i, int j) => Console.ForegroundColor = Cells[i, j].Profile.Color;

        private void Grow()
        {
            Action<LifeSimulation, int, int> Evaluate = (sim, i, j) =>
            {
                if (!sim.IsProcessed[i, j])
                    sim.Cells[i, j].Grow();
            };

            Action<LifeSimulation, int, int> ReSet = (sim, i, j) =>
                sim.IsProcessed[i, j] = false;

            Cycle(ReSet);
            Cycle(Evaluate);
        }



        private void DrawGame()
        {
            var defaultColor = Console.ForegroundColor;
            Action<LifeSimulation, int, int> Print = (sim, i, j) =>
            {
                Console.ForegroundColor = sim.Cells[i, j].Profile.Color;
                Console.Write(sim.Cells[i, j].Profile.Symbol);
                if (j == sim.Width - 1)
                    Console.WriteLine("\r");
            };

            Cycle(Print);
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        private void GenerateField()
        {
            Action<LifeSimulation, int, int> Generate = (sim, i, j) =>
            Activator.CreateInstance(sim.StartParameters.GetDice(), sim, new Vector(i, j));
            Cycle(Generate);
        }

        private void Cycle(Action<LifeSimulation, int, int> act)
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    act(this, i, j);
        }
    }
}
