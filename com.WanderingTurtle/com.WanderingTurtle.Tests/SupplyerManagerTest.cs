using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Tests the supplier manager methods
    /// Will Fritz 2015/3/27
    /// </summary>
    [TestClass]
    public class SupplyerManagerTest
    {
        SupplierManager SupplierMang = new SupplierManager();

        /// <summary>
        /// test the retrieve supplier method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void RetrieveSupplierTest()
        {
            //test 1 bad id
            string supplierID = "badID";
            Supplier testSupplier = SupplierMang.RetrieveSupplier(supplierID);
            Assert.IsNull(testSupplier);

            //test 2 empty string
            supplierID = "";
            testSupplier = SupplierMang.RetrieveSupplier(supplierID);
            Assert.IsNull(testSupplier);

            //test 3 null string
            supplierID = null;
            testSupplier = SupplierMang.RetrieveSupplier(supplierID);
            Assert.IsNull(testSupplier);
        }

        /// <summary>
        /// test the retrieve supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void RetrieveSupplierApplicationTest()
        {
            //test 1 bad id
            string supplierAppID = "badID";
            SupplierApplication testSupplierApp = SupplierMang.RetrieveSupplierApplication(supplierAppID);
            Assert.IsNull(testSupplierApp);

            //test 2 empty string
            supplierAppID = "";
            testSupplierApp = SupplierMang.RetrieveSupplierApplication(supplierAppID);
            Assert.IsNull(testSupplierApp);

            //test 3 null string
            supplierAppID = null;
            testSupplierApp = SupplierMang.RetrieveSupplierApplication(supplierAppID);
            Assert.IsNull(testSupplierApp);
        }

        /// <summary>
        /// Tests the retrieve suppliers method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void RetrieveSupplierListTest()
        {
            List<Supplier> suppliers = SupplierMang.RetrieveSupplierList();
            Assert.IsNotNull(suppliers);
        }

        /// <summary>
        /// Tests the retrieve suppliers Application method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void RetrieveSupplierApplicationListTest()
        {
            List<SupplierApplication> suppliersApps = SupplierMang.RetrieveSupplierApplicationList();
            Assert.IsNotNull(suppliersApps);
        }

        /// <summary>
        /// test the Add supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SqlTypeException))]
        public void AddSupplierApplicationTest()
        {
            //test 1 empty Supplier
            SupplierApplication testSupplierApp = new SupplierApplication();
            SupplierMang.AddASupplierApplication(testSupplierApp);

            //test 2 null Supplier
            testSupplierApp = null;
            SupplierMang.AddASupplierApplication(testSupplierApp);

            //test 3 half way filled out 
            testSupplierApp = new SupplierApplication();
            testSupplierApp.ApplicationDate = new DateTime();
            testSupplierApp.CompanyDescription = "test";
            testSupplierApp.CompanyName = "name";
            testSupplierApp.PhoneNumber = "234-234-2341";
            testSupplierApp.FirstName = "test";
            SupplierMang.AddASupplierApplication(testSupplierApp);
        }

        /// <summary>
        /// test the Add supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        /// <remarks>
        /// Edited by Rose Steffensmeier 2015/04/03
        /// </remarks>
        [TestMethod]
        public void AddSupplierTest()
        {
            //test 1 empty Supplier
            Supplier testSupplier = new Supplier();
            SupplierMang.AddANewSupplier(testSupplier);

            //test 2 null Supplier
            testSupplier = null;
            SupplierMang.AddANewSupplier(testSupplier);

            //test 3 half way filled out 
            testSupplier = new Supplier();
            testSupplier.CompanyName = "test";
            testSupplier.UserID = 100;
            testSupplier.PhoneNumber = "234-234-2341";
            testSupplier.FirstName = "test";
            SupplierMang.AddANewSupplier(testSupplier);
        }

        /// <summary>
        /// test the Archive supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        /// <remarks>
        /// Edited by Rose Steffensmeier 2015/04/03
        /// </remarks>
        [TestMethod]
        public void ArchiveSupplierTest()
        {
            //test 1 empty Supplier
            Supplier testSupplier = new Supplier();
            SupplierMang.ArchiveSupplier(testSupplier);

            //test 2 null Supplier
            testSupplier = null;
            SupplierMang.ArchiveSupplier(testSupplier);

            //test 3 half way filled out 
            testSupplier = new Supplier();
            testSupplier.CompanyName = "test";
            testSupplier.UserID = 100;
            testSupplier.PhoneNumber = "234-234-2341";
            testSupplier.FirstName = "test";
            SupplierMang.ArchiveSupplier(testSupplier);
        }

        /// <summary>
        /// test the Edit supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void EditSupplierTest()
        {
            //test 1 empty Suppliers
            Supplier testSupplier = new Supplier();
            SupplierMang.EditSupplier(testSupplier, testSupplier);

            //test 2 null Suppliers
            testSupplier = null;
            SupplierMang.EditSupplier(testSupplier, testSupplier);

            //test 3 half way filled out 
            testSupplier = new Supplier();
            testSupplier.CompanyName = "test";
            testSupplier.UserID = 100;
            testSupplier.PhoneNumber = "234-234-2341";
            testSupplier.FirstName = "test";
            SupplierMang.EditSupplier(testSupplier, testSupplier);

            //test4 one good one bad
            testSupplier = new Supplier();
            Supplier testSupplier2 = null;

            testSupplier.CompanyName = "test";
            testSupplier.Zip = "52342";
            testSupplier.LastName = "test";
            testSupplier.EmailAddress = "jk@jk.com";
            testSupplier.Address1 = "test 1234 st";
            testSupplier.Active = true;
            testSupplier.ApplicationID = 1;
            testSupplier.UserID = 100;
            testSupplier.PhoneNumber = "234-234-2341";
            testSupplier.FirstName = "test";

            SupplierMang.EditSupplier(testSupplier, testSupplier2);
        }

        /// <summary>
        /// test the Edit supplier Application method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SqlTypeException))]
        public void EditSupplierApplicationTest()
        {
            //test 1 empty Suppliers
            SupplierApplication testSupplierApp = new SupplierApplication();
            SupplierMang.EditSupplierApplication(testSupplierApp, testSupplierApp);

            //test 2 null Suppliers
            testSupplierApp = null;
            SupplierMang.EditSupplierApplication(testSupplierApp, testSupplierApp);

            //test 3 half way filled out 
            testSupplierApp = new SupplierApplication();
            testSupplierApp.CompanyName = "test";
            testSupplierApp.ApplicationID = 1;
            testSupplierApp.PhoneNumber = "234-234-2341";
            testSupplierApp.FirstName = "test";
            SupplierMang.EditSupplierApplication(testSupplierApp, testSupplierApp);

            //test4 one good one bad
            testSupplierApp = new SupplierApplication();
            SupplierApplication testSupplierApp2 = null;

            testSupplierApp.CompanyName = "test";
            testSupplierApp.Zip = "52342";
            testSupplierApp.LastName = "test";
            testSupplierApp.EmailAddress = "jk@jk.com";
            testSupplierApp.Address1 = "test 1234 st";
            testSupplierApp.Approved = true;
            testSupplierApp.ApplicationID = 1;
            testSupplierApp.CompanyDescription = "test";
            testSupplierApp.ApplicationDate = new DateTime(1999, 2, 3);
            testSupplierApp.ApprovalDate = new DateTime(2000, 2, 3);
            testSupplierApp.PhoneNumber = "234-234-2341";
            testSupplierApp.FirstName = "test";

            SupplierMang.EditSupplierApplication(testSupplierApp, testSupplierApp2);
        }
    }
}