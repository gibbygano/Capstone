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
        public List<Event> RetrieveEvent(string eventItemID)
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
        
    }
}
