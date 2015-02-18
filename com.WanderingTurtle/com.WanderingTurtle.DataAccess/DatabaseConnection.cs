using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.DataAccess
{
    public class DatabaseConnection
    {
        private const string ConnectionString = @"Data Source=localhost;Initial Catalog=com.WanderingTurtle.EventDatabase;Integrated Security=True";
        
        public static SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}