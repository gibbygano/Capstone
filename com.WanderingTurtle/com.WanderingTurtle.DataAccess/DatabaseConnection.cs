using System;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    /// <summary>
    /// Creates a connection string to connect to the database
    /// </summary>
    public static class DatabaseConnection
    {
        private const string ConnectionString = @"Data Source=localhost;Initial Catalog=WanderingTurtle.EventDatabase;Integrated Security=True";

        public static SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(ConnectionString);
        }

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