using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.Tests
{
    ///Justin Pennington 4/9/15
    /// <summary>
    /// Summary description for EventManagerTest
    /// </summary>
    [TestClass]
    public class EventManagerTest
    {

        private Event toTest            = new Event();
        private Event toTest2           = new Event();
        private EventManager myMan      = new EventManager();
        private EventType typeToTest    = new EventType();

        private void setup()
        {
            toTest.EventItemID      = 999;
            toTest.EventItemName    = "A Test Event";
            toTest.EventTypeName    = "Boat Ride";
            toTest.EventTypeID      = 100;
            toTest.OnSite           = false;
            toTest.Active           = true;
            toTest.Description      = "This is a test descrip";
            toTest.Transportation   = false;

            toTest2.EventItemID     = 999;
            toTest2.EventItemName   = "A Test Event";
            toTest2.EventTypeName   = "Boat Ride";
            toTest2.EventTypeID     = 100;
            toTest2.OnSite          = false;
            toTest2.Active          = true;
            toTest2.Description     = "This is a test descrip";
            toTest2.Transportation  = true;
        }

        private void toTestUpdate()
        {
            var myList = myMan.RetrieveEventList();
            foreach (var item in myList)
            {
                if (item.Description == "This is a test descrip")
                {
                    toTest = item;
                }
            }
        }


        /// <summary>
        /// Tests adding correct event
        /// </summary>
        [TestMethod]
        public void AddEventWorking_Test()
        {
            setup();
            Assert.AreEqual(myMan.AddNewEvent(toTest), EventManager.EventResult.Success);
            myMan.deleteTestEvent(toTest);
        }

        [TestMethod]
        public void RetrieveEventList_Test()
        {
            List<Event> actual = myMan.RetrieveEventList();
            Assert.IsNotNull(actual);
        }

     
        [TestMethod]
        public void EventSearch_Test()
        {
            //arrange
            setup();
            String inSearch = "Boat";
            //myMan.AddNewEvent(toTest);
            List<Event> expected = new List<Event>();
            expected.Add(toTest);

            //update cache
            DataCache._currentEventList = EventAccessor.GetEventList();
            DataCache._EventListTime = DateTime.Now;
            // act
            List<Event> myTempList = new List<Event>();
            myTempList = myMan.EventSearch(inSearch);
            Event[] myArray = myTempList.ToArray();
            
            //assert
            Assert.IsNotNull(myArray[0].EventItemName);
        }
         

        /// <summary>
        /// Tests Retrieving an event from the database
        /// Written by: Hunter Lind
        /// </summary>
        [TestMethod]
        public void EventRetrieve_Test()
        {
            setup();
            myMan.AddNewEvent(toTest);
            toTestUpdate();

            var expected = toTest;
            var result = myMan.RetrieveEvent(toTest.EventItemID.ToString());

            Assert.AreEqual(expected, result, "objects do not match");
            myMan.deleteTestEvent(toTest);
        }

        /// <summary>
        /// Tests editing an event.
        /// Written by: Hunter Lind
        /// </summary>
        [TestMethod]
        public void EditEvent_Test()
        {
            setup();
            myMan.AddNewEvent(toTest);
            toTestUpdate();
            Assert.AreEqual(EventManager.EventResult.Success, myMan.EditEvent(toTest, toTest2), "method failed");
            myMan.deleteTestEvent(toTest);
        }

        /*
        [TestMethod]
        public void AddNewEventType_Test()
        {
            typeToTest.EventName = "boo boo";
            typeToTest.EventTypeID = 107;

            Assert.AreEqual(EventManager.EventResult.Success, myMan.AddNewEventType(typeToTest), "method failed");
        }

        [TestMethod]
        public void EditEventType_Test()
        {
            EventType myEventType = new EventType { EventName = "boo boo", EventTypeID = 102 };
            EventType newEventType = new EventType { EventName = "new new", EventTypeID = 102 };
            EventManager myManager = new EventManager();
            var result = myManager.EditEventType(myEventType, newEventType);

            Assert.AreEqual(EventManager.EventResult.Success, result, "failed");
        }
        [TestMethod]
        public void ArchiveAnEventType_Test()
        {
            EventType newEventType = new EventType { EventName = "new new", EventTypeID = 102 };
            EventManager myManager = new EventManager();
            var result = myManager.ArchiveAnEventType(newEventType);

            Assert.AreEqual(EventManager.EventResult.Success, result, "failed");
        }
         * */
        /// <summary>
        /// Tests retrieving an EventTypeList
        /// Written by: Hunter Lind
        /// </summary>
        [TestMethod]
        public void RetrieveEventTypeList_Test()
        {
            Assert.IsNotNull(myMan.RetrieveEventTypeList(), "result is null");
        }
        [TestMethod]
        public void RetrieveEventType_Test()
        {
            string eventTypeID = "100";
            EventType expected = new EventType { EventTypeID = 100, EventName = "Boat Ride" };
            EventManager myManager = new EventManager();

            var result = myManager.RetrieveEventType(eventTypeID);

            Assert.AreEqual(expected.EventName, result.EventName, "EventName do not match");
            Assert.AreEqual(expected.EventTypeID, result.EventTypeID, "EventTypeID does not match");
        }
    }
}