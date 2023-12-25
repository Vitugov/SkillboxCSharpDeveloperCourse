
namespace GameOfLife
{
    public class LifeSimulation
    {
        public GameOfLifeStruct Data { get; }
        public LifeSimulationStartParameters StartParameters { get; set; }

        public LifeSimulation(LifeSimulationStartParameters startParameters)
        {
            StartParameters = startParameters;
            Data = new GameOfLifeStruct(StartParameters);
            GenerateField();
        }

        public void MoveAndEat(Creature current, Creature victim)
        {
            var currentPlace = Data.GetVector(current);
            var victimPlace = Data.GetVector(victim);
            Data.SetCreature(current, victimPlace);
            new Ground(this, currentPlace);
        }

        public void BornAndEat(Type type, Creature victim)
        {
            var victimVector = Data.GetVector(victim);
            Activator.CreateInstance(type, this, victimVector);
        }

        public void Die(Creature creature)
        {
            var place = Data.GetVector(creature);
            var newGround = new Ground(this, place);
        }

        public void Born(Creature creature, Vector vector)
        {
            Data.SetCreature(creature, vector);
        }

        public void DrawAndGrow()
        {
            DrawGame();
            Grow();
        }

        private void Grow()
        {
            Action<LifeSimulation, int, int> Evaluate = (sim, i, j) =>
            {
                if (!Data.IsUsed(i, j))
                    Data.GetCreature(new Vector(i, j)).Grow();
            };

            Data.ReSet();
            Data.Cycle(this, Evaluate);
        }

        private void DrawGame()
        {
            var defaultColor = Console.ForegroundColor;
            Action<LifeSimulation, int, int> Print1 = (sim, i, j) =>
            {
                var profile = sim.Data.GetCreature(new Vector(i, j)).Profile;
                Console.ForegroundColor = profile.Color;
                Console.Write(profile.Symbol);
            };

            Action<LifeSimulation, int, int> Print2 = (sim, i, j) => Console.WriteLine("\r");

            Data.Cycle(this, Print1, Print2);
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        private void GenerateField()
        {
            Action<LifeSimulation, int, int> Generate = (GOLStruct, i, j) =>
                Activator.CreateInstance(GOLStruct.StartParameters.GetDice(), GOLStruct, new Vector(i, j));
            Data.Cycle(this, Generate);
        }
    }
}
