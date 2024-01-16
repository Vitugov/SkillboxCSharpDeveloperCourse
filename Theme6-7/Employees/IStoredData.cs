using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class IStoredData
    {
        public static Dictionary<Type, Dictionary<int, IStoredData>> Data { get; set; }
        private static int indexer { get; set; }
        
        protected static int staticProperty;

        public int ID { get; set; }
        public DateTime DateAndTime { get; set; }
        public virtual int StaticProperty
        {
            get => staticProperty;
            set => staticProperty = value;
        }

        static IStoredData()
        {
            Data = new Dictionary<Type, Dictionary<int, IStoredData>>();
        }
        public IStoredData()
        {
            
        }

        public IStoredData(bool autoFill) : this()
        {
            if (autoFill == true)
            {
                indexer++;
                ID = indexer;
                DateAndTime = DateTime.Now;
                Initialize();
            }
        }

        public void Initialize()
        {
            if (!Data.ContainsKey(GetType()))
                Data.Add(GetType(), new Dictionary<int, IStoredData>());
            if (Data[GetType()].ContainsKey(ID))
                throw new ArgumentException("You are trying to add data that already are in System");
            Data[GetType()].Add(ID, this);
            indexer = Math.Max(ID, indexer);
        }

        public void AddData(List<IStoredData> DataToAdd)
        {

        }

        public static IStoredData Add()
        {
            return new IStoredData();
        }

        public static void Print(List<Worker> workers) { }


        public void DeleteData(int id)
        {
        
        }

        public virtual List<IStoredData> GetAllDataByType(Type type)
        {
            if (Data == null || !Data.ContainsKey(type.GetType()))
                throw new NullReferenceException();
            var result = Data[type.GetType()].Values.ToList();
            return result;
        }

        public virtual IStoredData GetDataByID(int id)
        {
            return new IStoredData();
        }

        public virtual List<IStoredData> GetDataBetweenTwoDates()
        { 
            return new List<IStoredData>();
        }
    }
}
