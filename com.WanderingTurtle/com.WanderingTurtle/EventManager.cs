using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle
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
            try
            {
                return EventAccessor.GetEvent(eventItemID); 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Retrieve a list of active Event objects from the Data Access layer with
        //Created by Matt Lapka 1/31/15
        public List<Event> RetrieveEventList()
        {
            try
            {
                return EventAccessor.GetEventList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Add a single Event object
        //Created by Matt Lapka 1/31/15
        public int AddNewEvent(Event newEvent)
        {
            try
            {
                return EventAccessor.AddEvent(newEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Edit an Event object
        //Created by Matt Lapka 1/31/15
        public int EditEvent(Event oldEvent, Event newEvent)
        {
            try
            {
                return EventAccessor.UpdateEvent(oldEvent, newEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //"Delete" a single Event object (make inactive)
        //Created by Matt Lapka 1/31/15
        public int ArchiveAnEvent(Event eventToDelete)
        {
            try
            {
                return EventAccessor.DeleteEvent(eventToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        

        /// <summary>
        /// Retrieve a single EventType object from the Data Access layer with an eventTypeID
        /// Created by Matt Lapka 2/8/15
        /// </summary>
        public EventType RetrieveEventType(string eventTypeID)
        {
            try
            {
                return EventTypeAccessor.GetEventType(eventTypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        ///Retrieve a list of active EventType objects from the Data Access layer
        ///Created by Matt Lapka 2/8/15
        public List<EventType> RetrieveEventTypeList()
        {
            try
            {
                return EventTypeAccessor.GetEventTypeList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        ///Add a single EventType object
        ///Created by Matt Lapka 2/8/15
        public int AddNewEventType(EventType newEventType)
        {
            try
            {
                return EventTypeAccessor.AddEventType(newEventType);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Edit an EventType object
        //Created by Matt Lapka 2/8/15
        public int EditEventType(EventType oldEventType, EventType newEventType)
        {
            try
            {
                return EventTypeAccessor.UpdateEventType(oldEventType, newEventType);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //"Delete" a single EventType object (make inactive)
        //Created by Matt Lapka 2/8/15
        public int ArchiveAnEventType(EventType eventTypeToDelete)
        {
            try
            {
                return EventTypeAccessor.DeleteEventType(eventTypeToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
