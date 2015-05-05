using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;


namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class SupplierLoginAccessorTest
    {
        private SupplierLoginAccessor access = new SupplierLoginAccessor();
        private SupplierAccessor access2 = new SupplierAccessor();
        private SupplierLogin retrieveSupplier;
        private int suppID;

        public void setup()
        {
            Supplier testSupplier = new Supplier();
            testSupplier = new Supplier();
            testSupplier.CompanyName = "fakeCompany";
            testSupplier.FirstName = "FakeLogin";
            testSupplier.LastName = "FakeLogin";
            testSupplier.Address1 = "255 East West St";
            testSupplier.Address2 = "APT 1";
            testSupplier.Zip = "50229";
            testSupplier.PhoneNumber = "575-542-8796";
            testSupplier.EmailAddress = "FakeLogin@gmail.com";
            testSupplier.ApplicationID = 999;
            testSupplier.SupplyCost = (decimal)((60) / 100);
            testSupplier.Active = true;

            SupplierAccessor.AddSupplier(testSupplier, "Test", "Password#1");
            try
            {

                var supList = SupplierAccessor.GetSupplierList();
                foreach (Supplier x in supList)
                {
                    if (x.FirstName.Equals("FirstBlab"))
                    {
                        suppID = x.SupplierID;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("what");
            }



        }

        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// Tests for if the get method works
        /// </summary>
        [TestMethod]
        public void TestSupplierLoginGet()
        {
            setup();
            retrieveSupplier = access.RetrieveSupplierLogin("Password#1", "Test");
            Assert.AreEqual("Test", retrieveSupplier.UserName);
        }

        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// Tests if a peice of data fails to retrieve data.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSupplierLoginGetFail()
        {
            setup();
            retrieveSupplier = access.RetrieveSupplierLogin("Password#2", "Test");
        }

        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/09
        /// Tests if a piece of data will fail to add to database if F@k3logg3r already exists as a username.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void TestSupplierLoginAddFail()
        {
            int numberAdded = access.AddSupplierLogin("Test", 101);
        }

        [TestCleanup]
        public void cleanUp()
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string commandText = @"DELETE FROM SupplierLogin WHERE UserName='Test!'";

            var cmd = new SqlCommand(commandText, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
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