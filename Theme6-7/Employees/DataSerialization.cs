using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public static class DataSerialization
    {
        public static List<string> Serialize(List<IStoredData> data)
        {
            Type dataType = data.GetType();
            PropertyInfo[] properties = dataType.GetProperties();
            List<string> serializedData = new List<string>();
            
            foreach (var item in data)
            {
                List<string> propertyValues = new List<string>();

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(data);
                    string serializedValue = value.ToString();
                    propertyValues.Add(serializedValue);
                }

                serializedData.Add(string.Join("#", propertyValues));
            }
            return serializedData;
        }

        public static void Deserialize(Type type, string[] serializedData)
        {
            PropertyInfo[] properties = type.GetProperties();

            foreach (var item in serializedData)
            {
                if (type == null || !typeof(IStoredData).IsAssignableFrom(type) || type.GetConstructor(Type.EmptyTypes) == null)
                    throw new ArgumentException($"Type {type.Name} must be derived from BaseType.");
                var instance = (IStoredData)Activator.CreateInstance(type);
                
                string[] propertyValues = item.Split('#');

                if (propertyValues.Length != properties.Length)
                    throw new ArgumentException("Invalid serialized data.");

                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = properties[i];
                    Type propertyType = property.PropertyType;

                    string serializedValue = propertyValues[i];
                    object deserializedValue = Convert.ChangeType(serializedValue, propertyType);

                    property.SetValue(instance, deserializedValue);
                }
                instance.Initialize();
            }
        }
    }
}