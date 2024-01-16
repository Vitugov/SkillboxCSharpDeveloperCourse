using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Task
{
    internal class JsonSerialization
    {
        internal void Serialize()
        { 
            var jsonText = JsonConvert.SerializeObject(Client.Clients);
            File.WriteAllText("Clients.json", jsonText);
            //jsonText = JsonConvert.SerializeObject(Edit.Edits);
            //File.WriteAllText("Edits.json", jsonText);
        }

        internal void Deserialize()
        {
            var jsonText = File.ReadAllText("Clients.json");
            if (jsonText != null)
            {
                var temp = JsonConvert.DeserializeObject<List<Client>>(jsonText);
                if (temp != null)
                    Client.Clients = temp;
            }
            
            //jsonText = File.ReadAllText("Edits.json");
            //if (jsonText != null)
            //{
            //    var temp = JsonConvert.DeserializeObject<List<Edit>>(jsonText);
            //    if (temp != null)
            //        Edit.Edits = temp;
            //}
        }
    }
}
