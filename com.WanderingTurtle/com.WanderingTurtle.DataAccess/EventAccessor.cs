//Justin Pennington

using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.WanderingTurtle.DataAccess
{
    public class EventAccessor
    {
        //Justin Pennington 2/14/15
        //creates a new event item in the database, using an event object that is passed in
        public static int AddEvent(Event newEvent)
        {
            //Connect To Database
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spInsertEventItem";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            //Set up Parameters For the Stored Procedure
            cmd.Parameters.AddWithValue("@EventItemName", newEvent.EventItemName);
            cmd.Parameters.AddWithValue("@EventStartTime", newEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@EventEndTime", newEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@MaxNumberOfGuests", newEvent.MaxNumGuests);
            cmd.Parameters.AddWithValue("@MinNumberOfGuests", newEvent.MinNumGuests);
            cmd.Parameters.AddWithValue("@EventTypeID", newEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@PricePerPerson", newEvent.PricePerPerson);
            cmd.Parameters.AddWithValue("@EventOnsite", newEvent.OnSite);
            cmd.Parameters.AddWithValue("@Transportation", newEvent.Transportation);
            cmd.Parameters.AddWithValue("@EventDescription", newEvent.Description);

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


        //Justin Pennington 2/14/15
        //needs the event object that is having its name being changed and the new name
        //Returns the number of rows affected (should be 1)
        public static int UpdateEvent(Event oldEvent, Event newEvent)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateEventItem";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            // set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventItemName", newEvent.EventItemName);
            cmd.Parameters.AddWithValue("@EventStartTime", newEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@EventEndTime", newEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@MaxNumberOfGuests", newEvent.MaxNumGuests);
            cmd.Parameters.AddWithValue("@MinNumberOfGuests", newEvent.MinNumGuests);
            cmd.Parameters.AddWithValue("@CurrentNumberOfGuests", newEvent.CurrentNumGuests);
            cmd.Parameters.AddWithValue("@EventTypeID", newEvent.EventItemID);
            cmd.Parameters.AddWithValue("@EventOnsite", newEvent.OnSite);
            cmd.Parameters.AddWithValue("@PricePerPerson", newEvent.PricePerPerson);
            cmd.Parameters.AddWithValue("@Transportation", newEvent.Transportation);
            cmd.Parameters.AddWithValue("@EventDescription", newEvent.Description);
            cmd.Parameters.AddWithValue("@Active", newEvent.Active);

            cmd.Parameters.AddWithValue("@originalEventItemName", oldEvent.EventItemName);
            cmd.Parameters.AddWithValue("@originalEventID", oldEvent.EventItemID);
            cmd.Parameters.AddWithValue("@originalEventStartTime", oldEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@originalEventEndTime", oldEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@originalMaxNumberOfGuests", oldEvent.MaxNumGuests);
            cmd.Parameters.AddWithValue("@originalMinNumberOfGuests", oldEvent.MinNumGuests);
            cmd.Parameters.AddWithValue("@originalCurrentNumberOfGuests", oldEvent.CurrentNumGuests);
            cmd.Parameters.AddWithValue("@originalEventOnsite", oldEvent.OnSite);
            cmd.Parameters.AddWithValue("@originalPricePerPerson", oldEvent.PricePerPerson);
            cmd.Parameters.AddWithValue("@originalTransportation", oldEvent.Transportation);
            cmd.Parameters.AddWithValue("@originalEventDescription", oldEvent.Description);
            cmd.Parameters.AddWithValue("@originalActive", oldEvent.Active);
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


        //Justin Pennington 2/14/15
        //requires: Event object, Boolean value for active/inactive
        //returns number of rows affected
        public static int DeleteEventItem(Event newEvent)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spDeleteEvent";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", 0);

            cmd.Parameters.AddWithValue("@originalEventItemName", newEvent.EventItemName);
            cmd.Parameters.AddWithValue("@originalEventID", newEvent.EventItemID);
            cmd.Parameters.AddWithValue("@originalEventStartTime", newEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@originalEventEndTime", newEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@originalMaxNumberOfGuests", newEvent.MaxNumGuests);
            cmd.Parameters.AddWithValue("@originalPricePerPerson", newEvent.PricePerPerson);
            cmd.Parameters.AddWithValue("@originalTransportation", newEvent.Transportation);
            cmd.Parameters.AddWithValue("@originalEventDescription", newEvent.Description);
            cmd.Parameters.AddWithValue("@originalActive", newEvent.Active);
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

        //Justin Pennington 2/14/15
        //retrieves all Events and returns a List of Event objects
        public static List<Event> GetEventList()
        {
            var EventList = new List<Event>();

            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "spSelectAllEventItems";
            var cmd = new SqlCommand(cmdText, conn);

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
                        currentEvent.EventStartDate = (DateTime)reader.GetValue(2);
                        currentEvent.EventEndDate = (DateTime)reader.GetValue(3);
                        currentEvent.MaxNumGuests = reader.GetInt32(4);
                        currentEvent.CurrentNumGuests = reader.GetInt32(5);
                        currentEvent.MinNumGuests = reader.GetInt32(6);
                        currentEvent.EventTypeID = reader.GetInt32(7);
                        currentEvent.PricePerPerson = reader.GetDecimal(8);
                        currentEvent.OnSite = reader.GetBoolean(9);
                        currentEvent.Transportation = reader.GetBoolean(10);
                        currentEvent.Description = reader.GetString(11);
                        currentEvent.Active = reader.GetBoolean(12);
                        EventList.Add(currentEvent);
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
            return EventList;
        }


        //Justin Pennington 2/14/15
        //retrieve data for an Event, create an object using data with retrieved data, and return the object that is created
        public static Event GetEvent(String eventID)
        {
            var theEvent = new Event();
            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "spSelectEventItem";
            var cmd = new SqlCommand(cmdText, conn);


            cmd.Parameters.AddWithValue("@EventItemID", eventID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    theEvent.EventItemID = reader.GetInt32(0);
                    theEvent.EventItemName = reader.GetString(1);
                    theEvent.EventEndDate = (DateTime)reader.GetValue(3);
                    theEvent.MaxNumGuests = reader.GetInt32(4);
                    theEvent.CurrentNumGuests = reader.GetInt32(5);
                    theEvent.MinNumGuests = reader.GetInt32(6);
                    theEvent.EventTypeID = reader.GetInt32(7);
                    theEvent.PricePerPerson = reader.GetDecimal(8);
                    theEvent.OnSite = reader.GetBoolean(9);
                    theEvent.Transportation = reader.GetBoolean(10);
                    theEvent.Description = reader.GetString(11);
                    theEvent.Active = reader.GetBoolean(12);
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
            return theEvent;
        }
    }
}