using System.Windows;

namespace Test
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            new SonClass();
            new SonClass();
            
            InitializeComponent();
            var sourse = BaseClass.DataClassElements.OfType<BaseClass>().ToList();
            NumberTable.ItemsSource = sourse;
            var number = sourse[0].Number; // Выводит "Hidden"
        }
    }
}