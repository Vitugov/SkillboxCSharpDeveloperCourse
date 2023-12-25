
namespace GameOfLife
{    
    public class Creature
    {
        public Creature NewNullClassAtThisPlace => new Ground(Simulation, GetPlace()) ;
        public CreatureProfile Profile { get; set; }
        private protected int Age { get; set; }
        public LifeSimulation Simulation { get; }
        public bool DiceToEat => CreatureProfile.Rnd(Profile.ChanceToEat);
        public bool DiceToEatAndMultiply => CreatureProfile.Rnd(Profile.ChanceToEatAndMultiply);
        public bool DiceToDieWithoutFood => CreatureProfile.Rnd(Profile.ChanceToDieWithoutFood);

        public Vector GetPlace() => Simulation.Data.Contains(this) ? Simulation.Data.GetVector(this) : new Vector(-1,-1);

        public Creature(LifeSimulation simulation, Vector place)
        {
            Profile = CreatureProfile.All[GetType()];
            Simulation = simulation;
            Age = 0;
            simulation.Born(this, place);
        }

        public virtual void Grow()
        {
            var neighborsOftype = Simulation.Data.GetNeighborsOfType(Profile.Victim, GetPlace());
            var neighborOfTypeCount = neighborsOftype.Count();
            var limitedNeighborOfTypeCount = Math.Min(neighborOfTypeCount, 2);
            if (neighborOfTypeCount > 0)
                Handle(neighborsOftype, limitedNeighborOfTypeCount);
            if (neighborOfTypeCount == 0 && DiceToDieWithoutFood)
                Simulation.Die(this); 
        }
        public override string ToString()
        {
            return Profile.Symbol.ToString() + GetPlace().ToString() + " Hash: " + this.GetHashCode();
        }

        private void Handle(List<Creature> list, int num)
        {
            var targets = list.OrderBy(x => (new Random()).Next()).Take(num).ToList();
            if (DiceToEat)
                Simulation.MoveAndEat(this, targets[0]);
            if (num == 2 && DiceToEatAndMultiply)
                Simulation.BornAndEat(GetType(), targets[1]);
        }


    }
}
