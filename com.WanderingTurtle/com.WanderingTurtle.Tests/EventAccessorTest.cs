using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Bryan Hurst
    /// Created 4/2/2015
    /// 
    /// Unit tests for the EventAccessor class in Data Access
    /// </summary>
    [TestClass]
    public class EventAccessorTest
    {
        int EventItemID = 0;
        string EventItemName = "asd";
        bool Transportation = false;
        int EventTypeID = 10;
        bool OnSite = true;
        int ProductID = 100;
        string Description = "dsa";
        bool Active = false;
        Event testEvent;

        int newEventItemID = 0;
        string newEventItemName = "dsa";
        bool newTransportation = true;
        int newEventTypeID = 22;
        bool newOnSite = false;
        int newProductID = 333;
        string newDescription = "asd";
        bool newActive = true;

        [TestMethod]
        public void AddEvent_ValidEvent()
        {
            testEvent = new Event(EventItemID, EventItemName, Transportation, EventTypeID, OnSite, ProductID, Description, Active);
            int expected = 1;

            int actual = EventAccessor.AddEvent(testEvent);

            Assert.AreEqual(expected, actual);
        }

        /*
        [TestMethod]
        [ExpectedException (typeof (ApplicationException))]
        public void AddEvent_InvalidEvent()
        {
            int expected = 1;
          
            Event testEvent = new Event(EventItemID, EventItemName, Transportation, EventTypeID, OnSite, ProductID, Description, Active);

            int actual = EventAccessor.AddEvent(testEvent);

            Assert.AreEqual(expected, actual);
        }*/

        [TestMethod]
        public void UpdateEvent_ValidEvent()
        {
            int expected = 1;

            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "SELECT EventItemID FROM EventItem WHERE EventItemName = '" + EventItemName + "'";
            var cmd = new SqlCommand(cmdText, conn);

            conn.Open();
            newEventItemID = (int)cmd.ExecuteScalar();
            conn.Close();

            Event testEvent = new Event(EventItemID, EventItemName, Transportation, EventTypeID, OnSite, ProductID, Description, Active);
            Event newtestEvent = new Event(newEventItemID, newEventItemName, newTransportation, newEventTypeID, newOnSite, newProductID, newDescription, newActive);

            int actual = EventAccessor.UpdateEvent(testEvent, newtestEvent);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException (typeof (ApplicationException))]
        public void UpdateEvent_InvalidEvent()
        {
            int oldEventItemID = 9999;
            string oldEventItemName = "asd";
            bool oldTransportation = false;
            int oldEventTypeID = 10;
            bool oldOnSite = true;
            int oldProductID = 100;
            string oldDescription = "dsa";
            bool oldActive = false;

            Event testEvent = new Event(oldEventItemID, oldEventItemName, oldTransportation, oldEventTypeID, oldOnSite, oldProductID, oldDescription, oldActive);
            Event newtestEvent = new Event(newEventItemID, newEventItemName, newTransportation, newEventTypeID, newOnSite, newProductID, newDescription, newActive);

            int actual = EventAccessor.UpdateEvent(testEvent, newtestEvent);
        }

        [TestMethod]
        public void DeleteEvent_ValidEvent()
        {
            int expected = 1;

            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "SELECT EventItemID FROM EventItem WHERE EventItemName = '" + newEventItemName + "'";
            var cmd = new SqlCommand(cmdText, conn);

            conn.Open();
            newEventItemID = (int)cmd.ExecuteScalar();
            conn.Close();

            Event testEvent = new Event(newEventItemID, newEventItemName, newTransportation, newEventTypeID, newOnSite, newProductID, newDescription, newActive);

            int actual = EventAccessor.DeleteEventItem(testEvent);

            EventAccessor.DeleteEventTestItem(testEvent);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void DeleteEvent_InvalidEvent()
        {
            int expected = 1;

            Event testEvent = new Event(newEventItemID, newEventItemName, newTransportation, newEventTypeID, newOnSite, newProductID, newDescription, newActive);

            int actual = EventAccessor.DeleteEventItem(testEvent);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof (ApplicationException))]
        public void GetEventList_DBMissing()
        {
            EventAccessor.GetEventList();
        }
    }
}