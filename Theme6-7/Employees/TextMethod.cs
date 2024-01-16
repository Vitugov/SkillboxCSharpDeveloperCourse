using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class TextMethod : IStorageMethod
    {
        public string Name { get; }
        public string Description { get; }

        public TextMethod(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void LoadFile(Type type, FileInfo file)
        {
            if (!file.Exists)
                throw new FileNotFoundException("File not found.", file.FullName);
            var lines = File.ReadAllLines(file.FullName);
            DataSerialization.Deserialize(type, lines);
        }

        public void ReWriteFile(List<IStoredData> storedData, FileInfo file)
        {
            var lines = DataSerialization.Serialize(storedData);
            File.WriteAllLines(file.FullName, lines);
        }

        public void CreateFile(Type type)
        {
            var catalog = GlobalParameters.Default.GetCatalogForSerialization();
            if (!Directory.Exists(catalog))
                Directory.CreateDirectory(catalog);
            var file = new FileInfo(GlobalParameters.Default.GetFullFileName(type));
            if (file.Exists)
                throw new IOException("File already exists.");
            File.Create(file.FullName).Close();
        }
    }
}
