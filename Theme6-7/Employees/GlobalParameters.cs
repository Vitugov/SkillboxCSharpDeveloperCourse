using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class GlobalParameters
    {
        public static Dictionary<IStorageMethod, GlobalParameters> Parameters { get; set; }
        public static GlobalParameters Default { get; set; }
        public static List<Type> RegisteredTypes { get; set; }
        public IStorageMethod StorageMethod { get; set; }
        private string WorkCatalog {  get; set; }
        private string FileExtension { get; set; }
        

        static GlobalParameters()
        {
            RegisteredTypes = new List<Type>() { typeof(Worker), typeof(Payroll), typeof(WagePayment) };
            Default = new GlobalParameters(new TextMethod("FileReadWriteMethods", ""), "txt");
            Parameters = new Dictionary<IStorageMethod, GlobalParameters>();
            Parameters.Add(Default.StorageMethod, Default);
        }
        public GlobalParameters(IStorageMethod storageMethod, string fileExtension)
        { 
            StorageMethod = storageMethod;
            FileExtension = fileExtension;
            WorkCatalog = StorageMethod.GetType().Name;
        }
        
        public string GetCatalogForSerialization() => Directory.GetCurrentDirectory() + @"\" + WorkCatalog;

        public string GetFullFileName(Type type) => GetCatalogForSerialization() + @"\" + type.Name + "." + FileExtension;
        
        public List<string> GetFullFileNames() => RegisteredTypes.Select(type => GetFullFileName(type)).ToList();

    }
}
