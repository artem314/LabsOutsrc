using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Lab2
{
    class databaseAdapter
    {
        private static universityDBEntities db;

        private databaseAdapter() { }

        private static databaseAdapter _instance;
        public static databaseAdapter GetInstance()
        {
            if (_instance == null)
            {
                string connectionString;
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + projectDirectory + "\\universityDB.mdf;Integrated Security=True;Connect Timeout=30";

                try
                {
                    db = new universityDBEntities();
                    db.Database.Connection.ConnectionString = connectionString;
                    db.Database.Connection.Open();

                    _instance = new databaseAdapter();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
            return _instance;
        }

        public static string[] getTableNames()
        {
            if (db != null)
            {
                string[] tableNames = new string[0];
                try
                {
                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
                    tableNames = db.Database.SqlQuery<string>(query).ToArray();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return tableNames;
            }
            return null;
        }

        public static string[] getColumnNames(string tableName)
        {
            if (db != null)
            {
                string[] columnNames = new string[0];
                try
                {
                    string query = "SELECT COLUMN_NAME " +
                        "FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table_name";
                    columnNames = db.Database.SqlQuery<string>(query, 
                        new SqlParameter("@table_name", tableName))
                        .ToArray();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return columnNames;
            }
            return null;
        }
    }
}
