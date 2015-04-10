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
        SupplierApplication testSupplierApp = new SupplierApplication();
        SupplierApplication testSupplierAppRetrieval = new SupplierApplication();
        string supplierID = "";
        string supplierAppID = "";
        Supplier testSupplier = new Supplier();
        Supplier testSupplierRetrieve = new Supplier();

        private void Setup()
        {
            //set up supplier
            testSupplier = new Supplier();
            testSupplier.CompanyName = "Awsome Tours";
            testSupplier.UserID = 101;
            testSupplier.PhoneNumber = "575-542-8796";
            testSupplier.FirstName = "Blab";
            testSupplier.Active = true;
            testSupplier.Address1 = "255 East West St";
            testSupplier.LastName = "blabla";
            testSupplier.Zip = "50229";
            testSupplier.ApplicationID = 100;
            testSupplier.EmailAddress = "blabla@gmail.com";
            testSupplier.Address2 = "";

            //setup Supplier application
            testSupplierApp = new SupplierApplication();
            testSupplierApp.ApplicationDate = new DateTime(2005, 2, 3);
            testSupplierApp.CompanyName = "Awsome Tours";
            testSupplierApp.CompanyDescription = "tours of awsomeness";
            testSupplierApp.PhoneNumber = "575-542-8796";
            testSupplierApp.FirstName = "Blab";
            testSupplierApp.Address1 = "255 East West St";
            testSupplierApp.LastName = "blabla";
            testSupplierApp.Zip = "50229";
            testSupplierApp.ApplicationID = 100;
            testSupplierApp.EmailAddress = "blabla@gmail.com";
            testSupplierApp.Address2 = "";
            testSupplierApp.Approved = true;
            testSupplierApp.FinalStatusDate = new DateTime(2005, 2, 3);
        }

        /// <summary>
        /// test the retrieve supplier method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RetrieveSupplierBadIDTest()
        {
            string supplierID = "badID";
            testSupplierRetrieve = SupplierMang.RetrieveSupplier(supplierID);
            Assert.IsNull(testSupplierRetrieve);
        }

        /// <summary>
        /// test the retrieve supplier method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RetrieveSupplierEmptyIDTest()
        {
            supplierID = "";
            testSupplierRetrieve = SupplierMang.RetrieveSupplier(supplierID);
            Assert.IsNull(testSupplierRetrieve);
        }

        /// <summary>
        /// test the retrieve supplier method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RetrieveSupplierNullTest()
        {
            supplierID = null;
            testSupplierRetrieve = SupplierMang.RetrieveSupplier(supplierID);
            Assert.IsNull(testSupplierRetrieve);
        }

        /// <summary>
        /// test the retrieve supplier method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        public void RetrieveSupplierWorkingTest()
        {
            supplierID = "100";
            testSupplierRetrieve = SupplierMang.RetrieveSupplier(supplierID);
            Assert.IsNotNull(testSupplierRetrieve);
        }

        /// <summary>
        /// test the retrieve supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RetrieveSupplierApplicationBadIDTest()
        {
            supplierAppID = "badID";
            SupplierApplication testSupplierAppRetrieval = SupplierMang.RetrieveSupplierApplication(supplierAppID);
            Assert.IsNull(testSupplierAppRetrieval);
        }

        /// <summary>
        /// test the retrieve supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RetrieveSupplierApplicationEmptyIDTest()
        {
            supplierAppID = "";
            testSupplierAppRetrieval = SupplierMang.RetrieveSupplierApplication(supplierAppID);
            Assert.IsNull(testSupplierAppRetrieval);
        }

        /// <summary>
        /// test the retrieve supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RetrieveSupplierApplicationNullIDTest()
        {
            supplierAppID = null;
            testSupplierAppRetrieval = SupplierMang.RetrieveSupplierApplication(supplierAppID);
            Assert.IsNull(testSupplierAppRetrieval);
        }

        /// <summary>
        /// test the retrieve supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RetrieveSupplierApplicationWorkingTest()
        {
            supplierAppID = "100";
            testSupplierAppRetrieval = SupplierMang.RetrieveSupplierApplication(supplierAppID);
            Assert.IsNotNull(testSupplierAppRetrieval);
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
        [ExpectedException(typeof(Exception))]
        public void AddSupplierApplicationEmptyTest()
        {
            SupplierApplication SupplierAppEmpty = new SupplierApplication();
            SupplierMang.AddASupplierApplication(SupplierAppEmpty);
        }

        /// <summary>
        /// test the Add supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddSupplierApplicationNullTest()
        {
            SupplierApplication SupplierAppNull = null;
            SupplierMang.AddASupplierApplication(SupplierAppNull);
        }

        /// <summary>
        /// test the Add supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddSupplierApplicationPartialTest()
        {
            SupplierApplication testSupplierApp2 = new SupplierApplication();
            testSupplierApp2.ApplicationDate = new DateTime();
            testSupplierApp2.CompanyDescription = "test";
            testSupplierApp2.CompanyName = "name";
            testSupplierApp2.PhoneNumber = "234-234-2341";
            testSupplierApp2.FirstName = "test";
            SupplierMang.AddASupplierApplication(testSupplierApp2);
        }

        /// <summary>
        /// test the Add supplier Application method
        /// Will Fritz 2015/3/27
        /// </summary>
        [TestMethod]
        public void AddSupplierApplicationWorkingTest()
        {
            Setup();
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
        public void AddSupplierEmptyTest()
        {
            //test 1 empty Supplier
            Supplier testSupplierEmpty = new Supplier();
            Assert.AreEqual(SupplierMang.AddANewSupplier(testSupplierEmpty), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Add supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        /// <remarks>
        /// Edited by Rose Steffensmeier 2015/04/03
        /// </remarks>
        [TestMethod]
        public void AddSupplierNullTest()
        {
            Supplier testSupplierNull = null;
            Assert.AreEqual(SupplierMang.AddANewSupplier(testSupplierNull), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Add supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void AddSupplierPartialTest()
        {
            Supplier testSupplierPartial = new Supplier();
            testSupplierPartial.CompanyName = "test";
            testSupplierPartial.UserID = 100;
            testSupplierPartial.PhoneNumber = "234-234-2341";
            testSupplierPartial.FirstName = "test";
            Assert.AreEqual(SupplierMang.AddANewSupplier(testSupplierPartial), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Add supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void AddSupplierWorkingTest()
        {
            Setup();
            Assert.AreEqual(SupplierMang.AddANewSupplier(testSupplier), SupplierResult.Success);
        }

        /// <summary>
        /// test the Edit supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void EditSupplierEmptyTest()
        {
            Supplier testSupplierEmpty = new Supplier();
            Assert.AreEqual(SupplierMang.EditSupplier(testSupplierEmpty, testSupplierEmpty), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Edit supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void EditSupplierNullTest()
        {
            Supplier testSupplierNull = null;
            Assert.AreEqual(SupplierMang.EditSupplier(testSupplierNull, testSupplierNull), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Edit supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void EditSupplierPartialTest()
        {
            Supplier testSupplierPartial = new Supplier();
            testSupplierPartial.CompanyName = "test";
            testSupplierPartial.UserID = 100;
            testSupplierPartial.PhoneNumber = "234-234-2341";
            testSupplierPartial.FirstName = "test";
            Assert.AreEqual(SupplierMang.EditSupplier(testSupplierPartial, testSupplierPartial), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Edit supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void EditSupplierPartialTest2()
        {
            Supplier testSupplier2 = null;

            Assert.AreEqual(SupplierMang.EditSupplier(testSupplier, testSupplier2), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Edit supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void EditSupplierworkingTest()
        {
            Setup();
            Supplier testSupplier2 = testSupplier;
            testSupplier2.CompanyName = "Awsomest Tours";
            testSupplier2.SupplierID = 103;

            Assert.AreEqual(SupplierMang.EditSupplier(testSupplier, testSupplier2), SupplierResult.Success);
        }

        /// <summary>
        /// test the Edit supplier Application method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EditSupplierApplicationEmptyTest()
        {
            SupplierApplication testSupplierAppEmpty = new SupplierApplication();
            SupplierMang.EditSupplierApplication(testSupplierAppEmpty, testSupplierAppEmpty);
        }

        /// <summary>
        /// test the Edit supplier Application method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EditSupplierApplicationNullTest()
        {
            SupplierApplication testSupplierAppNull = null;
            SupplierMang.EditSupplierApplication(testSupplierAppNull, testSupplierAppNull);
        }

        /// <summary>
        /// test the Edit supplier Application method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EditSupplierApplicationPartialTest()
        {
            SupplierApplication testSupplierAppPartial = new SupplierApplication();
            testSupplierAppPartial.CompanyName = "test";
            testSupplierAppPartial.ApplicationID = 1;
            testSupplierAppPartial.PhoneNumber = "234-234-2341";
            testSupplierAppPartial.FirstName = "test";
            SupplierMang.EditSupplierApplication(testSupplierAppPartial, testSupplierAppPartial);
        }

        /// <summary>
        /// test the Edit supplier Application method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EditSupplierApplicationPartialTest2()
        {
            SupplierApplication testSupplierApp2 = null;

            SupplierMang.EditSupplierApplication(testSupplierApp, testSupplierApp2);
        }

        /// <summary>
        /// test the Edit supplier Application method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void EditSupplierApplicationWorkingTest()
        {
            Setup();
            SupplierApplication testSupplierApp2 = testSupplierApp;
            testSupplierApp2.CompanyName = "Awsomest Tours";
            testSupplierApp2.CompanyDescription = "tours of epicness";
            testSupplierApp2.LastName = "blabla";
            testSupplierApp2.Zip = "50229";
            testSupplierApp2.EmailAddress = "blabla@gmail.com";
            testSupplierApp2.Address2 = "";
            testSupplierApp2.Approved = true;
            testSupplierApp2.FinalStatusDate = new DateTime(2005, 8, 3);

            SupplierMang.EditSupplierApplication(testSupplierApp, testSupplierApp2);
            testSupplierApp = testSupplierApp2;
        }

        /// <summary>
        /// test the Archive supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void ArchiveSupplierEmptyTest()
        {
            Supplier testSupplierEmpty = new Supplier();
            Assert.AreEqual(SupplierMang.ArchiveSupplier(testSupplierEmpty), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Archive supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void ArchiveSupplierNullTest()
        {
            Supplier testSupplierNull = null;
            Assert.AreEqual(SupplierMang.ArchiveSupplier(testSupplierNull), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Archive supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void ArchiveSupplierPartialTest()
        {
            Supplier testSupplierParital = new Supplier();
            testSupplierParital.CompanyName = "test";
            testSupplierParital.UserID = 100;
            testSupplierParital.PhoneNumber = "234-234-2341";
            testSupplierParital.FirstName = "test";
            Assert.AreEqual(SupplierMang.ArchiveSupplier(testSupplierParital), SupplierResult.DatabaseError);
        }

        /// <summary>
        /// test the Archive supplier method
        /// Will Fritz 2015/3/31
        /// </summary>
        [TestMethod]
        public void ArchiveSupplierWorkingTest()
        {
            Setup();
            testSupplier.CompanyName = "Awsomest Tours";

            Assert.AreEqual(SupplierMang.ArchiveSupplier(testSupplier), SupplierResult.Success);
            SupplierMang.deleteTestSupplier(testSupplier);
        }
    }
}