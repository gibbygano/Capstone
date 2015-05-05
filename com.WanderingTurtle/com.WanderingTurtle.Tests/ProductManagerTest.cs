using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class ProductManagerTest
    {
        ProductManager pMgr = new ProductManager();
        SupplierLoginAccessor sLA = new SupplierLoginAccessor();
        Supplier testSupp = new Supplier();
        Supplier modSupp = new Supplier();
        SupplierLogin testLog = new SupplierLogin();
        ItemListing itemListingToTest = new ItemListing();
        ItemListing itemListingToEdit = new ItemListing();
        List<ItemListing> itemList = new List<ItemListing>();
        List<Supplier> suppList = new List<Supplier>();

        /// <summary>
        /// Bryan Hurst 4/24/2015
        /// Setup method for ItemListingAccessorTest unit tests
        /// </summary>
        public void Setup()
        {
            itemListingToTest.ItemListID = 204;
            itemListingToTest.EventID = 102;
            itemListingToTest.StartDate = new DateTime(2015, 5, 28, 8, 30, 0);
            itemListingToTest.EndDate = new DateTime(2525, 5, 28, 10, 30, 0);
            itemListingToTest.Price = 10.0000M;
            itemListingToTest.MaxNumGuests = 11;
            itemListingToTest.MinNumGuests = 0;
            itemListingToTest.CurrentNumGuests = 10;
            itemListingToTest.SupplierID = 100;

            itemListingToEdit.ItemListID = 204;
            itemListingToEdit.EventID = 102;
            itemListingToEdit.StartDate = new DateTime(2015, 5, 28, 8, 30, 0);
            itemListingToEdit.EndDate = new DateTime(2525, 5, 28, 10, 30, 0);
            itemListingToEdit.Price = 10.0000M;
            itemListingToEdit.MaxNumGuests = 15;
            itemListingToEdit.MinNumGuests = 1;
            itemListingToEdit.CurrentNumGuests = 8;
            itemListingToEdit.SupplierID = 100;

            testSupp.CompanyName = "Test";
            testSupp.FirstName = "Test";
            testSupp.LastName = "Test";
            testSupp.Address1 = "Test";
            testSupp.Address2 = "Test";
            testSupp.Zip = "55555";
            testSupp.PhoneNumber = "555-555-5555";
            testSupp.EmailAddress = "Test@Test.Test";
            testSupp.ApplicationID = 0;
            testSupp.SupplyCost = new decimal(.7);
        }

        public void EditSetup()
        {
            SupplierAccessor.AddSupplier(testSupp, "Test", "Password#1");
            modSupp = getSupplierListCompName(suppList);
            itemListingToEdit.SupplierID = modSupp.SupplierID;
            ItemListingAccessor.AddItemListing(itemListingToTest);
            itemListingToTest = getItemListingTestObject(itemList);
            itemListingToEdit.SupplierID = modSupp.SupplierID;
        }

        public void Cleanup()
        {
            ItemListingAccessor.DeleteItemListingTestItem(itemListingToTest);
            testSupp.SupplierID = modSupp.SupplierID;
            testLog = sLA.RetrieveSupplierLogin("Password#1", "Test");
            SupplierLoginAccessor.DeleteTestSupplierLogin(testLog);
            TestCleanupAccessor.DeleteTestSupplier(testSupp);
        }

        /// <summary>
        /// Bryan Hurst 4/24/2015
        /// Retrieves the test ItemListing object used in this class's unit tests
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private ItemListing getItemListingTestObject(List<ItemListing> list)
        {
            list = ItemListingAccessor.GetItemListingList();
            foreach (ItemListing item in list)
            {
                if (item.EndDate.Equals(new DateTime(2525, 5, 28, 10, 30, 0)))
                {
                    return item;
                }
            }
            return new ItemListing();
        }

        /// <summary>
        /// Bryan Hurst 4/24/2015
        /// Retrieves the Test Supplier record for use in the test methods
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private Supplier getSupplierListCompName(List<Supplier> list)
        {
            list = SupplierAccessor.GetSupplierList();
            foreach (Supplier item in list)
            {
                if (item.CompanyName.Equals("Test"))
                {
                    return item;
                }
            }
            return new Supplier();
        }
        /// <summary>
        /// Bryan Hurst 4/24/2015
        /// Tests adding a valid ItemListing object
        /// </summary>
        [TestMethod]
        public void AddItemListing_Success()
        {
            Setup();

            SupplierAccessor.AddSupplier(testSupp, "Test", "Password#1");
            modSupp = getSupplierListCompName(suppList);
            itemListingToTest.SupplierID = modSupp.SupplierID;

            listResult actual = pMgr.AddItemListing(itemListingToTest);

            Cleanup();

            Assert.AreEqual(listResult.Success, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/24/2015
        /// Tests updating a valid ItemListing record
        /// </summary>
        [TestMethod]
        public void EditItemListing_ValidItemListing()
        {
            Setup();

            EditSetup();

            listResult actual = pMgr.EditItemListing(itemListingToEdit, itemListingToTest);

            Cleanup();

            Assert.AreEqual(listResult.Success, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/27/2015
        /// Tests attempting to update an ItemListing record with mismatched dates
        /// </summary>
        [TestMethod]
        public void EditItemListing_NoDateChange()
        {
            Setup();

            EditSetup();
            itemListingToEdit.StartDate = new DateTime(2025, 5, 28, 8, 30, 0);

            listResult actual = pMgr.EditItemListing(itemListingToEdit, itemListingToTest);

            Cleanup();

            Assert.AreEqual(listResult.NoDateChange, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/27/2015
        /// Tests attempting to update an ItemListing record with a lower max capacity than current capacity
        /// </summary>
        [TestMethod]
        public void EditItemListing_MaxSmallerThanCurrent()
        {
            Setup();

            EditSetup();
            itemListingToEdit.MaxNumGuests = 1;

            listResult actual = pMgr.EditItemListing(itemListingToEdit, itemListingToTest);

            Cleanup();

            Assert.AreEqual(listResult.MaxSmallerThanCurrent, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/27/2015
        /// Tests attempting to update an ItemListing record with mismatched prices
        /// </summary>
        [TestMethod]
        public void EditItemListing_NoPriceChange()
        {
            Setup();

            EditSetup();
            itemListingToEdit.Price = 20.0000M;

            listResult actual = pMgr.EditItemListing(itemListingToEdit, itemListingToTest);

            Cleanup();

            Assert.AreEqual(listResult.NoPriceChange, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/27/2015
        /// Tests attempting to update an ItemListing record with a start date past the end date
        /// </summary>
        [TestMethod]
        public void EditItemListing_StartEndDateError()
        {
            Setup();

            EditSetup();
            itemListingToEdit.StartDate = new DateTime(2525, 5, 28, 10, 30, 0);
            itemListingToEdit.EndDate = new DateTime(2015, 5, 28, 8, 30, 0);
            itemListingToTest = itemListingToEdit;

            listResult actual = pMgr.EditItemListing(itemListingToEdit, itemListingToTest);

            Setup();
            Cleanup();

            Assert.AreEqual(listResult.StartEndDateError, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/27/2015
        /// Tests attempting to update an ItemListing record with a past date
        /// </summary>
        [TestMethod]
        public void EditItemListing_DateInPast()
        {
            Setup();

            EditSetup();
            itemListingToEdit.StartDate = new DateTime(2014, 5, 28, 10, 30, 0);
            itemListingToEdit.EndDate = new DateTime(2015, 5, 28, 8, 30, 0);
            itemListingToTest = itemListingToEdit;

            listResult actual = pMgr.EditItemListing(itemListingToEdit, itemListingToTest);

            Setup();
            Cleanup();

            Assert.AreEqual(listResult.DateInPast, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/24/2015
        /// Tests attempting to update a nonexistent ItemListing record
        /// </summary>
        [TestMethod]
        public void EditItemListing_NotChanged()
        {
            Setup();

            EditSetup();
            itemListingToTest.ItemListID = 99;

            listResult actual = pMgr.EditItemListing(itemListingToEdit, itemListingToTest);

            Cleanup();

            Assert.AreEqual(listResult.NotChanged, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/24/2015
        /// Tests marking a valid ItemListing record as inactive
        /// </summary>
        [TestMethod]
        public void ArchiveItemListing_ValidItemListing()
        {
            Setup();

            SupplierAccessor.AddSupplier(testSupp, "Test", "Password#1");
            modSupp = getSupplierListCompName(suppList);
            itemListingToTest.SupplierID = modSupp.SupplierID;
            ItemListingAccessor.AddItemListing(itemListingToTest);
            itemListingToTest = getItemListingTestObject(itemList);

            listResult actual = pMgr.ArchiveItemListing(itemListingToTest);

            Cleanup();

            Assert.AreEqual(listResult.Success, actual);
        }

        /// <summary>
        /// Bryan Hurst 4/24/2015
        /// Tests marking a nonexistent ItemListing record as inactive
        /// </summary>
        [TestMethod]
        public void ArchiveItemListing_InvalidItemListing()
        {
            Setup();
            itemListingToTest.ItemListID = 99;

            listResult actual = pMgr.ArchiveItemListing(itemListingToTest);

            Assert.AreEqual(listResult.NotChanged, actual);
        }
    }
}