using Lab3;
using Lab4;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;

namespace Lab3
{
    class databaseAdapter
    {
        private static DbContext db;

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
                    db = new movieDBEntities();
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
            if(sqlQuery.Contains("SELECT"))
            switch (table)
            {
                case "Movies": { return getMovies(sqlQuery); }
                case "Details": { return getDetails(sqlQuery); }
                case "Reviews": { return getReviews(sqlQuery); }
                case "Cinema": { return getCinemas(sqlQuery); }
                default: return new List<object>();
            }
            try
            {
                switch (table)
                {
                    case "Movies":
                        {
                            var result = new List<ResMovie>();
                            result = db.Database.SqlQuery<ResMovie>(sqlQuery).ToList();
                            return getMovies();
                        }
                    case "Details":
                        {
                            var result = new List<ResDetails>();
                            result = db.Database.SqlQuery<ResDetails>(sqlQuery).ToList();
                            return getDetails();
                        }
                    case "Reviews":
                        {
                            var result = new List<ResReviews>();
                            result = db.Database.SqlQuery<ResReviews>(sqlQuery).ToList();
                            return getReviews();
                        }
                    case "Cinema":
                        {
                            var result = new List<ResCinema>();
                            result = db.Database.SqlQuery<ResCinema>(sqlQuery).ToList();
                            return getCinemas();
                        }
                    default: return new List<object>();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new List<object>();
            
        }

        public static IEnumerable<object> getCinemas(string query)
        {
            try
            {
                var result = new List<ResCinema>();
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
                result = db.Database.SqlQuery<ResDetails>(query).ToList();
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
