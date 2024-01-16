using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class BaseClass
    {
        public static List<BaseClass> DataClassElements {  get; set; }
        public DataClass DataClass { get; set; }
        public string Number { get {  return "Hidden"; } }

        static BaseClass()
        {
            DataClassElements = new List<BaseClass>();
        }

        public BaseClass()
        {
            DataClass = new DataClass();
            DataClassElements.Add(this);
        }
    }
}
