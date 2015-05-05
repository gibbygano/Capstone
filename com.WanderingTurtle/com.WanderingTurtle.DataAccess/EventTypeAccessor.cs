using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    public class EventTypeAccessor
    {
        /// <summary>
        /// Justin Pennington 
        /// Created:  2015/02/14
        /// 
        /// Add the event type to the database
        /// </summary>
        /// <param name="newEventType">String containing the name of a new EventType</param>
        /// <returns>number of rows affected:  0 fails and a 1 for success</returns>
        public static int AddEventType(string newEventType)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spInsertEventType";
            var cmd = new SqlCommand(cmdText, conn);

            var rowsAffected = 0;
            //set up parameters for EventType
            cmd.Parameters.AddWithValue("@EventName", newEventType);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Justin Pennington 
        /// Created:  2015/02/14
        /// 
        /// Updates an EventType object and database record
        /// </summary>
        /// </summary>
        /// <param name="oldEventType">The EventType object to be updated</param>
        /// <param name="newEventType">The EventType object with the updated changes</param>
        /// <returns>Returns the number of rows affected (should be 1)</returns>
        public static int UpdateEventType(EventType oldEventType, EventType newEventType)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateEventType";
            var cmd = new SqlCommand(cmdText, conn);
  
            cmd.CommandType = CommandType.StoredProcedure;
            var rowsAffected = 0;

            // set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventName", newEventType.EventName);

            cmd.Parameters.AddWithValue("@originalEventTypeID",oldEventType.EventTypeID);
            cmd.Parameters.AddWithValue("@originalEventName", oldEventType.EventName);

            try
            { 
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;  
        }

        /// <summary>
        /// Justin Pennington 
        /// Created:  2015/02/14
        /// Archives an event type record
        /// </summary>
        /// <param name="eventTypeToArchive">The EventType object to be archived</param>
        /// <returns>returns number of rows affected</returns>
        public static int DeleteEventType(EventType eventTypeToArchive)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spArchiveEventType";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            
            //Set up parameters for EventType
            cmd.Parameters.AddWithValue("@EventTypeID", eventTypeToArchive.EventTypeID);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;  
        }

        /// <summary>
        /// Justin Pennington
        /// Created 2015/02/14
        /// 
        /// Retrieves all EventTypes. 
        /// Makes a List of EventTypes,
        /// </summary>
        /// <returns> Returns the List of EventTypes</returns>
        public static List<EventType> GetEventTypeList()
        {
            var EventTypeList = new List<EventType>();

            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectAllEventTypes";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var currentEvent = new EventType();

                        currentEvent.EventTypeID = (int)reader.GetValue(0);
                        currentEvent.EventName = reader.GetString(1);
                        EventTypeList.Add(currentEvent);
                    }
                }
                else
                {
                    var ax = new ApplicationException("Event Types not found!");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return EventTypeList;
        }

        /// <summary>
        /// Justin Pennington 
        /// Created:  2015/02/14
        /// Gets an eventTypeID, retrieves data from databases
        /// </summary>
        /// <param name="eventTypeID">String containing the EventTypeID matching a specific EventType record/object</param>
        /// <returns>Returns an EventType object</returns>
        public static EventType GetEventType(string eventTypeID)
        {
            EventType theEventType = new EventType();
            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "spSelectEventType";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@EventTypeID", eventTypeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    theEventType.EventTypeID = reader.GetInt32(0);
                    theEventType.EventName = reader.GetString(1);
                }
                else
                {
                    var ax = new ApplicationException("Data not found!");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return theEventType;
        }
    }
}