
namespace GameOfLife
{    
    public class Creature
    {
        public CreatureProfile Profile { get; }
        private protected int Age { get; set; }
        public Game Simulation { get; }

        public Creature(Game simulation)
        {
            Profile = CreatureProfile.All[GetType()];
            Simulation = simulation;
            Age = 0;
        }
        private bool diceToEat => CreatureProfile.Rnd(Profile.ChanceToEat);
        private bool diceToEatAndMultiply => CreatureProfile.Rnd(Profile.ChanceToEatAndMultiply);
        private bool diceToDieWithoutFood => CreatureProfile.Rnd(Profile.ChanceToDieWithoutFood);
        
        public virtual void Grow()
        {
            var neighborsOftype = Simulation.Data.GetNeighborsOfType(Profile.Victim, this);
            var neighborOfTypeCount = neighborsOftype.Count();
            var limitedNeighborOfTypeCount = Math.Min(neighborOfTypeCount, 2);

            if (neighborOfTypeCount == 0 && diceToDieWithoutFood)
                Simulation.DieFromStarvation(this);
            if (neighborOfTypeCount > 0)
                Handle(neighborsOftype, limitedNeighborOfTypeCount);
        }
        private void Handle(List<Creature> list, int num)
        {
            var targets = list.OrderBy(x => (new Random()).Next()).Take(num).ToList();
            if (diceToEat)
                Simulation.EatAndMove(this, targets[0]);
            if (num == 2 && diceToEatAndMultiply)
                Simulation.EatAndGiveBirth(GetType(), targets[1]);
        }

        public override string ToString()
        {
            var place = Simulation.Data.Contains(this) ? Simulation.Data.GetVector(this).ToString() : "(0)";
            return Profile.Symbol.ToString() + place + " Hash: " + this.GetHashCode();
        }

    }
}
