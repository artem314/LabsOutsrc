using System.Windows;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SQLExecuteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
