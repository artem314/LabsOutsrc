using System.Windows;
using System.Windows.Controls;

namespace Lab3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }

        private void SQL1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string selectedItem = cb.SelectedItem.ToString();
            if (selectedItem == "System.Windows.Controls.ListBoxItem: INSERT INTO")
            {
                SQLUPD1.Visibility = Visibility.Hidden;
                SQLUPD2.Visibility = Visibility.Hidden;
                SQL2.Visibility = Visibility.Hidden;
                SQL3.Visibility = Visibility.Hidden;

                SQL6.Visibility = Visibility.Hidden;
                SQL7.Visibility = Visibility.Hidden;
            }
            if (selectedItem == "System.Windows.Controls.ListBoxItem: SELECT")
            {
                SQLUPD1.Visibility = Visibility.Hidden;
                SQLUPD2.Visibility = Visibility.Hidden;
                SQL2.Visibility = Visibility.Visible;
                SQL3.Visibility = Visibility.Visible;
                SQL6.Visibility = Visibility.Visible;
                SQL7.Visibility = Visibility.Visible;

                SQL8.Text = "1";
            }
            if (selectedItem == "System.Windows.Controls.ListBoxItem: DELETE")
            {
                SQLUPD1.Visibility = Visibility.Hidden;
                SQLUPD2.Visibility = Visibility.Hidden;
                SQL2.Visibility = Visibility.Hidden;
                SQL3.Visibility = Visibility.Visible;
                SQL6.Visibility = Visibility.Visible;
                SQL7.Visibility = Visibility.Visible;
            }
            if (selectedItem == "System.Windows.Controls.ListBoxItem: UPDATE")
            {
                SQLUPD1.Visibility = Visibility.Visible;
                SQLUPD2.Visibility = Visibility.Visible;
                SQL2.Visibility = Visibility.Hidden;
                SQL3.Visibility = Visibility.Hidden;
                SQL6.Visibility = Visibility.Visible;
                SQL7.Visibility = Visibility.Visible;
            }
        }
    }
}
