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

        private Event toTest = new Event();
        private Event toTest2 = new Event();
        private EventManager myMan = new EventManager();

        private void setup()
        {
            toTest.EventItemID = 999;
            toTest.EventItemName = "A Test Event";
            toTest.EventTypeName = "Boat Ride";
            toTest.EventTypeID = 100;
            toTest.OnSite = false;
            toTest.Active = true;
            toTest.Description = "This is a test descrip";
            toTest.Transportation = false;
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
            //arrange

            EventManager myManager = new EventManager();
            ApplicationException expected = new ApplicationException("Event not found.");

            // act
            List<Event> actual = myManager.RetrieveEventList();

            //assert
            Assert.IsNotNull(actual);

        }


        [TestMethod]
        public void EventSearch_Test()
        {
            //arrange
            String inSearch = "12345Test";
            EventManager myManager = new EventManager();
            Event myEvent = new Event { EventItemID = 112, EventItemName = "12345Test", EventTypeName = "Boat Ride", EventTypeID = 100, OnSite = false, Active = true, Description = "A really creepy midnight boat ride down the river.", Transportation = false };
            List<Event> expected = new List<Event>();
            expected.Add(myEvent);

            //update cache
            DataCache._currentEventList = EventAccessor.GetEventList();
            DataCache._EventListTime = DateTime.Now;
            // act
            List<Event> myTempList = new List<Event>();
            myTempList = myManager.EventSearch(inSearch);
            Event[] myArray = myTempList.ToArray();



            //currentEvent.EventItemID = reader.GetInt32(0);
            //currentEvent.EventItemName = reader.GetString(1);
            //currentEvent.EventTypeID = reader.GetInt32(2);
            //currentEvent.OnSite = reader.GetBoolean(3);
            //currentEvent.Transportation = reader.GetBoolean(4);
            //currentEvent.Description = reader.GetString(5);
            //currentEvent.Active = reader.GetBoolean(6);
            //currentEvent.EventTypeName = reader.GetString(7);
            //EventList.Add(currentEvent);


            //assert
            Assert.AreEqual(myEvent.Active, myArray[0].Active, "Active do not match");
            Assert.AreEqual(myEvent.Description, myArray[0].Description, "Description do not match");

            Assert.AreEqual(myEvent.EventItemName, myArray[0].EventItemName, "Event ItemName do not match");
            Assert.AreEqual(myEvent.EventTypeID, myArray[0].EventTypeID, "EventTypeID do not match");
            Assert.AreEqual(myEvent.EventTypeName, myArray[0].EventTypeName, "EventTypeName do not match");
            Assert.AreEqual(myEvent.OnSite, myArray[0].OnSite, "OnSite do not match");
            Assert.AreEqual(myEvent.OnSiteString, myArray[0].OnSiteString, "OnSiteString do not match");
            Assert.AreEqual(myEvent.ProductID, myArray[0].ProductID, "ProductID do not match");
            Assert.AreEqual(myEvent.Transportation, myArray[0].Transportation, "Transportation does not match");
            Assert.AreEqual(myEvent.TransportString, myArray[0].TransportString, "TransportationString does not match");
            Assert.AreEqual(myEvent.EventItemID, myArray[0].EventItemID, "EventItemID do not match");           //can fail until we can force an EventItemID
        }
        [TestMethod]
        public void EventRetrieve_Test()
        {
            //arrange
            String EventID = "101";
            EventManager myManager = new EventManager();

            Event expected = new Event { EventItemID = 112, EventItemName = "12345Test", EventTypeName = "Boat Ride", EventTypeID = 100, OnSite = false, Active = true, Description = "A really creepy midnight boat ride down the river.", Transportation = false };

            var result = myManager.RetrieveEvent(EventID);

            Assert.AreEqual(expected, result, "objects do not match");
        }

        [TestMethod]
        public void EditEvent_Test()
        {

            EventManager myManager = new EventManager();
            Event preChange = new Event { EventItemID = 112, EventItemName = "12345Test", EventTypeName = "Boat Ride", EventTypeID = 100, OnSite = false, Active = true, Description = "A really creepy midnight boat ride down the river.", Transportation = false };
            Event expected = new Event { EventItemID = 112, EventItemName = "12345TestWasChanged", EventTypeName = "Boat Ride", EventTypeID = 100, OnSite = false, Active = true, Description = "A really creepy midnight boat ride down the river.", Transportation = false };


            var result = myManager.EditEvent(preChange, expected);


            Assert.AreEqual(EventManager.EventResult.Success, result, "method failed");
        }

        [TestMethod]
        public void AddNewEventType_Test()
        {
            EventType myEventType = new EventType { EventName = "boo boo", EventTypeID = 102 };
            EventManager myManager = new EventManager();
            var result = myManager.AddNewEventType(myEventType);


            Assert.AreEqual(EventManager.EventResult.Success, result, "method failed");
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
        [TestMethod]
        public void RetrieveEventTypeList_Test()
        {
            EventManager myManager = new EventManager();
            var result = myManager.RetrieveEventTypeList();

            Assert.IsNotNull(result, "result is null");
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