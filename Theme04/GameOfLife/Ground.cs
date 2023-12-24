using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Ground : Creature
    {
        public Ground(LifeSimulation simulation, Vector place) : base(simulation, place) {}

        public override void Grow() { }
    }
}
