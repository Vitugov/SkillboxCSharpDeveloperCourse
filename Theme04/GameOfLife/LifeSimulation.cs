
namespace GameOfLife
{
    public class LifeSimulation
    {
        public LifeSimulationStartParameters StartParameters { get; set; }
        public int Height => StartParameters.Height;
        public int Width => StartParameters.Width;
        private Creature[,] Cells { get; set; }

        public bool[,] IsProcessed { get; set; }

        public LifeSimulation(LifeSimulationStartParameters startParameters)
        {
            StartParameters = startParameters;
            Cells = new Creature[Height, Width];
            IsProcessed = new bool[Height, Width];
            GenerateField();
        }

        public Creature this[Creature creature]
        {
            get => Cells[creature.Place.X, creature.Place.Y];
            set
            {
                Cells[creature.Place.X, creature.Place.Y] = value;
                IsProcessed[creature.Place.X, creature.Place.Y] = true;
            }
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
