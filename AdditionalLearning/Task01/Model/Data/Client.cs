using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Data
{
    public class Client
    {
        public List<Edit> Edits { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string TelephoneNumber { get; set; }
        public string PassportSeriesNumber { get; set; }

        public Client(string surname, string name, string patronymic, string telephoneNumber, string passportSeriesNumber, string author)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            TelephoneNumber = telephoneNumber;
            PassportSeriesNumber = passportSeriesNumber;
            Edits = new List<Edit>();
            new Edit(this, author);
            Repository.CurrentRepository.Add(this);
        }

        public Client()
        {
            Surname = "";
            Name = "";
            Patronymic = "";
            TelephoneNumber = "";
            PassportSeriesNumber = "";
            Edits = new List<Edit>();
            Repository.CurrentRepository.Add(this);
        }
    }
}
