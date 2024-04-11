using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializationTest
{
    public class SomeClass
    {
        public string SomeProperty { get; set; }

        public SomeClass(string someProperty)
        { this.SomeProperty = someProperty; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not SomeClass) return false;
            return SomeProperty == (obj as SomeClass).SomeProperty;
        }

        public override int GetHashCode()
        {
            return SomeProperty.GetHashCode();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
