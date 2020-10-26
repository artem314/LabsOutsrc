using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
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
                connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + projectDirectory + "\\movieDb.mdf;Integrated Security=True;Connect Timeout=30";
                
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

        public static IEnumerable<object> executeSQL(string sqlQuery, string table)
        {
            switch (table)
            {
                case "Movies": { getMovies(sqlQuery); break; }
                case "Details": { getDetails(sqlQuery); break; }
                case "Reviews": { getReviews(sqlQuery); break; }
                case "Cinema": { getCinemas(sqlQuery); break; }
                default: break;
            }
            return new List<object>();
        }

        public static IEnumerable<object> getCinemas(string query)
        {
            try
            {
                var result = new List<ResCinema>();

                CurrentQuery = query;
                result = db.Database.SqlQuery<ResCinema>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
        }

        public static IEnumerable<object> getMovies(string query)
        {
            try
            {
                var result = new List<ResMovie>();
                CurrentQuery = query;
                result = db.Database.SqlQuery<ResMovie>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
        }

        public static IEnumerable<object> getReviews(string query)
        {
            try
            {
                var result = new List<ResReviews>();
                CurrentQuery = query;
                result = db.Database.SqlQuery<ResReviews>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
        }

        public static IEnumerable<object> getDetails(string query)
        {
            try
            {
                var result = new List<ResDetails>();
                CurrentQuery = query;
                result = db.Database.SqlQuery<ResDetails>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
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

        private class ResCinema
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Session { get; set; }
            public int Seats { get; set; }
            public int Tickets { get; set; }
            public int Rating { get; set; }
        }

        private class ResDetails
        {
            public int Id { get; set; }
            public int Movie { get; set; }
            public string Description { get; set; }
            public string Genre { get; set; }
            public string Film_rating { get; set; }
            public string Main_character { get; set; }
            public string Running_time { get; set; }
            public int is3D { get; set; }
            public int World_score { get; set; }
        }

        private class ResMovie
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string Producer { get; set; }
            public DateTime Release_date { get; set; }
            public int? Cinema { get; set; }
        }

        private class ResReviews
        {
            public int Id { get; set; }
            public int Cinema { get; set; }
            public int Movie { get; set; }
            public string Review_text { get; set; }
            public int Rating { get; set; }
        }

        /// <summary>
        /// Возвращает записи в таблице кинотеатры
        /// </summary>
        /// <param>Таблица кинотеатры</param>
        /// <returns>Коллекцию объектов ResCinema<see cref="ResCinema"/></returns>
        public static IEnumerable<object> getCinemas()
        {
            try
            {
                var result = new List<ResCinema>();
                string query = "SELECT * FROM Cinema; ";

                CurrentQuery = query;
                result = db.Database.SqlQuery<ResCinema>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
        }

        /// <summary>
        /// Возвращает записи в таблице кинотеатры
        /// </summary>
        /// <param>Таблица кинотеатры</param>
        /// <returns>Коллекцию объектов ResCinema<see cref="ResCinema"/></returns>
        public static IEnumerable<object> getMovies()
        {
            try
            {
                var result = new List<ResMovie>();
                string query = "SELECT * FROM Movies; ";
                CurrentQuery = query;
                result = db.Database.SqlQuery<ResMovie>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
        }


        /// <summary>
        /// Возвращает записи в таблице кинотеатры
        /// </summary>
        /// <param>Таблица кинотеатры</param>
        /// <returns>Коллекцию объектов ResCinema<see cref="ResCinema"/></returns>
        public static IEnumerable<object> getReviews()
        {
            try
            {
                var result = new List<ResReviews>();
                string query = "SELECT * FROM Reviews; ";
                CurrentQuery = query;
                result = db.Database.SqlQuery<ResReviews>(query).ToList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
        }

        /// <summary>
        /// Возвращает записи в таблице кинотеатры
        /// </summary>
        /// <param>Таблица кинотеатры</param>
        /// <returns>Коллекцию объектов ResCinema<see cref="ResCinema"/></returns>
        public static IEnumerable<object> getDetails()
        {
            try
            {
                var result = new List<ResDetails>();
                string query = "SELECT * FROM Details; ";
                CurrentQuery = query;
                result = db.Database.SqlQuery<ResDetails>(query).ToList();
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
            if (_instance != null)
            {
                db.Database.Connection.Close();
                _instance = null;
            }
        }
        public static string getCurrentQuery()
        {
            if (!String.IsNullOrEmpty(CurrentQuery) && _instance != null)
                return CurrentQuery;
            return "";
        }
    }
}
