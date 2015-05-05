using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.BusinessLogic
{
    public static class ConnectionManager
    {
        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/04
        /// 
        /// Tests the database connections
        /// </summary>
        public static void TestConnection()
        {
            DatabaseConnection.TestConnection();
        }
    }
}