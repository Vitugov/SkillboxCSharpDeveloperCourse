using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Task01.Model.Accsess
{
    public class DataAccessor
    {
        public Object AssociatedObject { get; set; }
        public ExpandoObject DynamicObject { get; set; }
        public Role Role { get; set; }

        public DataAccessor(Object associatedObject, Role role)
        {
            
        }

        public void CreateExpandoObject()
        {
            DynamicObject = new ExpandoObject();
            var dictionary = DynamicObject as IDictionary<string, object>;
            //ToDo
            AssociatedObject.GetType().GetProperties();
        }
        public void AddProperty(string propertyName, object value)
        {
            var dictionary = DynamicObject as IDictionary<string, object>;
            if (dictionary != null)
            {
                if (!dictionary.ContainsKey(propertyName))
                {
                    dictionary.Add(propertyName, value);
                }
                else
                {
                    dictionary[propertyName] = value;
                }
            }
        }

        public object GetPropertyValue(string propertyName)
        {
            var dictionary = DynamicObject as IDictionary<string, object>;
            return dictionary != null && dictionary.ContainsKey(propertyName) ? dictionary[propertyName] : throw new NullReferenceException();
        }


    }
}
