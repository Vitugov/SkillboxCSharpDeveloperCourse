using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class CreatureProfile
    {
        public static Dictionary<Type, CreatureProfile> All { get; set; }
        public Type CreatureType {  get; set; }
        public char Symbol {  get; set; }
        public Type? Victim {  get; set; }
        private static Random RND { get; }
        public int MaxAge { get; set; }
        public ConsoleColor Color { get; set; }

        public readonly double ChanceToEat;
        public readonly double ChanceToEatAndMultiply;
        public readonly double ChanceToDieWithoutFood;

        static CreatureProfile()
        {
            All = new Dictionary<Type, CreatureProfile>();
            RND = new Random();
            All[typeof(Wolf)] = new CreatureProfile(typeof(Wolf), 'W', typeof(Sheep), 20, ConsoleColor.Red, 0.2, 0.3, 1);
            All[typeof(Sheep)] = new CreatureProfile(typeof(Sheep), 'S', typeof(Grass), 20, ConsoleColor.Gray, 0.2, 0.2, 1);
            All[typeof(Grass)] = new CreatureProfile(typeof(Grass), 'G', typeof(Ground), 20, ConsoleColor.Green, 1, 1, 1);
            All[typeof(Ground)] = new CreatureProfile(typeof(Ground), '0', null, 0, ConsoleColor.Black, 0, 0, 0);
        }

        public CreatureProfile(Type creatureType, char symbol, Type victim, int maxAge, ConsoleColor color,
            double chanceToEat, double chanceToEatAndMultiply, double chanceToDieWhithoutFood)
        {
            CreatureType = creatureType;
            Symbol = symbol;
            Victim = victim;
            MaxAge = maxAge;
            Color = color;
            ChanceToEat = chanceToEat;
            ChanceToEatAndMultiply = chanceToEatAndMultiply;
            ChanceToDieWithoutFood = chanceToDieWhithoutFood;
            All[creatureType] = this;
        }

        public static bool Rnd(double chance)
        {
            return RND.Next((int)Math.Round(1 / chance)) == 0;
        }
    }
    
    public class Creature
    {
        private protected Creature NewNullClassAtThisPlace => new Ground(Simulation, Place) ;
        public CreatureProfile Profile { get; set; }
        private protected int Age { get; set; }
        public LifeSimulation Simulation { get; }
        public bool DiceToEat => CreatureProfile.Rnd(Profile.ChanceToEat);
        public bool DiceToEatAndMultiply => CreatureProfile.Rnd(Profile.ChanceToEatAndMultiply);
        public bool DiceToDieWithoutFood => CreatureProfile.Rnd(Profile.ChanceToDieWithoutFood);

        private protected Vector place;
        public Vector Place
        {
            get => place;
            set
            {
                place = value;
                Simulation[this] = this;
            }
        }

        static Creature() {}
        public Creature(LifeSimulation simulation, Vector place)
        {
            Profile = CreatureProfile.All[GetType()];
            Simulation = simulation;
            Age = 0;
            Place = place;
            Simulation[this] = this;
        }

        public virtual void Grow()
        {
            var neighborsOftype = Simulation.GetNeighborsOfType(Profile.Victim, Place);
            var neighborOfTypeCount = neighborsOftype.Count();
            if (neighborOfTypeCount > 0)
                Handle(neighborsOftype, Math.Min(neighborOfTypeCount, 2));
            if (neighborOfTypeCount == 0 && DiceToDieWithoutFood)
                Simulation[this] = NewNullClassAtThisPlace;
        }

        public void Handle(List<Creature> list, int num)
        {
            var targets = list.Randomize().Take(num).ToList();
            if (DiceToEat)
            {
                Simulation[this] = NewNullClassAtThisPlace;
                Place = Simulation[targets[0]].Place;
            }
            if (num == 2 && DiceToEatAndMultiply)
                Activator.CreateInstance(GetType(), Simulation, Simulation[targets[1]].Place);
                //new Wolf(Simulation, Simulation[targets[1]].Place);
        }

        public override string ToString()
        {
            return Profile.Symbol.ToString();
        }
    }
}
