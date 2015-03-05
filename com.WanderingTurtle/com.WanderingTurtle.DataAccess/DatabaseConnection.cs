using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.DataAccess
{
    /// <summary>
    /// Creates a connection string to connect to the database
    /// </summary>
    public static class DatabaseConnection
    {
        private const string ConnectionString = @"Data Source=(localdb)\Capstone;Initial Catalog=com.WanderingTurtle.EventDatabase;Integrated Security=True";

        public static SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static void TestConnection()
        {
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

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