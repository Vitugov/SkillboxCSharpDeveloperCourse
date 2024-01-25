using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Data;

namespace Task01.Model.Accsess
{
    public class Synchronizer
    {
        public User User { get; set; }
        public Repository Repository { get; set; }
        public Type Type { get; set; }
        private Dictionary<ExpandoObject, DataAccessor> CollectionToDataAccessorDic { get; set; }
        public DynamicItemCollection Collection {  get; set; }

        public Synchronizer(User user, Repository repository, Type type)
        {
            User = user;
            Repository = repository;
            Type = type;
            CollectionToDataAccessorDic = [];
            Collection = [];
            UpdateDataFromDataAccessor();
        }
        public void UpdateDataFromDataAccessor()
        {
            var list = DataAccessor.GetListOfTypeForUser(User, Type, Repository);
            CollectionToDataAccessorDic = [];
            foreach (var item in list)
            {
                CollectionToDataAccessorDic[item.DynamicObject] = item;
            }
            Collection = new DynamicItemCollection([.. CollectionToDataAccessorDic.Keys]);
        }

        public void Update(ExpandoObject obj)
        {
            if (!CollectionToDataAccessorDic.ContainsKey(obj))
                throw new NullReferenceException("Object to update havn't finded");
            CollectionToDataAccessorDic[obj].UpdateAssociatedObject(User, Repository);
        }

        public void Delete(ExpandoObject obj)
        {
            if (!CollectionToDataAccessorDic.ContainsKey(obj))
                throw new NullReferenceException("Object to update havn't finded");
            Collection.Remove(obj);
            CollectionToDataAccessorDic[obj].DeleteAssociatedObject(User, Repository);
            CollectionToDataAccessorDic.Remove(obj);
        }

        public ExpandoObject CreateNew()
        {
            var associatedObj = Activator.CreateInstance(Type);
            var newDAObj = new DataAccessor(associatedObj, User, Repository);
            CollectionToDataAccessorDic[newDAObj.DynamicObject] = newDAObj;
            Collection.Add(newDAObj.DynamicObject);
            return newDAObj.DynamicObject;
        }

        public HashSet<string> GetDynamicObjectAllProperties(ExpandoObject obj)
        {
            var dictionary = obj as IDictionary<string, object>;
            return [.. dictionary.Keys];
        }
    }
}
