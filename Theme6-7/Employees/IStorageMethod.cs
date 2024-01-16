using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public interface IStorageMethod
    {
        public string Name { get; }
        public string Description { get; }

        public void LoadFiles(Type[] types, FileInfo file, IStorageMethod method)
        {
            foreach (var type in types)
            {
                object instance = Activator.CreateInstance(type);
                method.LoadFile(type, file);
            }
        }
        public void ReWriteFiles(Dictionary<Type, Dictionary<int, IStoredData>> storedData, FileInfo file, IStorageMethod method)
        {
            foreach (var dic in storedData)
                method.ReWriteFile(dic.Value.Values.ToList(), file);
        }
        public  void CreateFiles()
        {
            foreach (var type in GlobalParameters.RegisteredTypes)
                CreateFile(type);
        }

        public abstract void LoadFile(Type type, FileInfo file);

        public abstract void ReWriteFile(List<IStoredData> storedData, FileInfo file);

        public abstract void CreateFile(Type type);
    }
}
