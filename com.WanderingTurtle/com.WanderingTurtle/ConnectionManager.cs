using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.BusinessLogic
{
    public static class ConnectionManager
    {
        public static void TestConnection()
        {
            DatabaseConnection.TestConnection();
        }
    }
}