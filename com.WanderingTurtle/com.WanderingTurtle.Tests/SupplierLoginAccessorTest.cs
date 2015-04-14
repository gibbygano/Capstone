using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class SupplierLoginAccessorTest
    {
        private SupplierLoginAccessor access = new SupplierLoginAccessor();
        private SupplierAccessor access2 = new SupplierAccessor();
        private SupplierLogin retrieveSupplier;
        private Supplier fakeSupplier;
        
        [TestInitialize]
        public void initialize()
        {
            access.addSupplierLogin("F@k3logg3r", 101);
        }
        
        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// Tests to input a new SupplierLogin into the database.
        /// </summary>
        [TestMethod]
        public void TestSupplierLoginAdd()
        {
            int numberAdded = access.addSupplierLogin("TryM3!", 102);
            Assert.AreEqual("TryM3!", access.retrieveSupplierLogin("Password#1", "TryM3!").UserName);
        }

        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// Tests for if the get method works
        /// </summary>
        [TestMethod]
        public void TestSupplierLoginGet()
        {
            retrieveSupplier = access.retrieveSupplierLogin("Password#1", "F@k3logg3r");
            Assert.AreEqual("F@k3logg3r", retrieveSupplier.UserName, "There is no such supplier.");
        }
        
        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// Tests if a peice of data fails to retrieve data. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSupplierLoginGetFail()
        {
            retrieveSupplier = access.retrieveSupplierLogin("Password#1", "f@k3Loger");
        }

        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/09
        /// Tests if a piece of data will fail to add to database if F@k3logg3r already exists as a username.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void TestSupplierLoginAddFail()
        {
            int numberAdded = access.addSupplierLogin("F@k3logg3r", 101);
        }

        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/09
        /// Takes a piece of data and archives it.
        /// </summary>
        [TestMethod]
        public void TestSupplierLoginArchive()
        {
            SupplierLogin change = access.retrieveSupplierLogin("Password#1", "F@k3logg3r");
            int num = access.archiveSupplierLogin(change, false);
            Assert.AreEqual(1, num);
        }

        [TestCleanup]
        public void cleanUp()
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string commandText = @"DELETE FROM SupplierLogin WHERE UserName = 'F@k3logg3r'";
            string commandText2 = @"DELETE FROM SupplierLogin WHERE UserName='TryM3!'";

            var cmd = new SqlCommand(commandText, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(commandText2, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Fail!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
