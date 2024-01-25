using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Accsess;
using Task01.Model.Data;
using Task01.Infrastructure;
using System.Windows.Input;
using Task01.Commands.Base;
using System.Windows;

namespace Task01.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        public Synchronizer Synchronizer { get; }

        private ExpandoObject _OriginalItem;
        public ExpandoObject OriginalItem
        {
            get => _OriginalItem;
            set => Set(ref _OriginalItem, value);
        }

        private ExpandoObject _EditableItem;
        public ExpandoObject EditableItem
        {
            get => _EditableItem;
            set => Set(ref _EditableItem, value);
        }

        public Dictionary<string, bool> IsReadOnlyDic { get; set; }
        public ClientViewModel(Synchronizer synchronizer, ExpandoObject obj)
        {
            Synchronizer = synchronizer;
            OriginalItem = obj;
            EditableItem = obj.Clone();
            InitializeAccessDic(Synchronizer);
            Cancel = new RelayCommand(win => (win as Window).Close());
            Save = new RelayCommand(SaveEdits);
        }

        public void InitializeAccessDic(Synchronizer synchronizer)
        {
            var dic = synchronizer.User.Role.AccessRules[synchronizer.Type];
            IsReadOnlyDic = [];
            foreach(var prop in dic)
            {
                if (prop.Value.Read == true)
                    IsReadOnlyDic[prop.Key] = !prop.Value.Write;
            }
        }

        public ICommand Cancel { get; }
        public ICommand Save { get; }

        public void SaveEdits(object window)
        {
            var source = EditableItem as IDictionary<string, object>;
            var toUpdate = OriginalItem as IDictionary<string, object>;
            foreach (var prop in source)
            {
                toUpdate[prop.Key] = prop.Value;
            }
            Synchronizer.Update(OriginalItem);
            var win = window as Window;
            win.Close();
        }
    }
}
