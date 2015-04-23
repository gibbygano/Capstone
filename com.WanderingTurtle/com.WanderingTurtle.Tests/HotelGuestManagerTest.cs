using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Created by Rose Steffensmeier 2015/04/09, 
    /// Updated by Tony Noel- 2015/04/20, 2015/04/22 - All tests passing as of 04/22
    /// Updates: Fixed methods that were not working, updated tests to match the manager methods in the HotelGuestManager, and commented each test method
    /// Tests for different things in the HotelGuestManager
    /// </summary>
    [TestClass]
    public class HotelGuestManagerTest
    {
        private HotelManagerAccessorTest setup = new HotelManagerAccessorTest();
        private HotelGuestManager access = new HotelGuestManager();
        HotelGuest TestGuest;
        
        [TestInitialize]
        public void initialize()
        {
            //Contructs the HotelGuest object to be used for all tests.
            TestGuest = new HotelGuest("Fake", "Person", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", "234234", "3453", true);
        }

        [TestMethod]
        public void HotelManagerAdd()
        {
            //Assigns a Results Edit object after attempting to add the TestGuest into the database
            ResultsEdit changed = access.AddHotelGuest(TestGuest);
            //Asserts that the add will be successful.
            Assert.AreEqual(ResultsEdit.Success, changed);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void HotelManagerAddFail()
        {
            access.AddHotelGuest(TestGuest);
            access.AddHotelGuest(TestGuest);
        }

        [TestMethod]
        public void HotelManagerGet()
        {
            HotelGuest guest = access.GetHotelGuest(100);
            Assert.AreEqual(100, guest.HotelGuestID);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void HotelManagerGetFail()
        {
            HotelGuest guest = access.GetHotelGuest(-1);
        }

        [TestMethod]
        public void HotelManagerUpdate()
        {
            ResultsEdit changed = access.AddHotelGuest(TestGuest);
            //locates the fake record ID
            int guestID = TestCleanupAccessor.GetHotelGuest();
            //pulls from real manager
            HotelGuest guest = access.GetHotelGuest(guestID);
            //assigns a new value in guest2
            HotelGuest guest2 = new HotelGuest(guest.FirstName, "Individual", guest.Address1, guest.Address2, guest.CityState, guest.PhoneNumber, guest.EmailAddress, guest.Room, guest.GuestPIN, guest.Active);
            //calls to manager to complete update
            ResultsEdit edited = access.UpdateHotelGuest(guest, guest2);
            Assert.AreEqual(ResultsEdit.Success, edited);
        }


        [TestMethod]
        public void HotelManagerArchive()
        {
            ResultsEdit changed = access.AddHotelGuest(TestGuest);
            //locates the fake record ID
            int guestID = TestCleanupAccessor.GetHotelGuest();
            //pulls from real manager
            HotelGuest guest = access.GetHotelGuest(guestID);
            //archives the guest using the manager method
            bool archived = access.ArchiveHotelGuest(guest, false);
            //asserts that the test will pass
            Assert.IsTrue(archived);
        }

        [TestCleanup]
        public void cleanup()
        {
            setup.cleanup();
        }
    }
}