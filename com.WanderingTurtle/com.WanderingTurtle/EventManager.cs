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

        public EventManager()
        {
            //default constructor
        }

        //Retrieve a single Event object from the Data Access layer with an eventItemID
        //Created by Matt Lapka 1/31/15
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
                else
                {
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
                        Event currentEvent = list.Where(e => e.EventItemID.ToString() == eventItemID).FirstOrDefault();
                        if (currentEvent != null)
                        {
                            return currentEvent;
                        }
                        else
                        {
                            throw new ApplicationException("Event not found.");
                        }
                    }
                }
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
                else
                {
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
                    else
                    {
                        return DataCache._currentEventList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Add a single Event object
        //Created by Matt Lapka 1/31/15
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
                else
                {
                    return EventResult.NotAdded;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return EventResult.ChangedByOtherUser;
                }
                return EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        //Edit an Event object
        //Created by Matt Lapka 1/31/15
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
                else
                {
                    return EventResult.NotChanged;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return EventResult.ChangedByOtherUser;
                }
                return EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        //"Delete" a single Event object (make inactive)
        //Created by Matt Lapka 1/31/15
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
                else
                {
                    return EventResult.NotChanged;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return EventResult.ChangedByOtherUser;
                }
                return EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        /// <summary>
        /// Retrieve a single EventType object from the Data Access layer with an eventTypeID
        /// Created by Matt Lapka 2/8/15
        /// </summary>
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
                else
                {
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
                        else
                        {
                            throw new ApplicationException("Event not found.");
                        }
                    }
                }
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
                else
                {
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
                    else
                    {
                        return DataCache._currentEventTypeList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///Add a single EventType object
        ///Created by Matt Lapka 2/8/15
        public EventResult AddNewEventType(EventType newEventType)
        {
            try
            {
                if (EventTypeAccessor.AddEventType(newEventType) == 1)
                {
                    //refresh cache
                    DataCache._currentEventTypeList = EventTypeAccessor.GetEventTypeList();
                    DataCache._EventTypeListTime = DateTime.Now;
                    return EventResult.Success;
                }
                else
                {
                    return EventResult.NotAdded;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return EventResult.ChangedByOtherUser;
                }
                return EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        //Edit an EventType object
        //Created by Matt Lapka 2/8/15
        public EventResult EditEventType(EventType oldEventType, EventType newEventType)
        {
            try
            {
                if (EventTypeAccessor.UpdateEventType(oldEventType, newEventType) == 1)
                {
                    //update cache
                    DataCache._currentEventTypeList = EventTypeAccessor.GetEventTypeList();
                    DataCache._EventTypeListTime = DateTime.Now;
                    return EventResult.Success;
                }
                else
                {
                    return EventResult.NotChanged;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return EventResult.ChangedByOtherUser;
                }
                return EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

        //"Delete" a single EventType object (make inactive)
        //Created by Matt Lapka 2/8/15
        public EventResult ArchiveAnEventType(EventType eventTypeToDelete)
        {
            try
            {
                if (EventTypeAccessor.DeleteEventType(eventTypeToDelete) == 1)
                {
                    //update cache
                    DataCache._currentEventTypeList = EventTypeAccessor.GetEventTypeList();
                    DataCache._EventTypeListTime = DateTime.Now;
                    return EventResult.Success;
                }
                else
                {
                    return EventResult.NotChanged;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return EventResult.ChangedByOtherUser;
                }
                return EventResult.DatabaseError;
            }
            catch (Exception)
            {
                return EventResult.DatabaseError;
            }
        }

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
            else
            {
                return DataCache._currentEventList;
            }
        }

        public int deleteTestEvent(Event testEvent)
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