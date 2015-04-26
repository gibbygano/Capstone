using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    public class CityStateAccessor
    {
        /// <summary>
        /// Get a list of all CityState records
        /// </summary>
        /// <param name="zip"></param>
        /// <returns>List of CityState Objects</returns>
        /// Miguel Santana 2/18/2015
        public static List<CityState> CityStateGet(String zip = null)
        {
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

            String cmdText = (zip == null) ? "spSelectCityStateList" : "spSelectCityState";
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            if (zip != null) { cmd.Parameters.AddWithValue("@zip", zip); }

            List<CityState> list = new List<CityState>();
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(new CityState(
                            reader.GetString(0), //Zip
                            reader.GetString(1), //City
                            reader.GetString(2) //State
                        ));
                    }
                }
                else
                {
                    throw new ApplicationException(zip == null ? "Did not find any records for CityState" : "Could not find CityState record for specified Zip");
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
            return list;
        }
    }
}