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
        private SupplierLogin retrieveSupplier;
        
        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// Tests to input a new SupplierLogin into the database.
        /// </summary>
        [TestMethod]
        public void TestSupplierLoginAdd()
        {
            int numberAdded = access.addSupplierLogin("f@k3Loger");
            Assert.AreEqual(1, numberAdded);
        }
        
        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// Tests to see if the login fails.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void TestSupplierLoginAddFail()
        {
            TestSupplierLoginAdd();
            int numberAdded = access.addSupplierLogin("f@k3Loger");
        }

        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// Tests for if the get method works
        /// </summary>
        [TestMethod]
        public void TestSupplierLoginGet()
        {
            TestSupplierLoginAdd();
            retrieveSupplier = access.retrieveSupplierLogin("Password#1", "f@k3Loger");
            Assert.AreEqual("f@k3Loger", retrieveSupplier.UserName, "There is no such supplier.");
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

        [TestMethod]
        public void TestSupplierLoginArchive()
        {
            TestSupplierLoginGet();
            int num = access.archiveSupplierLogin(retrieveSupplier, false);
            Assert.AreEqual(1, num, "Data was not changed.");
        }

        [TestMethod]
        public void TestSupplierLoginArchiveFail()
        {
            TestSupplierLoginGet();
            retrieveSupplier.UserName = "faker";
            int num = access.archiveSupplierLogin(retrieveSupplier, false);
            Assert.AreEqual(0, num, "The data was changed.");

        }

        [TestCleanup]
        public void cleanUp()
        {
            var conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=com.WanderingTurtle.EventDatabase;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            string commandLine = "DELETE FROM [SupplierLogin] WHERE [UserName] = @userName";

            var cmd = new SqlCommand(commandLine, conn);
            cmd.Parameters.AddWithValue("@userName", "f@k3Loger");

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                Console.WriteLine("Not working");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
