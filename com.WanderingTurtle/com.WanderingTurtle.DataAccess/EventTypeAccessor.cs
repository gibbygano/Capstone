using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    class EventTypeAccessor
    {
        public static int addEventType(EventType newEventType)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spInsertEventType";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            cmd.Parameters.AddWithValue("@EventTypeID", newEventType.EventTypeID);
            cmd.Parameters.AddWithValue("@EventName", newEventType.EventName);
            
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

        //needs the event object that is having its name being changed and the new name
        //Returns the number of rows affected (should be 1)
        public static int updateEventType(EventType oldEventType, EventType newEventType)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateEventType";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            // set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventName", newEventType.EventName);

            cmd.Parameters.AddWithValue("@original_EventTypeID", oldEventType.EventItemName);
            cmd.Parameters.AddWithValue("@original_EventName", oldEventType.EventItemID);
            
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
            return rowsAffected;  // needs to be rows affected
        }

        //requires: Event object, Boolean value for active/inactive
        //returns number of rows affected
        public static int DeleteEventType(EventType oldEventType)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spDeleteEventType";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@original_EventTypeID", oldEventType.EventItemName);
            cmd.Parameters.AddWithValue("@original_EventName", oldEventType.EventItemID);
            
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
            return rowsAffected;  // needs to be rows affected
        }

        public static List<EventType> getEventTypeList()
        {
            var EventTypeList = new List<Event>();

            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "SELECT EventTypeID, EventName "+
            "FROM EventType";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var currentEvent = new Event();

                        currentEvent.EventItemID = reader.GetInt32(0);
                        currentEvent.EventItemName = reader.GetString(1);
                        
                    }
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
            return EventTypeList;
        }

        public static Event getEventType(String eventTypeID)
        {
            var theEventType = new EventType();
            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "SELECT EventTypeID, EventName" + 
            "FROM EventType WHERE EventTypeID = " + eventTypeID;
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    theEventType.EventTypeD = reader.GetInt32(0);
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
