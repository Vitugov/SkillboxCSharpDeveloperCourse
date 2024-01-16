using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.ViewModel
{
    public class ClientsViewModel
    {
        public ObservableCollection<ClientViewModel> Clients { get; }

        //public MainViewModel()
        //{
        //    Clients = new ObservableCollection<ClientViewModel>();
        //    // Здесь добавьте объекты XClassViewModel в коллекцию, используя разные роли
        //    // Пример:
        //    var adminXClass = new XClass { AdminProperty = "AdminValue", UserProperty = "UserValue" };
        //    var userXClass = new XClass { AdminProperty = "AdminValue", UserProperty = "UserValue" };

        //    XClassItems.Add(new XClassViewModel(adminXClass, "Admin"));
        //    XClassItems.Add(new XClassViewModel(userXClass, "User"));
        //}
    }
}
