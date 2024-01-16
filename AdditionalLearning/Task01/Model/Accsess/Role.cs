using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Data;

namespace Task01.Model.Accsess
{
    public class Role
    {
        public string Name { get; set; }
        public Dictionary<Type, Dictionary<string, Permission>> AccessRules { get; set; }

        public Role(string name)
        {
            Name = name;
            AccessRules = new Dictionary<Type, Dictionary<string, Permission>>();
        }
    }
}
