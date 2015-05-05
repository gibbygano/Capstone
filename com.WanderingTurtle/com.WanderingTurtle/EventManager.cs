using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
    public class EventManager
    {
        public enum EventResult
        {
            //item could not be found
            NotFound = 0,

            //new event could not be added
            NotAdded,

            NotChanged,

            //worked
            Success,

            //Can change record
            OkToEdit,

            //concurrency error
            ChangedByOtherUser,

            DatabaseError,
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/01/31
        /// Default constructorf
        /// </summary>
        public EventManager()
        {
            //default constructor
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/01/31
        /// Retrieve a single Event object from the Data Access layer with an eventItemID
        /// </summary>
        /// <param name="eventItemID"></param>
        /// <returns>the event with the id</returns>
        public Event RetrieveEvent(string eventItemID)
        {
            var now = DateTime.Now;
            double cacheExpirationTime = 5;

            try
            {
                if (DataCache._currentEventList == null)
                {
                    return EventAccessor.GetEvent(eventItemID);
                }
                //check time. If less than 5 min, return event from cache
                if (now > DataCache._EventListTime.AddMinutes(cacheExpirationTime))
                {
                    //get event from DB
                    var currentEvent = EventAccessor.GetEvent(eventItemID);
                    return currentEvent;
                }
                else
                {
                    //get event from cached list
                    var list = DataCache._currentEventList;
                    Event currentEvent = list.FirstOrDefault(e => e.EventItemID.ToString() == eventItemID);
                    if (currentEvent != null)
                    {
                        return currentEvent;
                    }
                    throw new ApplicationException("Event not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Matt Lapka
        /// Created:  2015/01/31
        //  Retrieve a list of active Event objects from the Data Access layer
        /// </summary>
        /// <returns></returns>
        public List<Event> RetrieveEventList()
        {
            double cacheExpirationTime = 5; //how long the cache should live (minutes)
            var now = DateTime.Now;
            try
            {
                if (DataCache._currentEventList == null)
                {
                    //data hasn't been retrieved yet. get data, set it to the cache and return the result.
                    var list = EventAccessor.GetEventList();
                    DataCache._currentEventList = list;
                    DataCache._EventListTime = now;
                    return list;
                }
                //check time. If less than 5 min, return cache

                if (now > DataCache._EventListTime.AddMinutes(cacheExpirationTime))
                {
                    //get new list from DB
                    var list = EventAccessor.GetEventList();
                    //set cache to new list and update time
                    DataCache._currentEventList = list;
                    DataCache._EventListTime = now;
                    return list;
                }
                return DataCache._currentEventList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/01/31
        /// Add a single Event object
        ///</summary>
        ///<returns>result of adding event</returns>
        public EventResult AddNewEvent(Event newEvent)
        {
            try
            {
                if (EventAccessor.AddEvent(newEvent) == 1)
                {
                    //refresh cache
                    DataCache._currentEventList = EventAccessor.GetEventList();
                    DataCache._EventListTime = DateTime.Now;
                    return EventResult.Success;
                }
                return EventResult.NotAdded;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? EventResult.ChangedByOtherUser : EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/01/31
        /// Edit an Event object
        ///</summary>
        ///<returns>result of adding event</returns>
        public EventResult EditEvent(Event oldEvent, Event newEvent)
        {
            try
            {
                if (EventAccessor.UpdateEvent(oldEvent, newEvent) == 1)
                {
                    //update cache
                    DataCache._currentEventList = EventAccessor.GetEventList();
                    DataCache._EventListTime = DateTime.Now;
                    return EventResult.Success;
                }
                return EventResult.NotChanged;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? EventResult.ChangedByOtherUser : EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/01/31
        /// "Delete" a single Event object (make inactive)
        ///</summary>
        ///<returns>result of adding event</returns>
        public EventResult ArchiveAnEvent(Event eventToDelete)
        {
            try
            {
                if (EventAccessor.DeleteEventItem(eventToDelete) == 1)
                {
                    //update cache
                    DataCache._currentEventList = EventAccessor.GetEventList();
                    DataCache._EventListTime = DateTime.Now;
                    return EventResult.Success;
                }
                return EventResult.NotChanged;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? EventResult.ChangedByOtherUser : EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/02/08
        /// Retrieve a single EventType object from the Data Access layer with an eventTypeID
        ///</summary>
        ///<returns>eventType Object</returns>
        public EventType RetrieveEventType(string eventTypeID)
        {
            var now = DateTime.Now;
            double cacheExpirationTime = 10;

            try
            {
                if (DataCache._currentEventTypeList == null)
                {
                    return EventTypeAccessor.GetEventType(eventTypeID);
                }
                //check time. If less than 10 min, return event from cache
                if (now > DataCache._EventTypeListTime.AddMinutes(cacheExpirationTime))
                {
                    //get event from DB
                    var currentEventType = EventTypeAccessor.GetEventType(eventTypeID);
                    return currentEventType;
                }
                else
                {
                    //get event from cached list
                    var list = DataCache._currentEventTypeList;
                    EventType currentEventType = list.Where(e => e.EventTypeID.ToString() == eventTypeID).FirstOrDefault();
                    if (currentEventType != null)
                    {
                        return currentEventType;
                    }
                    throw new ApplicationException("Event not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/02/08
        /// Retrieve a list of active EventType objects from the Data Access layer
        ///</summary>
        ///<returns>list of eventType Object</returns>
        public List<EventType> RetrieveEventTypeList()
        {
            double cacheExpirationTime = 10; //how long the cache should live (minutes)
            var now = DateTime.Now;
            try
            {
                if (DataCache._currentEventTypeList == null)
                {
                    //data hasn't been retrieved yet. get data, set it to the cache and return the result.
                    var list = EventTypeAccessor.GetEventTypeList();
                    DataCache._currentEventTypeList = list;
                    DataCache._EventTypeListTime = now;
                    return list;
                }
                //check time. If less than 5 min, return cache

                if (now > DataCache._EventTypeListTime.AddMinutes(cacheExpirationTime))
                {
                    //get new list from DB
                    var list = EventTypeAccessor.GetEventTypeList();
                    //set cache to new list and update time
                    DataCache._currentEventTypeList = list;
                    DataCache._EventTypeListTime = now;

                    return list;
                }
                return DataCache._currentEventTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/02/08
        /// Add a single EventType object
        ///</summary>
        ///<returns>result of adding the eventtype</returns>
        public EventResult AddNewEventType(string eventName)
        {
            try
            {
                if (EventTypeAccessor.AddEventType(eventName) == 1)
                {
                    //refresh cache
                    DataCache._currentEventTypeList = EventTypeAccessor.GetEventTypeList();
                    DataCache._EventTypeListTime = DateTime.Now;
                    return EventResult.Success;
                }
                return EventResult.NotAdded;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? EventResult.ChangedByOtherUser : EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/02/08
        /// Edit an EventType object
        ///</summary>
        ///<returns>result of adding the eventtype</returns>
        public EventResult EditEventType(EventType oldEventType, EventType newEventType)
        {
            try
            {
                if (EventTypeAccessor.UpdateEventType(oldEventType, newEventType) == 1)
                {
                    //update cache
                    DataCache._currentEventTypeList = EventTypeAccessor.GetEventTypeList();
                    DataCache._EventTypeListTime = DateTime.Now;
                    DataCache._currentEventList = EventAccessor.GetEventList();
                    DataCache._EventListTime = DateTime.Now;
                    return EventResult.Success;
                }
                return EventResult.NotChanged;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? EventResult.ChangedByOtherUser : EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/02/08
        /// "Delete" a single EventType object (make inactive)
        ///</summary>
        ///<returns>result of adding the eventtype</returns>
        public EventResult ArchiveAnEventType(EventType eventTypeToDelete)
        {
            try
            {
                if (EventTypeAccessor.DeleteEventType(eventTypeToDelete) == 1)
                {
                    //update cache
                    DataCache._currentEventTypeList = EventTypeAccessor.GetEventTypeList();
                    DataCache._EventTypeListTime = DateTime.Now;
                    DataCache._currentEventList = EventAccessor.GetEventList();
                    DataCache._EventListTime = DateTime.Now;
                    return EventResult.Success;
                }
                return EventResult.NotChanged;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? EventResult.ChangedByOtherUser : EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }
        
        /// <summary>
        /// Justin Pennington
        /// Created:  2015/03/27
        /// Searches for a string the user asks for
        /// </summary>
        /// <param name="inSearch"></param>
        /// <returns></returns>
        public List<Event> EventSearch(String inSearch)
        {
            //List<Event> myTempList = new List<Event>();
            if (!inSearch.Equals("") && !inSearch.Equals(null))
            {
                //Lambda Version
                //return myTempList.AddRange(DataCache._currentEventList.Where(s => s.EventItemName.ToUpper().Contains(inSearch.ToUpper())).Select(s => s));
                //LINQ version
                List<Event> myTempList = new List<Event>();
                myTempList.AddRange(
                  from inEvent in DataCache._currentEventList
                  where inEvent.EventItemName.ToUpper().Contains(inSearch.ToUpper())
                  select inEvent);
                return myTempList;

                //Will empty the search list if nothing is found so they will get feedback for typing something incorrectly
            }
            return DataCache._currentEventList;
        }

        /// <summary>
        /// Bryan Hurst
        /// Created: 2015/04/03
        /// Deletes information concerning the test event
        /// </summary>
        /// <param name="testEvent"></param>
        /// <returns></returns>
        public int DeleteTestEvent(Event testEvent)
        {
            try
            {
                return EventAccessor.DeleteEventTestItem(testEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}