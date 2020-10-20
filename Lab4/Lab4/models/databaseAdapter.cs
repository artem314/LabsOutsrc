using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab4
{
    class databaseAdapter
    {
        private static ProductsDbEntities db;

        private static string CurrentQuery { get; set; }
        private databaseAdapter() { }

        private static databaseAdapter _instance;
        public static databaseAdapter GetInstance()
        {
            if (_instance == null)
            {
                string connectionString;
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + projectDirectory + "\\ProductsDb.mdf;Integrated Security=True;Connect Timeout=30";

                try
                {
                    db = new ProductsDbEntities();
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

        private class ResProductsByGroups
        {
            public string groupName { get; set; }
            public int totalAmount { get; set; }
        }
        /// <summary>
        /// Возвращает результат выборки 9.1.Выборка количества (SALES+PURCHASE) товаров (PRODUCTS) по группам (GROUPS)
        /// </summary>
        /// <param name="GroupName">Группа</param>
        /// <returns>Коллекцию объектов ResProductsByGroups<see cref="ResProductsByGroups"/></returns>
        public static IEnumerable<object> getProductsByGroups(string GroupName)
        {
            if(db!=null)
            {
                try
                {
                    string query = "SELECT  Groups.product_group as groupName, SUM(Sales.amount + Purchase.amount) as totalAmount " +
                        "FROM Groups INNER JOIN Sales ON Groups.id = Sales.group_id " +
                        "INNER JOIN Purchase ON Groups.id = Purchase.group_id " +
                        "GROUP BY Groups.product_group " +
                        "ORDER BY totalAmount; ";

                    var result = new List<ResProductsByGroups>();
                    if (!String.IsNullOrEmpty(GroupName))
                    {
                        query = "SELECT  Groups.product_group as groupName, SUM(Sales.amount + Purchase.amount) as totalAmount " +
                       "FROM Groups INNER JOIN Sales ON Groups.id = Sales.group_id " +
                       "INNER JOIN Purchase ON Groups.id = Purchase.group_id " +
                       "WHERE Groups.product_group = @Group_Name" +
                       "GROUP BY Groups.product_group " +
                       "ORDER BY totalAmount; ";

                        result = db.Database.SqlQuery<ResProductsByGroups>(query,
                            new SqlParameter("@Group_Name", GroupName))
                            .ToList();
                        CurrentQuery = query;
                        return result;
                    }

                    CurrentQuery = query;
                    result = db.Database.SqlQuery<ResProductsByGroups>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return new List<object>();
            }
            else
            {
                MessageBox.Show("Необходимо подключить БД");
            }

        }

        private class ResSumBySuppliers
        {
            public string FIO { get; set; }
            public double sum { get; set; }
        }
        /// <summary>
        /// Возвращает результат выборки 9.2.Суммы закупок(PURCHASE) по поставщикам (SUPPLIERS)
        /// </summary>
        /// <param name="FIO">ФИО поставщика</param>
        /// <returns>Коллекцию объектов ResSumBySuppliers<see cref="ResSumBySuppliers"/></returns>
        public static IEnumerable<object> getSumBySuppliers(string FIO)
        {
            try
            {
                string query = "SELECT Suppliers.FIO as FIO, SUM(sum) as sum " +
               "FROM Purchase INNER JOIN Suppliers ON Purchase.supplier_id = Suppliers.Id " +
               "GROUP BY FIO";

                var result = new List<ResSumBySuppliers>();

                if (!String.IsNullOrEmpty(FIO))
                {
                    query = "SELECT Suppliers.FIO as FIO, SUM(sum) as sum " +
                         "FROM Purchase INNER JOIN Suppliers ON Purchase.supplier_id = Suppliers.Id " +
                         "WHERE Suppliers.FIO = @FIO " +
                         "GROUP BY FIO; ";
                    CurrentQuery = query;
                    result = db.Database.SqlQuery<ResSumBySuppliers>(query, new SqlParameter("@FIO", FIO)).ToList();
                    return result;
                }

                CurrentQuery = query;
                result = db.Database.SqlQuery<ResSumBySuppliers>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new List<object>();
        }


        private class ResSumBySuppliersForDate
        {
            public string FIO { get; set; }
            public DateTime date { get; set; }
            public double sum { get; set; }
        }
        /// <summary>
        /// Возвращает результат выборки 9.3.Суммы закупок (PURCHASE) по Поставщикам (SUPPLIERS) по Датам (date)
        /// </summary>
        /// <param name="FIO">ФИО поставщика</param>
        /// <param name="date">Дата</param>
        /// <returns>Коллекцию объектов ResSumBySuppliersForDate<see cref="ResSumBySuppliersForDate"/></returns>

        public static IEnumerable<object> getSumBySuppliersForDate(string FIO, DateTime date)
        {
            try
            {
                if (String.IsNullOrEmpty(FIO) || String.IsNullOrEmpty(date.ToString()))
                {
                    MessageBox.Show("Пожалуйста, введите два параметра ФИО и дату");
                    return new List<object>();
                }
           
                string query = "SELECT Suppliers.FIO as FIO,Purchase.date as date, Purchase.sum as sum " +
                    "FROM Purchase INNER JOIN Suppliers ON Purchase.supplier_id = Suppliers.Id " +
                    "WHERE FIO = @FIO AND Purchase.date <= @date ";

                var result = db.Database.SqlQuery<ResSumBySuppliersForDate>(query,
                    new SqlParameter("@FIO", FIO),
                    new SqlParameter("@date", date))
                    .ToList();

                CurrentQuery = query;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();

        }

        private class ResSumByCustomers
        {
            public int id { get; set; }
            public string FIO { get; set; }
            public double sum { get; set; }
        }
        /// <summary>
        /// Возвращает результат выборки 9.4.Суммы продаж (SALES) по покупателям(CUSTOMERS) за период 
        /// </summary>
        /// <param name="FIO">ФИО поставщика</param>
        /// <returns>Коллекцию объектов ResSumByCustomers<see cref="ResSumByCustomers"/></returns>
        public static IEnumerable<object> getSumByCustomers(string FIO)
        {
            try
            {
                var result = new List<ResSumByCustomers>();

                string query = "SELECT Customers.id as id, Customers.FIO as FIO, Sales.sum as sum " +
                "FROM Customers INNER JOIN Sales ON Customers.Id = Sales.customer_id; ";

                if (!String.IsNullOrEmpty(FIO))
                {
                    query = "SELECT Customers.id as id, Customers.FIO as FIO, Sales.sum as sum " +
                   "FROM Customers INNER JOIN Sales ON Customers.Id = Sales.customer_id " +
                   "WHERE FIO = @FIO;";

                    result = db.Database.SqlQuery<ResSumByCustomers>(query,
                    new SqlParameter("@FIO", FIO))
                    .ToList();

                    CurrentQuery = query;
                    return result;
                }
                CurrentQuery = query;
                result = db.Database.SqlQuery<ResSumByCustomers>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
        }

        private class ResMaxSalesByCustomer
        {
            public string FIO { get; set; }
            public double sum { get; set; }
        }

        /// <summary>
        /// Возвращает результат выборки 9.5.Максимальная продажа (SALES) по покупателю (CUSTOMER) за период 
        /// </summary>
        /// <param name="FIO">ФИО поставщика</param>
        /// <returns>Коллекцию объектов ResMaxSalesByCustomer<see cref="ResMaxSalesByCustomer"/></returns>
        public static IEnumerable<object> getMaxSalesByCustomer(string FIO)
        {
            try
            {
                var result = new List<ResMaxSalesByCustomer>();
                string query = "SELECT Customers.FIO as FIO, MAX(Sales.sum) as sum " +
                    "FROM Customers INNER JOIN Sales ON Customers.Id = Sales.customer_id " +
                    "GROUP BY FIO; ";

                if (!String.IsNullOrEmpty(FIO))
                {
                    query = "SELECT Customers.FIO as FIO, MAX(Sales.sum) as sum " +
                    "FROM Customers INNER JOIN Sales ON Customers.Id = Sales.customer_id " +
                    "WHERE Customers.FIO = @FIO " +
                    "GROUP BY FIO; ";

                result = db.Database.SqlQuery<ResMaxSalesByCustomer>(query,
                new SqlParameter("@FIO", FIO))
                .ToList();

                    CurrentQuery = query;
                    return result;
                }

                CurrentQuery = query;
                result = db.Database.SqlQuery<ResMaxSalesByCustomer>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
        }

        public static void CloseConnection()
        {
            if(_instance!=null)
            {
                db.Database.Connection.Close();
                _instance = null;
            }
        }
        public static string getCurrentQuery()
        {
            if (!String.IsNullOrEmpty(CurrentQuery) && _instance!=null)
                return CurrentQuery;
            return "";
        }
    }
}
