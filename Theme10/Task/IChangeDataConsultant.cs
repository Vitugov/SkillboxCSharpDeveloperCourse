using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal interface IChangeDataConsultant
    {
        internal void ChangeClientData(Client client, string telephoneNumber)
        {
            if (telephoneNumber != client.TelephoneNumber)
            {
                client.TelephoneNumber = telephoneNumber;
                new Edit(client, new List<string> { "TelephoneNumber" }, this.GetType().ToString(), "Edit");
            }
        }
    }
}
