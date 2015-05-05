using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using com.WanderingTurtle.BusinessLogic;


namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class SupplierLoginManagerTest
    {
        private SupplierLoginManager access = new SupplierLoginManager();
        private SupplierLoginAccessorTest testing = new SupplierLoginAccessorTest();
        private SupplierLogin retrieveSupplier = new SupplierLogin();
        private int suppID;
        Supplier testSupplier = new Supplier();

        public void setup()
        {
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

            
            try
            {
                SupplierAccessor.AddSupplier(testSupplier, "Test", "Password#1");
                var supList = SupplierAccessor.GetSupplierList();
                foreach (Supplier x in supList)
                {
                    if (x.FirstName.Equals("FakeLogin"))
                    {
                        suppID = x.SupplierID;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("what");
            }
        }

        [TestMethod]
        public void TestSupplierLoginManageGet()
        {
            setup();
            retrieveSupplier = access.RetrieveSupplierLogin("Password#1", "Test");
            Assert.AreEqual("Test", retrieveSupplier.UserName);
        }

        [TestMethod]
        public void TestSupplierUserNameGet()
        {
            setup();
            Assert.AreEqual("Test", access.RetrieveSupplierUserName(suppID));
        }

        [TestMethod]
        public void TestSupplierLoginUpdate()
        {
            setup();
            retrieveSupplier = access.RetrieveSupplierLogin("Password#1", "Test");
            Assert.AreEqual(ResultsEdit.Success, access.UpdateSupplierLogin("Password#2", retrieveSupplier));
        }

        [TestMethod]
        public void TestLoginUpdateFail()
        {
            setup();
            retrieveSupplier = access.RetrieveSupplierLogin("Password#1", "Test");
            retrieveSupplier.UserName = "Tested";

            Assert.AreEqual(ResultsEdit.DatabaseError, access.UpdateSupplierLogin("Password#2", retrieveSupplier));
        }

        [TestCleanup]
        public void cleanUp()
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string commandText = @"DELETE FROM SupplierLogin WHERE UserName='Test'";

            string commandText2 = @"DELETE FROM Supplier WHERE CompanyName = 'fakeCompany'";

            var cmd = new SqlCommand(commandText, conn);
            var cmd2 = new SqlCommand(commandText2, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
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