using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class Employee : INotifyCollectionChanged
    {
        public static ObservableCollection<Employee> ClientList {  get; set; } 
        internal Client Client {  get; }
        public string Name { get { return Client.Name; } set { ; } }
        public string Surname { get { return Client.Surname; } set { ; } }
        public string Patronymic { get { return Client.Patronymic; } set { ; } }

        public string TelephoneNumber
        {
            get { return Client.TelephoneNumber; }
            set { Client.TelephoneNumber = value != "" ? value : throw new NotImplementedException(); }
        }

        public string PassportSeriesNumber
        {
            get { return Client.PassportSeriesNumber != "" ? "XX ХХ ХХХ ХХХ" : ""; }
            set {; }
        }

        public string GetPassport => Client.PassportSeriesNumber != "" ? "XX ХХ ХХХ ХХХ" : "";

        static Employee()
        {
            ClientList = new ObservableCollection<Employee>();
        }
        
        public Employee(Client client)
        {
            Client = client;
            ClientList.Add(this);
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}
