
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

        public Vector GetPlace()
        {
            if (!Simulation.CreaturesPlaces.ContainsKey(this))
                 throw new NullReferenceException();
            return Simulation.CreaturesPlaces[this];
        }
        public Creature(LifeSimulation simulation, Vector place)
        {
            Profile = CreatureProfile.All[GetType()];
            Simulation = simulation;
            Age = 0;
            Simulation[place] = this;
        }

        public virtual void Grow()
        {
            
            var neighborsOftype = Simulation.GetNeighborsOfType(Profile.Victim, GetPlace());
            var neighborOfTypeCount = neighborsOftype.Count();
            if (neighborOfTypeCount > 0)
                Handle(neighborsOftype, Math.Min(neighborOfTypeCount, 2));
            if (neighborOfTypeCount == 0 && DiceToDieWithoutFood)
            {
                var place = GetPlace();
                var newGround = new Ground(Simulation, place);
            }    
            
        }

        public void Handle(List<Creature> list, int num)
        {
            var targets = list.OrderBy(x => (new Random()).Next()).Take(num).ToList();
            if (DiceToEat)
            {
                var tempPlace = this.GetPlace();
                Simulation[targets[0].GetPlace()] = this;
                new Ground(Simulation, tempPlace);
            }
            if (num == 2 && DiceToEatAndMultiply)
                Activator.CreateInstance(GetType(), Simulation, Simulation[targets[1]].GetPlace());
        }

        public override string ToString()
        {
            return Profile.Symbol.ToString() + " " + GetPlace().ToString();
        }
    }
}
