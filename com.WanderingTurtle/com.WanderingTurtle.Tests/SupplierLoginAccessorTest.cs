using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class SupplierLoginAccessorTest
    {
        private SupplierLoginAccessor access = new SupplierLoginAccessor();
        private SupplierLogin retrieveSupplier;

        [TestMethod]
        public void TestSupplierLoginGet()
        {
            retrieveSupplier = access.retrieveSupplierLogin("Password#1", "Ch@rmb1");
            //Console.WriteLine(retrieveSupplier.UserID.ToString() + " now " + retrieveSupplier.UserName + " now " + retrieveSupplier.UserPassword);
        }

        [TestMethod]
        public void TestSupplierLoginAdd()
        {
            int numberAdded = access.addSupplierLogin("f@k3Loger");
        }
        
        //this method should always fail
        [TestMethod]
        public void TestSupplierLoginAddFail()
        {
            int numberAdded = access.addSupplierLogin("Chr0m!");
        }

        [TestMethod]
        public void TestSupplierLoginArchive()
        {
            SupplierLogin change = new SupplierLogin("Password#1", "Frank0$");
            change.UserID = 101;
            change.Active = true;
            Console.WriteLine("UserID: " + change.UserID + "UserName: " + change.UserName);
            int num = access.archiveSupplierLogin(change, false);
        }
    }
}
