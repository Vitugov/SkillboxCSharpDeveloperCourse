using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal interface IChangeDataManager
    {
        internal void ChangeClientData(Client client, string surname, string name, string patronymic, string telephoneNumber, string passportSeriesNumber)
        {
            var edits = new List<string>();

            if (surname != client.Surname)
                edits.Add("Surname");
            if (name != client.Name)
                edits.Add("Name");
            if (name != client.Name)
                edits.Add("Patronimic");
            if (telephoneNumber != client.TelephoneNumber)
                edits.Add("TelephoneNumber");
            if (passportSeriesNumber != client.PassportSeriesNumber)
                edits.Add("PassportSeriesNumber");

            client.Surname = surname;
            client.Name = name;
            client.Patronymic = patronymic;
            client.TelephoneNumber = telephoneNumber;
            client.PassportSeriesNumber = passportSeriesNumber;

            new Edit(client, edits, this.GetType().ToString(), "Edit");
        }

        internal Client CreateNewClient(string surname, string name, string patronymic, string telephoneNumber, string passportSeriesNumber)
        {
            var edits = new List<string> { "Surname", "Name", "Patronimic", "TelephoneNumber", "PassportSeriesNumber" };
            var client = new Client(surname, name, patronymic, telephoneNumber, passportSeriesNumber, this.GetType().ToString());
            new Edit(client, edits, this.GetType().ToString(), "New");
            return client;
        }
    }
}
