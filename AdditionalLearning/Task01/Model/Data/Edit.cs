using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Data
{
    public class Edit
    {
        public static List<string> FullChanges { get; set; }
        public DateTime DateTime { get; set; }
        public List<string> ChangedData { get; set; }
        public string TypeOfChanges { get; set; }
        public string Author { get; set; }

        static Edit()
        {
            FullChanges = new List<string>() { "Surname", "Name", "Patronimic", "TelephoneNumber", "PassportSeriesNumber" };
        }
        public Edit(Client client, List<string> changedData, string author, string typeOfChanges)
        {
            DateTime = DateTime.Now;
            ChangedData = changedData;
            Author = author;
            TypeOfChanges = typeOfChanges;
            client.Edits.Add(this);
        }

        public Edit(Client client, string author) : this(client, FullChanges, author, "new") { }
    }
}
