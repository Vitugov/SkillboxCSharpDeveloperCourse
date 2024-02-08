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
                col.Header = (DataContext as ClientsViewModel).ColumnHeaders[col.Header.ToString()];
        }
    }
}