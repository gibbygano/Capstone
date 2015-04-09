﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class SupplierLoginAccessorTest
    {
        private SupplierLoginAccessor access = new SupplierLoginAccessor();
        private SupplierLogin retrieveSupplier;

        [TestInitialize]
        public void initialize()
        {
            access.addSupplierLogin("F@k3logg3r");
        }

        [TestMethod]
        public void TestSupplierLoginGet()
        {
            retrieveSupplier = access.retrieveSupplierLogin("Password#1", "F@k3logg3r");
            Assert.AreEqual("F@k3logg3r", retrieveSupplier.UserName);
        }

        [TestMethod]
        public void TestSupplierLoginAdd()
        {
            int numberAdded = access.addSupplierLogin("TryM3!");
            Assert.AreEqual("TryM3!", access.retrieveSupplierLogin("Password#1", "TryM3!").UserName);
        }
        
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void TestSupplierLoginAddFail()
        {
            int numberAdded = access.addSupplierLogin("F@k3logg3r");
        }

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
