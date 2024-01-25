using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Task01.Model.Data;

namespace Task01.Model.Accsess
{
    public class DataAccessor
    {
        public ExpandoObject DynamicObject { get; set; }
        private object AssociatedObject { get; set; }
        public User User { get; set; }
        public Repository Repository { get; set; }

        public DataAccessor(object associatedObj, User user, Repository repository)
        {
            AssociatedObject = associatedObj;
            User = user;
            Repository = repository;
            DynamicObject = CreateExpandoObject(associatedObj, user);
            
        }

        private dynamic CreateExpandoObject(object associatedObj, User user)
        {
            dynamic dynamicObject = new ExpandoObject();
            var dictionary = dynamicObject as IDictionary<string, object>;
            var type = associatedObj.GetType();
            var rules = user.Role[type];
            var propertyList = type.GetProperties()
                .Select(property => property.Name)
                .Where(propertyName =>rules.ContainsKey(propertyName) && rules[propertyName].Read)
                .ToList();
            propertyList
                .ForEach(propertyName => { dictionary[propertyName] = type.GetProperty(propertyName).GetValue(associatedObj); });
            return dynamicObject;
        }

        public void UpdateAssociatedObject(User user, Repository repository)
        {
            var dictionary = DynamicObject as IDictionary<string, object>;
            var objType = AssociatedObject.GetType();
            var rules = User.Role[objType];
            foreach (var propertyName in dictionary.Keys)
            {
                if (rules.ContainsKey(propertyName) && rules[propertyName].Write)
                {
                    var propertyInfo = objType.GetProperty(propertyName);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(AssociatedObject, dictionary[propertyName]);
                    }
                }
            }
        }

        public static List<DataAccessor> GetListOfTypeForUser(User user, Type type, Repository repository)
        {
            var dynamicList = new List<DataAccessor>();
            var list = repository.GetDataOfType(type);
            foreach (var item in list)
            {
                dynamicList.Add(new DataAccessor(item, user, repository));
            }
            
            return dynamicList;
        }

        public void DeleteAssociatedObject(User user, Repository repository)
        {
            repository.Delete(AssociatedObject);
        }
    }
}
