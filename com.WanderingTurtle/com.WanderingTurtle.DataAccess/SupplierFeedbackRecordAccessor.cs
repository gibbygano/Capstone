//Justin Pennington

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.DataAccess
{
    class SupplierFeedbackRecordAccessor
    {
        /// <summary>
        /// Created by Justin Pennington 2015/02/25
        /// Retrieves a SupplierFeedbackRecord from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ratingID">A rating ID pulled from an object</param>
        /// <exception cref="SQLException">Insert Fails</exception>
        /// <returns>SupplierFeedbackRecord</returns>
        public static SupplierFeedbackRecord GetSupplierFeedbackRecord(String ratingID)
        {
            var theRecord = new SupplierFeedbackRecord();
            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "spSelectSupplierFeedbackRecord";
            var cmd = new SqlCommand(cmdText, conn);


            cmd.Parameters.AddWithValue("@RatingID", ratingID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    theRecord.RatingID = reader.GetInt32(0);
                    theRecord.SupplierID = reader.GetInt32(1);
                    theRecord.EmployeeID = reader.GetInt32(2);
                    theRecord.Rating = reader.GetInt32(3);
                    theRecord.RatingNotes = reader.GetString(4);
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
            return theRecord;
        }

        /// <summary>
        /// Created by Justin Pennington 2015/02/25
        /// Retrieves a SupplierFeedbackRecord list from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <exception cref="SQLException">Insert Fails</exception>
        /// <returns>List of SupplierFeedbackRecord</returns>
        public static List<SupplierFeedbackRecord> GetSupplierFeedbackRecordList()
        {
            var recordList = new List<SupplierFeedbackRecord>();

            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "spSelectAllSupplierFeedbackRecord";
            var cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var currentRecord = new SupplierFeedbackRecord();

                        currentRecord.RatingID = reader.GetInt32(0);
                        currentRecord.SupplierID = reader.GetInt32(1);
                        currentRecord.EmployeeID = reader.GetInt32(2);
                        currentRecord.Rating = reader.GetInt32(3);
                        currentRecord.RatingNotes = reader.GetString(4);
                        recordList.Add(currentRecord);
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
            return recordList;
        }

        /// <summary>
        /// Created by Justin Pennington 2015/02/25
        /// Deletes a SupplierFeedbackRecord from the database (no archive setting as of 2015/02/25)
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="supplierFeedback">A Supplier feedback record Object that contain all the information to be deleted</param>
        /// <exception cref="SQLException">Delete Fails</exception>
        /// <returns>rowsAffected (int)</returns>
        public static int DeleteSupplierFeedbackRecord(SupplierFeedbackRecord supplierFeedback)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spDeleteSupplierFeedbackRecord";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RatingID", supplierFeedback.RatingID);
            cmd.Parameters.AddWithValue("@SupplierID", supplierFeedback.SupplierID);
            cmd.Parameters.AddWithValue("@EmployeeID", supplierFeedback.EmployeeID);
            cmd.Parameters.AddWithValue("@Rating", supplierFeedback.Rating);
            cmd.Parameters.AddWithValue("@Notes", supplierFeedback.RatingNotes);
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

        /// <summary>
        /// Created by Justin Pennington 2015/02/25
        /// Deletes a SupplierFeedbackRecord from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="oldFeedbackRecord">An object that contains all the values of the record pre-change</param>
        /// <param name="newFeedbackRecord">An object that contains all the values of the record post-change</param>
        /// <returns>rowsAffected (int)</returns>
        public static int UpdateSupplierFeedbackRecord(SupplierFeedbackRecord oldFeedbackRecord, SupplierFeedbackRecord newFeedbackRecord)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateSupplierFeedbackRecord";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            // set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@RatingID", newFeedbackRecord.RatingID);
            cmd.Parameters.AddWithValue("@SupplierID", newFeedbackRecord.SupplierID);
            cmd.Parameters.AddWithValue("@EmployeeID", newFeedbackRecord.EmployeeID);
            cmd.Parameters.AddWithValue("@Rating", newFeedbackRecord.Rating);
            cmd.Parameters.AddWithValue("@Notes", newFeedbackRecord.RatingNotes);

            cmd.Parameters.AddWithValue("@originalRatingID", oldFeedbackRecord.RatingID);
            cmd.Parameters.AddWithValue("@originalSupplierID", oldFeedbackRecord.SupplierID);
            cmd.Parameters.AddWithValue("@originalEmployeeID", oldFeedbackRecord.EmployeeID);
            cmd.Parameters.AddWithValue("@originalRating", oldFeedbackRecord.Rating);
            cmd.Parameters.AddWithValue("@originalNotes", oldFeedbackRecord.RatingNotes);
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

        /// <summary>
        /// Created by Justin Pennington 2015/02/25
        /// Adds a SupplierFeedbackRecord to the database
        /// </summary>
        /// <param name="newFeedbackRecord">Contains values to be added to the database</param>
        /// <returns>rowsAffected (int)</returns>
        public static int AddSupplierFeedbackRecord(SupplierFeedbackRecord newFeedbackRecord)
        {
            //Connect To Database
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spInsertSupplierFeedbackRecord";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            //Set up Parameters For the Stored Procedure
            cmd.Parameters.AddWithValue("@SupplierID", newFeedbackRecord.SupplierID);
            cmd.Parameters.AddWithValue("@EmployeeID", newFeedbackRecord.EmployeeID);
            cmd.Parameters.AddWithValue("@Rating", newFeedbackRecord.Rating);
            cmd.Parameters.AddWithValue("@Notes", newFeedbackRecord.RatingNotes);

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

    }
}
