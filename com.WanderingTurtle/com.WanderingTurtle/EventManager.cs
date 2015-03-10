using System.Collections.Generic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.BusinessLogic
{
    public class EventManager
    {
        public EventManager()
        {
            //default constructor
        }

        //Retrieve a single Event object from the Data Access layer with an eventItemID
        //Created by Matt Lapka 1/31/15
        public Event RetrieveEvent (string eventItemID)
        {
			return EventAccessor.GetEvent(eventItemID); 
        }

        //Retrieve a list of active Event objects from the Data Access layer with
        //Created by Matt Lapka 1/31/15
        public List<Event> RetrieveEventList()
        {
			return EventAccessor.GetEventList();
        }

        //Add a single Event object
        //Created by Matt Lapka 1/31/15
        public int AddNewEvent(Event newEvent)
        {
            return EventAccessor.AddEvent(newEvent);
        }

        //Edit an Event object
        //Created by Matt Lapka 1/31/15
        public int EditEvent(Event oldEvent, Event newEvent)
        {
            return EventAccessor.UpdateEvent(oldEvent, newEvent);
        }

        //"Delete" a single Event object (make inactive)
        //Created by Matt Lapka 1/31/15
        public int ArchiveAnEvent(Event eventToDelete)
        {
            return EventAccessor.DeleteEventItem(eventToDelete);
        }
        

        /// <summary>
        /// Retrieve a single EventType object from the Data Access layer with an eventTypeID
        /// Created by Matt Lapka 2/8/15
        /// </summary>
        public EventType RetrieveEventType(string eventTypeID)
        {
			return EventTypeAccessor.GetEventType(eventTypeID);
        }

        ///Retrieve a list of active EventType objects from the Data Access layer
        ///Created by Matt Lapka 2/8/15
        public List<EventType> RetrieveEventTypeList()
        {
            return EventTypeAccessor.GetEventTypeList();
        }

        ///Add a single EventType object
        ///Created by Matt Lapka 2/8/15
        public int AddNewEventType(EventType newEventType)
        {
            return EventTypeAccessor.AddEventType(newEventType);
        }

        //Edit an EventType object
        //Created by Matt Lapka 2/8/15
        public int EditEventType(EventType oldEventType, EventType newEventType)
        {
            return EventTypeAccessor.UpdateEventType(oldEventType, newEventType);
        }

        //"Delete" a single EventType object (make inactive)
        //Created by Matt Lapka 2/8/15
        public int ArchiveAnEventType(EventType eventTypeToDelete)
        {
            return EventTypeAccessor.DeleteEventType(eventTypeToDelete);
        }
    }
}
