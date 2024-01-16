using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            FillClients();
            InitializeComponent();
            SwitchView("Консультант");
            
        }

        public void SwitchView(string role)
        {
            if (role == "Менеджер")
                SetManagerView();

            if (role == "Консультант")
                SetConsultantView();
            ClientCard_Update();
        }

        public void SetConsultantView()
        {
            Passport.Binding = new Binding("PassportSeriesNumber");
            Telephone.Binding = new Binding("TelephoneNumber");
            ClientsTable.ItemsSource = Employee.ClientList;
            //ClientsTable.Items.Refresh();
        }

        public void SetManagerView()
        {
            Passport.Binding = new Binding("PassportSeriesNumberExtended");
            Telephone.Binding = new Binding("TelephoneNumberExtended");
            ClientsTable.ItemsSource = Manager.ClientList.Cast<Manager>();
            //ClientsTable.Items.Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SwitchView(ComboBox_GetValue());
        }

        private string ComboBox_GetValue()
        {
            if (RoleComboBox == null)
                throw new NullReferenceException();

            var selectedItem = ((ComboBoxItem)RoleComboBox.SelectedItem).Content;

            if (selectedItem == null)
                return "";

            var result = selectedItem.ToString();

            if (result == null)
                throw new NullReferenceException();

            return result;
        }

        public void FillClients()
        {
            new Manager(new Client("Скворцов", "Виктор", "Сергеевич", "+79139941587", "52 03 987456", "Manager"));
            new Manager(new Client("Игнатов", "Александр", "Александрович", "+79238976544", "28 25 879654", "Manager"));
        }

        private void ClientsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsTable.SelectedItems.Count != 1)
                return;

            var role = ComboBox_GetValue();
            
            if (role == "Менеджер")
            {
                var selectedPerson = ClientsTable.SelectedItem as Manager;
                ClientCard_UpdateFields(selectedPerson.Name, selectedPerson.Surname, selectedPerson.Patronymic,
                    selectedPerson.PassportSeriesNumberExtended, selectedPerson.TelephoneNumberExtended);
                ClientCard_SetAccess(true, true, true, true, true);
            };
            if (role == "Консультант")
            {
                var selectedPerson = ClientsTable.SelectedItem as Consultant;
                ClientCard_UpdateFields(selectedPerson.Name, selectedPerson.Surname, selectedPerson.Patronymic,
                    selectedPerson.PassportSeriesNumber, selectedPerson.TelephoneNumber);
                ClientCard_SetAccess(false, false, false, false, true);
            }

        }

        private void ClientCard_Update()
        {
            var tmp = ClientsTable.SelectedItem;
            ClientsTable.SelectedItem = null;
            ClientsTable.SelectedItem = tmp;
        }

        public void ClientCard_SetAccess(bool isNameEnabled, bool isSurnameEnabled, bool isPatronymicEnabled, bool isPassportEnabled, bool isTelephoneEnabled)
        {
            NameTextBox.IsEnabled = isNameEnabled;
            SurnameTextBox.IsEnabled = isSurnameEnabled;
            PatronymicTextBox.IsEnabled = isPatronymicEnabled;
            PassportTextBox.IsEnabled = isPassportEnabled;
            TelephoneTextBox.IsEnabled = isTelephoneEnabled;
        }

        public void ClientCard_UpdateFields(string name, string surname, string patronymic, string passport, string telephone)
        {
            NameTextBox.Text = name;
            SurnameTextBox.Text = surname;
            PatronymicTextBox.Text = patronymic;
            PassportTextBox.Text = passport;
            TelephoneTextBox.Text = telephone;
        }
    }
}