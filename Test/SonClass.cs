using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class SonClass : BaseClass
    {
        public new string Number { get { return DataClass.Number; } }
    }
}
