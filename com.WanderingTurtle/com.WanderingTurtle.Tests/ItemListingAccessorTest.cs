using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Bryan Hurst
    /// Created 4/2/2015
    /// 
    /// Summary description for ItemListingAccessorTest
    /// </summary>
    [TestClass]
    public class ItemListingAccessorTest
    {
        SupplierLoginAccessor sLA = new SupplierLoginAccessor();
        Supplier testSupp = new Supplier();
        Supplier modSupp = new Supplier();
        SupplierLogin testLog = new SupplierLogin();
        ItemListing itemListingToTest = new ItemListing();
        ItemListing itemListingToEdit = new ItemListing();
        List<ItemListing> itemList = new List<ItemListing>();
        List<Supplier> suppList = new List<Supplier>();

        /// <summary>
        /// Setup method for ItemListingAccessorTest unit tests
        /// </summary>
        public void setup()
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
            itemListingToEdit.Price = 222;
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

        /// <summary>
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
        /// Tests adding a valid ItemListing object
        /// </summary>
        [TestMethod]
        public void AddItemListing_ValidItemListing()
        {
            int expected = 1;
            setup();

            SupplierAccessor.AddSupplier(testSupp, "Test", "Password#1");
            modSupp = getSupplierListCompName(suppList);
            itemListingToTest.SupplierID = modSupp.SupplierID;

            int actual = ItemListingAccessor.AddItemListing(itemListingToTest);

            ItemListingAccessor.DeleteItemListingTestItem(itemListingToTest);
            testSupp.SupplierID = modSupp.SupplierID;
            testLog = sLA.RetrieveSupplierLogin("Password#1", "Test");
            SupplierLoginAccessor.DeleteTestSupplierLogin(testLog);
            TestCleanupAccessor.DeleteTestSupplier(testSupp);
                    
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests updating a valid ItemListing record
        /// </summary>
        [TestMethod]
        public void UpdateItemListing_ValidItemListing()
        {
            int expected = 1;
            setup();

            SupplierAccessor.AddSupplier(testSupp, "Test", "Password#1");
            modSupp = getSupplierListCompName(suppList);
            itemListingToEdit.SupplierID = modSupp.SupplierID;
            ItemListingAccessor.AddItemListing(itemListingToTest);
            itemListingToTest = getItemListingTestObject(itemList);
            itemListingToEdit.SupplierID = modSupp.SupplierID;

            int actual = ItemListingAccessor.UpdateItemListing(itemListingToEdit, itemListingToTest);

            ItemListingAccessor.DeleteItemListingTestItem(itemListingToEdit);
            testSupp.SupplierID = modSupp.SupplierID;
            testLog = sLA.RetrieveSupplierLogin("Password#1", "Test");
            SupplierLoginAccessor.DeleteTestSupplierLogin(testLog);
            TestCleanupAccessor.DeleteTestSupplier(testSupp);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests attempting to update a nonexistent ItemListing record
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateItemListing_InvalidItemListing()
        {
            setup();
            itemListingToTest.ItemListID = 99;

            int actual = ItemListingAccessor.UpdateItemListing(itemListingToEdit, itemListingToTest);

            if (actual == 0)
            {
                throw new ApplicationException("Concurrency Error");
            }
        }

        /// <summary>
        /// Tests marking a valid ItemListing record as inactive
        /// </summary>
        [TestMethod]
        public void DeleteItemListing_ValidItemListing()
        {
            int expected = 1;
            setup();

            SupplierAccessor.AddSupplier(testSupp, "Test", "Password#1");
            modSupp = getSupplierListCompName(suppList);
            itemListingToTest.SupplierID = modSupp.SupplierID;
            ItemListingAccessor.AddItemListing(itemListingToTest);
            itemListingToTest = getItemListingTestObject(itemList);

            int actual = ItemListingAccessor.DeleteItemListing(itemListingToTest);

            ItemListingAccessor.DeleteItemListingTestItem(itemListingToTest);
            testSupp.SupplierID = modSupp.SupplierID;
            testLog = sLA.RetrieveSupplierLogin("Password#1", "Test");
            SupplierLoginAccessor.DeleteTestSupplierLogin(testLog);
            TestCleanupAccessor.DeleteTestSupplier(testSupp);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests marking a nonexistent ItemListing record as inactive
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void DeleteItemListing_InvalidItemListing()
        {
            setup();
            itemListingToTest.ItemListID = 99;

            int actual = ItemListingAccessor.DeleteItemListing(itemListingToTest);

            if (actual == 0)
            {
                throw new ApplicationException("Concurrency Error");
            }
        }
    }
}