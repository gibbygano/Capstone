using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.DataAccess
{
    public class DatabaseConnection
    {
        const string ConnectionString = @"Data Source=localhost;Initial Catalog=EventDatabase;Integrated Security=True";
        public static SqlConnection getConnection() 
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
