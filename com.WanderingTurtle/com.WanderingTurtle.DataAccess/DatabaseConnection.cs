using System;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    /// <summary>
    /// Creates a connection string to connect to the database
    /// </summary>
    public static class DatabaseConnection
    {
        private const string ConnectionString = @"Data Source=HUNTER\SQLEXPRESS;Initial Catalog=WanderingTurtle.EventDatabase;Integrated Security=True";


        /// <summary>
        /// Miguel Santana
        /// Created:  2015/02/06
        /// Get the database string
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/03/04
        /// Checks to see if db is connected
        /// </summary>
        public static void TestConnection()
        {
            SqlConnection conn = GetDatabaseConnection();

            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}