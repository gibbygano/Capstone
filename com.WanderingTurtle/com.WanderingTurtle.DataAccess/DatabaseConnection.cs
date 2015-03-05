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
    public class DatabaseConnection
    {
        private const string myHomeString= @"Data Source=(localdb)\Projects;Initial Catalog=com.WanderingTurtle.EventDatabase;Integrated Security=True";
        private const string ConnectionString = myHomeString;//@"Data Source=localhost;Initial Catalog=com.WanderingTurtle.EventDatabase;Integrated Security=True";
        
        public static SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
