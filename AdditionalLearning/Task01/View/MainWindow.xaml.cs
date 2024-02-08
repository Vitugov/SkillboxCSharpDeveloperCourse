using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
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
using Task01.Infrastructure;
using Task01.Model.Accsess;
using Task01.Model.Data;
using Task01.ViewModel;

namespace Task01.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Session session)
        {
            DataContext = new ClientsViewModel(session);
            InitializeComponent();
        }

        private void SetDataGridHeaders(object sender, EventArgs e)
        {
            var grid = sender as DataGrid;
            foreach (var col in grid.Columns)
            {
                var property = col.Header.ToString();
                col.SortMemberPath = property;
                col.Header = (DataContext as ClientsViewModel).ColumnHeaders[property];
            }
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true; // Предотвращаем стандартную сортировку

            var direction = e.Column.SortDirection != ListSortDirection.Ascending
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            // Обновление направления сортировки для UI
            e.Column.SortDirection = direction;

            var propertyName = e.Column.SortMemberPath;
            DynamicSorter.Sort((DataContext as ClientsViewModel).SourceList, propertyName, direction);

            // Обновление UI DataGrid после сортировки
            dynamicDataGrid.Items.Refresh();
        }
    }
}