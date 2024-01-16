using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class Edit
    {
        public static List<string> FullChanges {get; set;}
        public DateTime DateTime { get; set; }
        public List<string> ChangedData { get; set; }
        public string Type { get; set; }
        public string Author {  get; set; }

        static Edit()
        {
            FullChanges = new List<string>() {"Surname", "Name", "Patronimic", "TelephoneNumber", "PassportSeriesNumber"};
        }
        public Edit(Client client, List<string> changedData, string author, string type)
        {
            DateTime = DateTime.Now;
            ChangedData = changedData;
            Author = author;
            Type = type;
            client.Edits.Add(this);
        }

        public Edit(Client client, string author) : this(client, Edit.FullChanges, author, "new") { }
    }
}
