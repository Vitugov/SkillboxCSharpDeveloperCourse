
namespace GameOfLife
{
    public class Ground : Creature
    {
        public Ground(LifeSimulation simulation, Vector place) : base(simulation, place) {}

        public override void Grow() { }
    }
}
