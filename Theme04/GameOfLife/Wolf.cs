using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Wolf : Creature
    {
        public Wolf(LifeSimulation simulation, Vector place) : base(simulation, place) { }

    }
}
