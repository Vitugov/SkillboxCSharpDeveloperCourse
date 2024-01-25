using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Model.Data
{
    public class Repository
    {
        public static Repository CurrentRepository { get; set; }
        private static Dictionary<Type, PropertyInfo> dataByType {  get; set; }
        public HashSet<object> Clients { get; set; }
        static Repository()
        {
            CurrentRepository = new Repository();
            dataByType = new Dictionary<Type, PropertyInfo>();
            dataByType[typeof(Client)] = typeof(Repository).GetProperty("Clients");
        }

        public Repository()
        {
            Clients = new HashSet<object>();
        }

        public void Add(object obj)
        {
            Type objectType = obj.GetType();

            if (!dataByType.ContainsKey(objectType))
            {
                throw new ArgumentException($"Type {objectType} is not supported in this repository.");
            }
            
            AddToSpecificList(obj, dataByType[objectType]);
        }

        public void Delete(object obj)
        {
            Type objectType = obj.GetType();
            if (!dataByType.ContainsKey(objectType))
            {
                throw new ArgumentException($"Type {objectType} is not supported in this repository.");
            }
            var data = (HashSet<object>)dataByType[objectType].GetValue(this);
            data.Remove(obj);
        }

        public HashSet<object> GetDataOfType(Type type)
        {
            var list = (HashSet<object>)dataByType[type].GetValue(this);
            return list;
        }

        private void AddToSpecificList(object obj, PropertyInfo property)
        {
            var hashset = (HashSet<object>)property.GetValue(this);

            if (hashset == null)
            {
                throw new InvalidOperationException($"Property {property.Name} is not a valid list in this repository.");
            }

            hashset.Add(obj);
        }
    }

}
