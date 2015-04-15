using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class SupplierLoginManagerTest
    {
        private SupplierLoginManager access = new SupplierLoginManager();
        private SupplierLoginAccessorTest testing = new SupplierLoginAccessorTest();
        private SupplierLogin retrieveSupplier = new SupplierLogin();

        [TestInitialize]
        public void initialize()
        {
            testing.initialize();
        }

        [TestMethod]
        public void TestSupplierLoginManageGet()
        {
            retrieveSupplier = access.retrieveSupplierLogin("Password#1", "F@k3logg3r");
            Assert.AreEqual("F@k3logg3r", retrieveSupplier.UserName);
        }

        [TestMethod]
        public void testSupplierLoginManageAdd()
        {
            int numberAdded = access.addSupplierLogin("TryM3!", 101);
            Assert.AreEqual("TryM3!", access.retrieveSupplierLogin("Password#1", "TryM3!").UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void TestSupplierLoginManageAddFail()
        {
            int numberAdded = access.addSupplierLogin("F@k3logg3r", 101);
        }

        [TestMethod]
        public void TestSupplierLoginManageArchive()
        {
            SupplierLogin change = access.retrieveSupplierLogin("Password#1", "F@k3logg3r");
            int num = access.archiveSupplierLogin(change, false);
            Assert.AreEqual(1, num);
        }

        [TestCleanup]
        public void cleanup()
        {
            testing.cleanUp();
        }
    }
}
