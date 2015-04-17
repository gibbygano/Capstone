using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        private int EventItemID = 0;
        private string EventItemName = "asd";
        private bool Transportation = false;
        private int EventTypeID = 10;
        private bool OnSite = true;
        private int ProductID = 100;
        private string Description = "dsa";
        private bool Active = true;
        private Event testEvent;

        private int newEventItemID = 0;
        private string newEventItemName = "dsa";
        private bool newTransportation = true;
        private int newEventTypeID = 22;
        private bool newOnSite = false;
        private int newProductID = 333;
        private string newDescription = "asd";
        private bool newActive = true;

        [TestMethod]
        public void AddEvent_ValidEvent()
        {
            int expected = 1;
            testEvent = new Event(EventItemID, EventItemName, Transportation, EventTypeID, OnSite, ProductID, Description, Active);

            int actual = EventAccessor.AddEvent(testEvent);

            EventAccessor.DeleteEventTestItem(testEvent);

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
            Event testEvent = new Event(EventItemID, EventItemName, Transportation, EventTypeID, OnSite, ProductID, Description, Active);
            EventAccessor.AddEvent(testEvent);

            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "SELECT EventItemID FROM EventItem WHERE EventItemName = '" + EventItemName + "'";
            var cmd = new SqlCommand(cmdText, conn);

            conn.Open();
            newEventItemID = (int)cmd.ExecuteScalar();
            conn.Close();

            Event newtestEvent = new Event(newEventItemID, newEventItemName, newTransportation, newEventTypeID, newOnSite, newProductID, newDescription, newActive);

            int actual = EventAccessor.UpdateEvent(testEvent, newtestEvent);

            EventAccessor.DeleteEventTestItem(newtestEvent);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateEvent_InvalidEvent()
        {
            int oldEventItemID = 9999;
            string oldEventItemName = "asd";
            bool oldTransportation = false;
            int oldEventTypeID = 10;
            bool oldOnSite = true;
            int oldProductID = 100;
            string oldDescription = "dsa";
            bool oldActive = true;

            Event testEvent = new Event(oldEventItemID, oldEventItemName, oldTransportation, oldEventTypeID, oldOnSite, oldProductID, oldDescription, oldActive);
            Event newtestEvent = new Event(newEventItemID, newEventItemName, newTransportation, newEventTypeID, newOnSite, newProductID, newDescription, newActive);

            int actual = EventAccessor.UpdateEvent(testEvent, newtestEvent);
        }

        [TestMethod]
        public void DeleteEvent_ValidEvent()
        {
            int expected = 1;
            Event testEvent = new Event(EventItemID, EventItemName, Transportation, EventTypeID, OnSite, ProductID, Description, Active);
            EventAccessor.AddEvent(testEvent);

            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "SELECT EventItemID FROM EventItem WHERE EventItemName = '" + EventItemName + "'";
            var cmd = new SqlCommand(cmdText, conn);

            conn.Open();
            newEventItemID = (int)cmd.ExecuteScalar();
            conn.Close();

            Event newTestEvent = new Event(newEventItemID, EventItemName, Transportation, EventTypeID, OnSite, ProductID, Description, Active);

            int actual = EventAccessor.DeleteEventItem(newTestEvent);

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

        //[TestMethod]
        //[ExpectedException(typeof (ApplicationException))]
        //public void GetEventList_DBMissing()
        //{
        //    EventAccessor.GetEventList();
        //}
    }
}