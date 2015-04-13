using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Created by Rose Steffensmeier 2015/04/09
    /// Tests for different things in the HotelGuestManager
    /// </summary>
    [TestClass]
    public class HotelGuestManagerTest
    {
        HotelManagerAccessorTest setup = new HotelManagerAccessorTest();
        HotelGuestManager access = new HotelGuestManager();

        /*
        [TestInitialize]
        public void initialize()
        {
            setup.initialize();
        }

        [TestMethod]
        public void HotelManagerAdd()
        {
            bool changed = access.AddHotelGuest(new HotelGuest("Fake", "Person", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", 234234234, true));
            Assert.IsTrue(changed);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void HotelManagerAddFail()
        {
            access.AddHotelGuest(new HotelGuest("Fake", "Guest", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", 000, true));
        }
        */
        [TestMethod]
        public void HotelManagerGetList()
        {
            List<HotelGuest> list = access.GetHotelGuestList();
            Assert.AreEqual(0, (int)list[0].HotelGuestID);
        }

        [TestMethod]
        public void HotelManagerGet()
        {
            HotelGuest guest = access.GetHotelGuest(0);
            Assert.AreEqual(0, guest.HotelGuestID);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void HotelManagerGetFail()
        {
            HotelGuest guest = access.GetHotelGuest(-1);
        }
        /*
        [TestMethod]
        public void HotelManagerUpdate()
        {
            List<HotelGuest> guest = access.GetHotelGuestList();
            guest.Add(new HotelGuest((int)guest[guest.Count - 1].HotelGuestID, guest[guest.Count - 1].FirstName, guest[guest.Count - 1].LastName, guest[guest.Count - 1].Address1, guest[guest.Count - 1].Address2, guest[guest.Count - 1].CityState, guest[guest.Count - 1].PhoneNumber, guest[guest.Count - 1].EmailAddress, guest[guest.Count - 1].Room, false));
            bool changed = access.UpdateHotelGuest(guest[guest.Count - 2], guest[guest.Count - 1]);
            Assert.IsTrue(changed);
        }
        */
        [TestMethod]
        public void HotelManagerArchive()
        {
            List<HotelGuest> guest = access.GetHotelGuestList();
            bool changed = access.ArchiveHotelGuest(guest[guest.Count - 1], false);
            Assert.IsTrue(changed);
        }

        [TestCleanup]
        public void cleanup()
        {
            setup.cleanup();
        }
    }
}
