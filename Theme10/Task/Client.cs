using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task
{
    public class Client
    {
        public static List<Client> Clients {  get; set; }

        public List<Edit> Edits { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string TelephoneNumber { get; set; }
        public string PassportSeriesNumber { get; set; }

        static Client()
        {
            Clients = new List<Client>();
            
        }
        public Client(string surname, string name, string patronymic, string telephoneNumber, string passportSeriesNumber, string author)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            TelephoneNumber = telephoneNumber;
            PassportSeriesNumber = passportSeriesNumber;
            Edits = new List<Edit>();
            new Edit(this, author);
            Clients.Add(this);
        }

        public Client()
        {
            Surname = "";
            Name = "";
            Patronymic = "";
            TelephoneNumber = "";
            PassportSeriesNumber = "";
            Edits = new List<Edit>();
        }
    }
}
