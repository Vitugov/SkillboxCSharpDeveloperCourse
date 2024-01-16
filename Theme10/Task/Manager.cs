using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class Manager : Consultant, IChangeDataManager
    {
        public new string Name { get { return Client.Name; } set { Client.Name = value; } }
        public new string Surname { get { return Client.Surname; } set { Client.Surname = value; } }
        public new string Patronymic { get { return Client.Patronymic; } set { Client.Patronymic = value; } }
        public string PassportSeriesNumberExtended { get { return Client.PassportSeriesNumber; } set { Client.PassportSeriesNumber = value; } }
        public string TelephoneNumberExtended { get { return Client.TelephoneNumber; } set { Client.TelephoneNumber = value; } }

        public Manager(Client client) : base(client) {}
    }
}
