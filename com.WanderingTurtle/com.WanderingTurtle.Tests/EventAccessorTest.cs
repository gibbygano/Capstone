using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;
using System.Data;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Bryan Hurst
    /// Created 4/2/2015
    /// 
    /// Unit tests for the methods in Event Accessor
    /// </summary>
    [TestClass]
    public class EventAccessorTest
    {

        Event eventToTest = new Event();
        Event eventToEdit = new Event();
        List<Event> list = new List<Event>();

        /// <summary>
        /// Sets up our events to use in unit tests.
        /// </summary>
        public void setup()
        {
            eventToTest.EventItemID = 0;
            eventToTest.EventItemName = "TEST";
            eventToTest.Transportation = false;
            eventToTest.EventTypeID = 100;
            eventToTest.OnSite = true;
            eventToTest.ProductID = 100;
            eventToTest.Description = "dsa";
            eventToTest.Active = true;

            eventToEdit.EventItemID = 0;
            eventToEdit.EventItemName = "TEST2";
            eventToEdit.Transportation = false;
            eventToEdit.EventTypeID = 100;
            eventToEdit.OnSite = true;
            eventToEdit.ProductID = 100;
            eventToEdit.Description = "dsa";
            eventToEdit.Active = true;
        }

        /// <summary>
        /// Tests adding events to the database
        /// from the eventAccessor
        /// </summary>
        [TestMethod]
        public void AddEvent_ValidEvent()
        {
            int expected = 1;
            setup();

            int actual = EventAccessor.AddEvent(eventToTest);

            EventAccessor.DeleteEventTestItem(eventToTest);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests updating event via the eventAccessor
        /// </summary>
        [TestMethod]
        public void UpdateEvent_ValidEvent()
        {
            int expected = 1;

            setup();
            EventAccessor.AddEvent(eventToTest);
            eventToTest = getEventObjectID(list);

            int actual = EventAccessor.UpdateEvent(eventToTest, eventToEdit);

            EventAccessor.DeleteEventTestItem(eventToEdit);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Method for retrieveing the test record created by theses methods
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private Event getEventObjectID(List<Event> list)
        {
            list = EventAccessor.GetEventList();
            foreach (Event item in list)
            {
                if (item.EventItemName.Equals("TEST"))
                {
                    return item;
                }
            }
            return new Event();
        }

        /// <summary>
        /// Tests an attempt to update an event
        /// that does not exist in the database.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateEvent_InvalidEvent()
        {
            setup();
            eventToTest.EventItemID = 9999;
            EventAccessor.UpdateEvent(eventToTest, eventToEdit);
        }

        /// <summary>
        /// Tests the archiving (deletion) of a 
        /// valid event in the database.
        /// </summary>
        [TestMethod]
        public void DeleteEvent_ValidEvent()
        {
            int expected = 1;
            setup();
            EventAccessor.AddEvent(eventToTest);
            eventToTest = getEventObjectID(list);
            int actual = EventAccessor.DeleteEventItem(eventToTest);
            EventAccessor.DeleteEventTestItem(eventToTest);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Tests the archiving (deletion) of 
        /// an invalid event in the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void DeleteEvent_InvalidEvent()
        {
            int expected = 1;
            setup();
            eventToEdit.EventItemID = 9999;
            int actual = EventAccessor.DeleteEventItem(eventToEdit);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test retrieving a single record from the EventItem database
        /// </summary>
        [TestMethod]
        public void GetEvent_Valid()
        {
            setup();

            string expected = eventToTest.EventItemName;

            EventAccessor.AddEvent(eventToTest);
            eventToEdit = getEventObjectID(list);

            Event actual = EventAccessor.GetEvent(eventToEdit.EventItemID.ToString());

            EventAccessor.DeleteEventTestItem(eventToTest);

            Assert.AreEqual(expected, actual.EventItemName);
        }

        /// <summary>
        /// Test retrieving a single nonexistent record from the EventItem database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void GetEvent_Invalid()
        {
            setup();
            eventToEdit.EventItemID = 9999;

            Event actual = EventAccessor.GetEvent(eventToEdit.EventItemID.ToString());
        }
    }
}