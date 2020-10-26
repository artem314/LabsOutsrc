using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Lab4
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {

        private RelayCommand _connectCommand;
        public RelayCommand ConnectCommand
        {
            get
            {
                return _connectCommand ??
                  (_connectCommand = new RelayCommand(obj =>
                  {
                      databaseAdapter.GetInstance();
                  }));
            }
        }

        private RelayCommand _ShowCinemasCommand;
        public RelayCommand ShowCinemasCommand
        {
            get
            {
                return _ShowCinemasCommand ??
                  (_ShowCinemasCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getCinemas();
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }

        private RelayCommand _ShowMoviesCommand;
        public RelayCommand ShowMoviesCommand
        {
            get
            {
                return _ShowMoviesCommand ??
                  (_ShowMoviesCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getMovies();
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }

        private RelayCommand _ShowReviewsCommand;
        public RelayCommand ShowReviewsCommand
        {
            get
            {
                return _ShowReviewsCommand ??
                  (_ShowReviewsCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getReviews();
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }

        private RelayCommand _ShowDetailsCommand;
        public RelayCommand ShowDetailsCommand
        {
            get
            {
                return _ShowDetailsCommand ??
                  (_ShowDetailsCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getDetails();
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                  (_saveCommand = new RelayCommand(obj =>
                  {
                      using (StreamWriter fs = File.CreateText("TableDump.json"))
                      {
                          var json = JsonSerializer.Serialize<object>(TableData);
                          fs.Write(json);
                      }
                  }));
            }
        }

        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand ??
                  (_closeCommand = new RelayCommand(obj =>
                  {
                      databaseAdapter.CloseConnection();
                      App.Current.MainWindow.Close();
                  }));
            }
        }

        private string _queryText { get; set; }
        public string queryText
        {
            get { return _queryText; }
            set
            {
                _queryText = value;
                OnPropertyChanged("queryText");
            }
        }

        private string _queryCondition { get; set; }
        public string queryCondition
        {
            get { return _queryCondition; }
            set
            {
                _queryCondition = value;
                OnPropertyChanged("queryCondition");
            }
        }

        private DateTime _selectedDate { get; set; }
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private RelayCommand _executeSQLCommand;
        public RelayCommand executeSQLCommand
        {
            get
            {
                if (SQL1 != null && SQL4 != null)
                {
                    string sqlQuery = SQL1 + " " + SQL2 + " " + SQL3 + " " + SQL4 + " " + SQL5 + " " + SQL6 + " " + SQL7 + " " + SQL8 + " ";

                    databaseAdapter.executeSQL(sqlQuery, SQL4.ToString());
                    return _executeSQLCommand ??
                      (_executeSQLCommand = new RelayCommand(obj =>
                      {
                          TableData = databaseAdapter.executeSQL(sqlQuery, SQL4.ToString());
                          queryText = databaseAdapter.getCurrentQuery();
                      }));
                }
                return null;
            }
        }

        private RelayCommand _productsByGroupsCommand;
        public RelayCommand ProductsByGroupsCommand
        {
            get
            {
                return _productsByGroupsCommand ??
                  (_productsByGroupsCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getProductsByGroups(queryCondition);
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }

        private RelayCommand _sumBySuppliersCommand;
        public RelayCommand SumBySuppliersCommand
        {
            get
            {
                return _sumBySuppliersCommand ??
                  (_sumBySuppliersCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getSumBySuppliers(queryCondition);
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }

        private RelayCommand _getSumBySuppliersForDateCommand;
        public RelayCommand GetSumBySuppliersForDateCommand
        {
            get
            {
                return _getSumBySuppliersForDateCommand ??
                  (_getSumBySuppliersForDateCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getSumBySuppliersForDate(queryCondition, SelectedDate);
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }


        private RelayCommand _getSumByCustomersCommand;
        public RelayCommand GetSumByCustomersCommand
        {
            get
            {
                return _getSumByCustomersCommand ??
                  (_getSumByCustomersCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getSumByCustomers(queryCondition);
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }

        private RelayCommand _getCinemaCommand;
        public RelayCommand GetCinemaCommand
        {
            get
            {
                return _getCinemaCommand ??
                  (_getCinemaCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getCinemas();
                      queryText = databaseAdapter.getCurrentQuery();
                  }));
            }
        }

        public IEnumerable<object> _tableData { get; set; }

        public IEnumerable<object> TableData
        {
            get { return _tableData; }
            set
            {
                _tableData = value;
                OnPropertyChanged("TableData");
            }
        }

        public IEnumerable<string> _SQL1 { get; set; }

        public IEnumerable<string> SQL1
        {
            get { return _SQL1; }
            set
            {
                _SQL1 = value;
                OnPropertyChanged("SQL1");
            }
        }

        public IEnumerable<string> _SQL2 { get; set; }

        public IEnumerable<string> SQL2
        {
            get { return _SQL2; }
            set
            {
                _SQL2 = value;
                OnPropertyChanged("SQL2");
            }
        }

        public string SQL3 = "FROM";
        public IEnumerable<string> _SQL4 { get; set; }

        public IEnumerable<string> SQL4
        {
            get { return _SQL4; }
            set
            {
                _SQL4 = value;
                OnPropertyChanged("SQL4");
            }
        }


        public string SQL5 = "WHERE";


        public IEnumerable<string> _SQL6 { get; set; }

        public IEnumerable<string> SQL6
        {
            get { return _SQL6; }
            set
            {
                _SQL6 = value;
                OnPropertyChanged("SQL6");
            }
        }

        public IEnumerable<string> _SQL7 { get; set; }

        public IEnumerable<string> SQL7
        {
            get { return _SQL7; }
            set
            {
                _SQL7 = value;
                OnPropertyChanged("SQL7");
            }
        }

        public IEnumerable<string> _SQL8 { get; set; }

        public IEnumerable<string> SQL8
        {
            get { return _SQL8; }
            set
            {
                _SQL8 = value;
                OnPropertyChanged("SQL8");
            }
        }


        private RelayCommand _getFirstCommand;
        public RelayCommand GetFirstCommand
        {
            get
            {
                return _getFirstCommand ??
                  (_getFirstCommand = new RelayCommand(obj =>
                  {
                      SelectedItemInTable = TableData.First();
                  }));
            }
        }

        private object _selectedItemInTable { get; set; }
        public object SelectedItemInTable
        {
            get { return _selectedItemInTable; }
            set
            {
                _selectedItemInTable = value;
                OnPropertyChanged("SelectedItemInTable");
            }
        }

        private RelayCommand _getLastCommand;

        public RelayCommand GetLastCommand
        {
            get
            {
                return _getLastCommand ??
                  (_getLastCommand = new RelayCommand(obj =>
                  {
                      SelectedItemInTable = TableData.Last();
                  }));
            }
        }

        public ApplicationViewModel()
        {
            SelectedDate = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
