
namespace GameOfLife
{    
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
            var targets = list.OrderBy(x => (new Random()).Next()).Take(num).ToList();
            if (DiceToEat)
            {
                Simulation[this] = NewNullClassAtThisPlace;
                Place = Simulation[targets[0]].Place;
            }
            if (num == 2 && DiceToEatAndMultiply)
                Activator.CreateInstance(GetType(), Simulation, Simulation[targets[1]].Place);
        }

        public override string ToString()
        {
            return Profile.Symbol.ToString();
        }
    }
}
