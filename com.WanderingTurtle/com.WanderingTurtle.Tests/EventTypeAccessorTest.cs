using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Bryan Hurst 5/2/2015
    /// Unit test for the class EventTypeAccessor
    /// </summary>
    [TestClass]
    public class EventTypeAccessorTest
    {
        EventType eventTypeToTest = new EventType();
        EventType eventTypeToEdit = new EventType();
        List<EventType> list = new List<EventType>();

        /// <summary>
        /// Bryan Hurst 5/2/2015
        /// Setup method for tests
        /// </summary>
        public void setup()
        {
            eventTypeToTest.EventName = "Test";
            eventTypeToTest.EventTypeID = 0;

            eventTypeToEdit.EventName = "Testing";
            eventTypeToEdit.EventTypeID = 0;
        }

        /// <summary>
        /// Method for retrieving the test record created by these methods
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private EventType getEventTypeID(List<EventType> list)
        {
            list = EventTypeAccessor.GetEventTypeList();
            foreach (EventType item in list)
            {
                if (item.EventName.Equals("Test"))
                {
                    return item;
                }
            }
            return new EventType();
        }

        /// <summary>
        /// Bryan Hurst 5/2/2015
        /// Test to add an EventType
        /// </summary>
        [TestMethod]
        public void AddEventType_Valid()
        {
            setup();

            Assert.AreEqual(1, EventTypeAccessor.AddEventType("Test"));

            TestCleanupAccessor.DeleteEventTypeTest(eventTypeToTest);
        }

        /// <summary>
        /// Bryan Hurst 5/2/2015
        /// Test to update a valid event type
        /// </summary>
        [TestMethod]
        public void UpdateEventType_Valid()
        {
            setup();

            EventTypeAccessor.AddEventType("Test");
            eventTypeToTest = getEventTypeID(list);

            Assert.AreEqual(1, EventTypeAccessor.UpdateEventType(eventTypeToTest, eventTypeToEdit));

            TestCleanupAccessor.DeleteEventTypeTest(eventTypeToEdit);
        }

        /// <summary>
        /// Bryan Hurst 5/2/2015
        /// Test to update a non-existent EventType
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateEventType_Invalid()
        {
            setup();
            eventTypeToTest.EventTypeID = 9999;

            Assert.AreEqual(1, EventTypeAccessor.UpdateEventType(eventTypeToTest, eventTypeToEdit));
        }

        /// <summary>
        /// Bryan Hurst 5/2/2015
        /// Test to archive a valid EventType
        /// </summary>
        [TestMethod]
        public void DeleteEventType_Valid()
        {
            setup();

            EventTypeAccessor.AddEventType("Test");
            eventTypeToTest = getEventTypeID(list);

            Assert.AreEqual(1, EventTypeAccessor.DeleteEventType(eventTypeToTest));

            TestCleanupAccessor.DeleteEventTypeTest(eventTypeToTest);
        }

        /// <summary>
        /// Bryan Hurst 5/2/2015
        /// Test to archive a non-existent EventType
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void DeleteEventType_Invalid()
        {
            setup();
            eventTypeToTest.EventTypeID = 9999;

            Assert.AreEqual(1, EventTypeAccessor.DeleteEventType(eventTypeToTest));
        }
    }
}