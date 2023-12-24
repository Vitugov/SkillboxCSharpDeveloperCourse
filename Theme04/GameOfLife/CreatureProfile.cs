﻿
namespace GameOfLife
{
    public class CreatureProfile
    {
        public static Dictionary<Type, CreatureProfile> All { get; set; }
        public Type CreatureType { get; set; }
        public char Symbol { get; set; }
        public Type? Victim { get; set; }
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
}
