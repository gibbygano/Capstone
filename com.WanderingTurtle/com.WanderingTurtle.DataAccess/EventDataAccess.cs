//Justin Pennington

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;
using System.Data;

namespace com.WanderingTurtle.DataAccess
{
    class EventDataAccess
    {
         
        //this will only be allowed by the admin of a system, this is NOT for suppliers because this will purge data from the server
        //for supplier deletion we will mark the event as inactive as far as my understanding goes.
        public static int DeleteEvent(string inEventID)
        {
            var conn = DatabaseConnection.GetDBConnection();
            string query = "DELETE FROM EventItem" +
                " WHERE EventItemID = " + inEventID;
            var cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        //creates a new event item in the database, using an event object that is passed in
        public static int InsertEvent(Event newEvent)
        {
            var conn = DatabaseConnection.GetDBConnection();
            string query = "INSERT INTO EventItem (EventItemName, EventStartTime, EventEndTime, MaxNumberOfGuests," +
            "CurrentNumberOfGuests, EventTypeID, PricePerPerson, EventOnsite, Transportation, EventDescription, Active) " +
                                 "VALUES(@EventItemName, EventStartTime, EventEndTime, MaxNumberOfGuests, CurrentNumberOfGuests, EventTypeID" +
                                 ", PricePerPerson, EventOnsite, Transportation, EventDescription, Active)";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@EventItemName", newEvent.EventItemName);
            cmd.Parameters.AddWithValue("@EventStartTime", newEvent.EventStartTime);
            cmd.Parameters.AddWithValue("@EventEndTime", newEvent.EventEndTime);
            cmd.Parameters.AddWithValue("@MaxNumberOfGuests", newEvent.MaxNumberOfGuests);
            cmd.Parameters.AddWithValue("@EventTypeID", newEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@PricePerPerson", newEvent.PricePerPerson);
            cmd.Parameters.AddWithValue("@EventOnsite", newEvent.EventOnsite);
            cmd.Parameters.AddWithValue("@Transportation", newEvent.Transportation);
            cmd.Parameters.AddWithValue("@EventDescription", newEvent.EventDescription);
            cmd.Parameters.AddWithValue("@Active", newEvent.Active);

            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        //needs the event object that is having its name being changed and the new name
        //Returns the number of rows affected (should be 1) 
        public static int UpdateEvent(Event oldEvent, Event newEvent)
        {
            var conn = DatabaseConnection.GetDBConnection();
            var cmdText = "spUpdateEventName";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            // set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventItemName", newEvent.EventName);
            cmd.Parameters.AddWithValue("@EventStartTime", newEvent.EventStartTime);
            cmd.Parameters.AddWithValue("@EventEndTime", newEvent.EventEndTime);
            cmd.Parameters.AddWithValue("@MaxNumberOfGuests", newEvent.MaxNumberOfGuests);
            cmd.Parameters.AddWithValue("@PricePerPerson", newEvent.PricePerPerson);
            cmd.Parameters.AddWithValue("@Transportation", newEvent.Transportation);
            cmd.Parameters.AddWithValue("@EventDescription", newEvent.EventDescription);
            cmd.Parameters.AddWithValue("@Active", newEvent.Active);

            cmd.Parameters.AddWithValue("@original_EventItemName", oldEvent.Name);
            cmd.Parameters.AddWithValue("@original_EventID", oldEvent.EventItemID);
            cmd.Parameters.AddWithValue("@original_EventStartTime", oldEvent.EventStartTime);
            cmd.Parameters.AddWithValue("@original_EventEndTime", oldEvent.EventEndTime);
            cmd.Parameters.AddWithValue("@original_MaxNumberOfGuests", oldEvent.MaxNumberOfGuests);
            cmd.Parameters.AddWithValue("@original_PricePerPerson", oldEvent.PricePerPerson);
            cmd.Parameters.AddWithValue("@original_Transportation", oldEvent.Transportation);
            cmd.Parameters.AddWithValue("@original_EventDescription", oldEvent.EventDescription);
            cmd.Parameters.AddWithValue("@original_Active", oldEvent.Active);
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
        public static int DeleteEvent(Event newEvent)
        {
            var conn = DatabaseConnection.GetDBConnection();
            var cmdText = "spDeleteEvent";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", 0);

            cmd.Parameters.AddWithValue("@original_EventItemName", newEvent.Name);
            cmd.Parameters.AddWithValue("@original_EventID", newEvent.EventItemID);
            cmd.Parameters.AddWithValue("@original_EventStartTime", newEvent.EventStartTime);
            cmd.Parameters.AddWithValue("@original_EventEndTime", newEvent.EventEndTime);
            cmd.Parameters.AddWithValue("@original_MaxNumberOfGuests", newEvent.MaxNumberOfGuests);
            cmd.Parameters.AddWithValue("@original_PricePerPerson", newEvent.PricePerPerson);
            cmd.Parameters.AddWithValue("@original_Transportation", newEvent.Transportation);
            cmd.Parameters.AddWithValue("@original_EventDescription", newEvent.EventDescription);
            cmd.Parameters.AddWithValue("@original_Active", newEvent.Active);
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

        public static List<Event> RetrieveEventList()
        {
            var CharacterList = new List<Event>();

            // set up the database call
            var conn = DatabaseConnection.GetDBConnection();
            string query = "SELECT EventItemID, EventItemName, EventStartTime, EventEndTime, MaxNumberOfGuests," +
            "CurrentNumberOfGuests, MinNumberOfGuests, EventTypeID, PricePerPerson, EventOnsite, Transportation, EventDescription, Active " +
            "FROM EventItem";
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

                        currentEvent.EventItemID = reader.GetString(0);
                        currentEvent.EventItemName = reader.GetString(1);
                        currentEvent.EventEndTime = (DateTime)reader.GetValue(3);
                        currentEvent.MaxNumberOfGuests = reader.GetInt32(4);
                        currentEvent.CurrentNumberOfGuests = reader.GetInt32(5);
                        currentEvent.MinNumberOfGuests = reader.GetInt32(6);
                        currentEvent.EventTypeID = reader.GetInt32(7);
                        currentEvent.PricePerPerson = reader.GetDecimal(8);
                        currentEvent.EventOnSite = reader.GetBoolean(9);
                        currentEvent.Transportation = reader.GetBoolean(10);
                        currentEvent.EventDescription = reader.GetString(11);
                        currentEvent.Active = reader.GetBoolean(12);
                        CharacterList.Add(currentEvent);
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
            return CharacterList;
        }
    }
}

