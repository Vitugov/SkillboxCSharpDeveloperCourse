
namespace GameOfLife
{
    public class Game
    {
        public GameManager Data { get; }
        public StartParameters StartParameters { get; set; }

        public Game(StartParameters startParameters)
        {
            StartParameters = startParameters;
            Data = new GameManager(StartParameters);
            GenerateField();
        }

        public void EatAndMove(Creature current, Creature victim)
        {
            var currentPlace = Data.GetVector(current);
            var victimPlace = Data.GetVector(victim);
            Data.Remove(victim);
            Data.Move(current, victimPlace);
            Data.Add(new Ground(this), currentPlace);
            Data.Check();
        }

        public void EatAndGiveBirth(Type type, Creature victim)
        {
            var victimVector = Data.GetVector(victim);
            Data.Remove(victim);
            var NewCreature = Activator.CreateInstance(type, this);
            if (NewCreature == null || NewCreature is not Creature)
                throw new InvalidCastException();
            Data.Add((Creature)NewCreature, victimVector);
            Data.Check();
        }

        public void DieFromStarvation(Creature creature)
        {
            var vector = Data.GetVector(creature);
            Data.Remove(creature);
            Data.Add(new Ground(this), vector);
            Data.Check();
        }

        public void DrawAndGrow()
        {
            DrawGame();
            Grow();
        }

        private void Grow()
        {
            Action<Game, int, int> Evaluate = (sim, i, j) =>
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
            Action<Game, int, int> Print1 = (sim, i, j) =>
            {
                var profile = sim.Data.GetCreature(new Vector(i, j)).Profile;
                Console.ForegroundColor = profile.Color;
                Console.Write(profile.Symbol);
            };

            Action<Game, int, int> Print2 = (sim, i, j) => Console.WriteLine("\r");

            Data.Cycle(this, Print1, Print2);
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        private void GenerateField()
        {
            Action<Game, int, int> Generate = (GOLStruct, i, j) =>
            {
                var vector = new Vector(i, j);
                var creature = Activator.CreateInstance(GOLStruct.StartParameters.GetDice(), GOLStruct);
                if (creature == null || creature is not Creature)
                    throw new InvalidCastException();
                Data.Add((Creature)creature, vector);
            };
            Data.Cycle(this, Generate);
        }
    }
}
