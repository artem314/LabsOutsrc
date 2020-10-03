using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                         var json =  JsonSerializer.Serialize<object>(TableData);
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

        private RelayCommand _getMaxSalesByCustomerCommand;
        public RelayCommand GetMaxSalesByCustomerCommand
        {
            get
            {
                return _getMaxSalesByCustomerCommand ??
                  (_getMaxSalesByCustomerCommand = new RelayCommand(obj =>
                  {
                      TableData = databaseAdapter.getMaxSalesByCustomer(queryCondition);
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
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
