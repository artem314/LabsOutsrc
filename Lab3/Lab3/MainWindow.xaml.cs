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

                SQL5.Content = "VALUES";

                SQLUPD2.Text = "";
                SQLUPD1.Content = "";
                SQL2.Text = "";
                SQL3.Content = "";
                SQL8.Text = "(value1, value2, value3, ...)";
            }
            if (selectedItem == "System.Windows.Controls.ListBoxItem: SELECT")
            {
                SQLUPD1.Visibility = Visibility.Hidden;
                SQLUPD2.Visibility = Visibility.Hidden;
                SQL2.Visibility = Visibility.Visible;
                SQL3.Visibility = Visibility.Visible;
                SQL6.Visibility = Visibility.Visible;
                SQL7.Visibility = Visibility.Visible;

                SQLUPD2.Text = "";
                SQLUPD1.Content = "";
                SQL3.Content = "FROM";
                SQL5.Content = "WHERE";
                SQL8.Text = "1";

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

                SQLUPD2.Text = "";
                SQLUPD1.Content = "";
                SQL2.Text = "";
                SQL3.Content = "FROM";
                SQL5.Content = "WHERE";
                SQL8.Text = "1";
            }
            if (selectedItem == "System.Windows.Controls.ListBoxItem: UPDATE")
            {
                SQLUPD1.Visibility = Visibility.Visible;
                SQLUPD2.Visibility = Visibility.Visible;
                SQL2.Visibility = Visibility.Hidden;
                SQL3.Visibility = Visibility.Hidden;
                SQL6.Visibility = Visibility.Visible;
                SQL7.Visibility = Visibility.Visible;

                SQLUPD2.Text = "Name = 'Movie1'";
                SQLUPD1.Content = "SET";
                SQL2.Text = "";
                SQL3.Content = "";
                SQL5.Content = "WHERE";
                SQL8.Text = "1";
            }
        }
    }
}
