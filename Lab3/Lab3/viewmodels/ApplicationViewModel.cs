﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Controls;

namespace Lab3
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
                      SQL1 = "SELECT";
                      SQL2 = "*";

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
                      System.Windows.Application.Current.MainWindow.Close();
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
                return _executeSQLCommand ??
                  (_executeSQLCommand = new RelayCommand(obj =>
                  {
                      if (SQL1 != null && SQL4 != null)
                      {
                          string sqlQuery = SQL1 + " " + SQL2 + " " + SQL3 + " " + SQL4 + " " + SQLUPD1 + " " + SQLUPD2 + " " + SQL5 + " " + SQL6 + " " + SQL7 + " '" + SQL8 + "' ";
                          if(SQL1 == "INSERT INTO")  sqlQuery = SQL1 + " " + SQL2 + " " + SQL3 + " " + SQL4 + " " + SQLUPD1 + " " + SQLUPD2 + " " + SQL5 + " " + SQL6 + " " + SQL7 + " " + SQL8 + " ";
                          TableData = databaseAdapter.executeSQL(sqlQuery, SQL4.ToString());
                          queryText = databaseAdapter.getCurrentQuery();
                      }
                  }));

            }
        }

        private string _SQL1ChangeCommand;

        public string SQL1ChangeCommand
        {

            get { return _SQL1ChangeCommand; }
            set
            {

                string selectedItem = value;
                if (selectedItem == "System.Windows.Controls.ListBoxItem: INSERT INTO")
                {
                    SQLUPD2 = "";
                    SQLUPD1 = "";
                    SQL2 = "";
                    SQL3 = "";
                    SQL5 = "VALUES";
                    SQL8 = "(value1, value2, value3, ...)";
                }
                if (selectedItem == "System.Windows.Controls.ListBoxItem: SELECT" )
                {
                    SQLUPD2 = "";
                    SQLUPD1 = "";
                    SQL3 = "FROM";
                    SQL5 = "WHERE";
                    SQL8 = "1";
                }
                if (selectedItem == "System.Windows.Controls.ListBoxItem: DELETE")
                {
                    SQLUPD2 = "";
                    SQLUPD1 = "";
                    SQL2 = "";
                    SQL3 = "FROM";
                    SQL5 = "WHERE";
                    SQL8 = "1";
                }
                if (selectedItem == "System.Windows.Controls.ListBoxItem: UPDATE")
                {
                    SQLUPD2 = "Name = 'Movie1'";
                    SQLUPD1 = "SET";
                    SQL2 = "";
                    SQL3 = "";
                    SQL5 = "WHERE";
                    SQL8 = "1";
                }


                _SQL1ChangeCommand = value;
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
        public string _SQLUPD1 { get; set; }

        public string SQLUPD1
        {
            get { return _SQLUPD1; }
            set
            {
                _SQLUPD1 = value;
                OnPropertyChanged("SQLUPD1");
            }
        }
        public string _SQLUPD2 { get; set; }

        public string SQLUPD2
        {
            get { return _SQLUPD2; }
            set
            {
                _SQLUPD2 = value;
                OnPropertyChanged("SQLUPD2");
            }
        }

        public string _SQL1 { get; set; }

        public string SQL1
        {
            get { return _SQL1; }
            set
            {
                _SQL1 = value;
                OnPropertyChanged("SQL1");
            }
        }

        public string _SQL2 { get; set; }

        public string SQL2
        {
            get { return _SQL2; }
            set
            {
                _SQL2 = value;
                OnPropertyChanged("SQL2");
            }
        }

        public string SQL3 = "FROM";
        public string _SQL4 { get; set; }

        public string SQL4
        {
            get { return _SQL4; }
            set
            {
                _SQL4 = value;
                OnPropertyChanged("SQL4");
            }
        }


        public string SQL5 = "WHERE";


        public string _SQL6 { get; set; }

        public string SQL6
        {
            get { return _SQL6; }
            set
            {
                _SQL6 = value;
                OnPropertyChanged("SQL6");
            }
        }

        public string _SQL7 { get; set; }

        public string SQL7
        {
            get { return _SQL7; }
            set
            {
                _SQL7 = value;
                OnPropertyChanged("SQL7");
            }
        }

        public string _SQL8 { get; set; }

        public string SQL8
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
