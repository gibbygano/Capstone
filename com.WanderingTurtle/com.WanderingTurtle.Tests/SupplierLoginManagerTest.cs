using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class SupplierLoginManagerTest
    {
        private SupplierLoginManager access = new SupplierLoginManager();
        private SupplierLoginAccessorTest testing = new SupplierLoginAccessorTest();
        private SupplierLogin retrieveSupplier = new SupplierLogin();

        [TestMethod]
        public void TestSupplierLoginManageGet()
        {
            retrieveSupplier = access.retrieveSupplierLogin("Password#1", "F@k3logg3r");
            Assert.AreEqual("F@k3logg3r", retrieveSupplier.UserName);
        }

        [TestCleanup]
        public void cleanup()
        {
            testing.cleanUp();
        }
    }
}