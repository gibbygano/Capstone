using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
    /// <summary>
    /// Manages CRUD Operations for data pertaining to Suppliers
    /// </summary>
    public enum SupplierResult
    {
        //item could not be found
        NotFound = 0,

        //new supplier could not be added
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

    public class SupplierManager
    {
        public SupplierManager()
        {
            //default constructor
        }

        /// <summary>
        /// Reece Maas 
        /// Created: 2015/02/18
        /// Gets a single Supplier  from the Data Access layer
        /// </summary>
        /// <remarks>
        /// Matt Lapka
        /// Updated:  2015/03/27
        /// Added supplier cache updates
        /// </remarks>
        /// <param name="supplierID">The string ID of the application to be retrieved</param>
        /// <returns>Supplier object whose ID matches the passed parameter</returns>
        public Supplier RetrieveSupplier(string supplierID)
        {
            var now = DateTime.Now;
            double cacheExpirationTime = 5;

            try
            {
                if (DataCache._currentSupplierList == null)
                {
                    return SupplierAccessor.GetSupplier(supplierID);
                }
                //check time. If less than 5 min, return event from cache
                if (now > DataCache._SupplierListTime.AddMinutes(cacheExpirationTime))
                {
                    //get event from DB
                    var currentSupplier = SupplierAccessor.GetSupplier(supplierID);
                    return currentSupplier;
                }
                else
                {
                    //get event from cached list
                    var list = DataCache._currentSupplierList;
                    Supplier currentSupplier = list.Where(e => e.SupplierID.ToString() == supplierID).FirstOrDefault();
                    if (currentSupplier != null)
                    {
                        return currentSupplier;
                    }
                    throw new ApplicationException("Supplier not found.");
                }
            }
            catch (Exception)
            {
                throw new Exception("Supplier not found.");
            }
        }

        /// <summary>
        /// Reece Maas 
        /// Created: 2015/02/18
        /// Gets a list of Suppliers
        /// </summary>
        /// <remarks>
        /// Matt Lapka 
        /// Updated:  2015/03/27
        /// Added supplier cache
        /// </remarks>
        /// <returns>A List object containing Supplier objects returned by the database</returns>
        public List<Supplier> RetrieveSupplierList()
        {
            double cacheExpirationTime = 5; //how long the cache should live (minutes)
            var now = DateTime.Now;
            try
            {
                if (DataCache._currentSupplierList == null)
                {
                    //data hasn't been retrieved yet. get data, set it to the cache and return the result.
                    var list = SupplierAccessor.GetSupplierList();
                    DataCache._currentSupplierList = list;
                    DataCache._SupplierListTime = now;
                    return list;
                }
                //check time. If less than 5 min, return cache

                if (now > DataCache._SupplierListTime.AddMinutes(cacheExpirationTime))
                {
                    //get new list from DB
                    var list = SupplierAccessor.GetSupplierList();
                    //set cache to new list and update time
                    DataCache._currentSupplierList = list;
                    DataCache._SupplierListTime = now;

                    return list;
                }
                return DataCache._currentSupplierList;
            }
            catch (Exception)
            {
                throw new Exception("No suppliers in database.");
            }
        }

        /// <summary>
        /// Reece Maas 
        /// Created: 2015/02/18
        /// Adds a single Supplier to the database
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <remarks>
        /// Matt Lapka 
        /// Updated:  2015/03/27
        /// Added supplier cache
        /// </remarks>
        /// <param name="supplierToAdd">Supplier object containing the information of the supplier to be added</param>
        /// <param name="userName">The username to be given to the Supplier</param>
        /// <returns>An enumerated result depicting pass or fail</returns>
        public SupplierResult AddANewSupplier(Supplier supplierToAdd, string userName)
        {
            try
            {
                if (SupplierAccessor.AddSupplier(supplierToAdd, userName) == 2)
                {
                    //refresh cache
                    DataCache._currentSupplierList = SupplierAccessor.GetSupplierList();
                    DataCache._SupplierListTime = DateTime.Now;
                    return SupplierResult.Success;
                }
                return SupplierResult.NotAdded;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? SupplierResult.ChangedByOtherUser : SupplierResult.DatabaseError;
            }
            catch (Exception ex)
            {
                throw ex;
                //return SupplierResult.DatabaseError;
            }
        }

        /// <summary>
        /// Reece Maas 
        /// Created: 2015/02/18
        /// Updates a Supplier
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <remarks>
        /// Matt Lapka 
        /// Updated:  2015/03/27
        /// Added supplier cache
        /// </remarks>
        /// <param name="newSupplier">Supplier object containing the new information of the supplier</param>
        /// <param name="oldSupplier">Supplier object containing the current information of the supplier to be matched to salve concurrency problems</param>
        /// <returns>An enumerated result depicting pass or fail</returns>
        public SupplierResult EditSupplier(Supplier oldSupplier, Supplier newSupplier)
        {
            try
            {
                if (SupplierAccessor.UpdateSupplier(newSupplier, oldSupplier) == 1)
                {
                    //update cache
                    DataCache._currentSupplierList = SupplierAccessor.GetSupplierList();
                    DataCache._SupplierListTime = DateTime.Now;
                    return SupplierResult.Success;
                }
                return SupplierResult.NotChanged;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? SupplierResult.ChangedByOtherUser : SupplierResult.DatabaseError;
            }
            catch (Exception)
            {
                return SupplierResult.DatabaseError;
            }
        }


        /// <summary>
        /// Matt Lapka
        /// Created:  2015/02/08
        /// Archives a Supplier
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated:  2015/04/26
        /// Added archiving of login at the same time as archiving other supplier information
        /// </remarks>
        /// <param name="supplierToDelete">The Supplier object to be deleted/made inactive</param>
        /// <returns>
        /// An enumerated result depicting pass or fail
        /// </returns>
        public SupplierResult ArchiveSupplier(Supplier supplierToDelete)
        {
            try
            {
                if (SupplierAccessor.DeleteSupplier(supplierToDelete) == 2)
                {
                    //update cache
                    DataCache._currentSupplierList = SupplierAccessor.GetSupplierList();
                    DataCache._SupplierListTime = DateTime.Now;
                    return SupplierResult.Success;
                }
                return SupplierResult.NotChanged;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? SupplierResult.ChangedByOtherUser : SupplierResult.DatabaseError;
            }
            catch (Exception)
            {
                return SupplierResult.DatabaseError;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/02/08
        /// Gets a single Supplier Application Record from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="applicationID">The string ID of the application to be retrieved</param>
        /// <returns>A SupplierApplication object whose applicationID matches the passed parameter</returns>
        public SupplierApplication RetrieveSupplierApplication(string applicationID)
        {
            try
            {
                return SupplierApplicationAccessor.GetSupplierApplication(applicationID);
            }
            catch (Exception)
            {
                throw new Exception("Application does not exist.");
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Gets a list of Pending Supplier Application Records from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <returns>List of SupplierApplication objects</returns>
        public List<SupplierApplication> RetrieveSupplierApplicationList()
        {
            try
            {
                return SupplierApplicationAccessor.GetSupplierApplicationList();
            }
            catch (Exception)
            {
                throw new Exception("No applications.");
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Add a single Supplier Application Record to the database
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated:  2015/04/11
        /// Added logic for returning the result of the operations to the presentation layer
        /// </remarks>
        /// <param name="newSupplier">Supplier object containing the information of the supplier to be added</param>
        /// <returns>An int reflecting the number of rows affected</returns>
        public SupplierResult AddASupplierApplication(SupplierApplication newSupplierApp)
        {
            try
            {
                return SupplierApplicationAccessor.AddSupplierApplication(newSupplierApp) == 1 ? SupplierResult.Success : SupplierResult.NotAdded;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation" ? SupplierResult.ChangedByOtherUser : SupplierResult.DatabaseError;
            }
            catch (Exception)
            {
                return SupplierResult.DatabaseError;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Updates a Supplier Application Record
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated:  2015/04/11
        /// Added logic for returning the result of the operations to the presentation layer
        /// </remarks>
        /// <param name="newSupplier">Supplier object containing the new information of the supplier</param>
        /// <param name="oldSupplier">Supplier object containing the current information of the supplier to be matched to salve concurrency problems</param>
        /// <returns>An int reflecting the of rows affected</returns>
        public SupplierResult EditSupplierApplication(SupplierApplication oldSupplierApp, SupplierApplication updatedSupplierApp)
        {
            try
            {
                if (updatedSupplierApp.ApplicationStatus.Equals(ApplicationStatus.Pending.ToString()))
                {
                    //just editing application - still Pending
                    //update db with new info not related to approval
                    int numRows = SupplierApplicationAccessor.UpdateSupplierApplication(oldSupplierApp, updatedSupplierApp);

                    if (numRows == 1)
                    {
                        RetrieveSupplierApplicationList();
                        return SupplierResult.Success;
                    }
                    return SupplierResult.ChangedByOtherUser;
                }
                //Rejecting the application
                if (updatedSupplierApp.ApplicationStatus.Equals(ApplicationStatus.Rejected.ToString()))
                {
                    //update db with rejection
                    int numRows = SupplierApplicationAccessor.UpdateSupplierApplication(oldSupplierApp, updatedSupplierApp);

                    return numRows == 1 ? SupplierResult.Success : SupplierResult.ChangedByOtherUser;
                }
                return SupplierResult.NotChanged;
            }
            catch (ApplicationException ex)
            {
                return ex.Message == "Concurrency Violation." ? SupplierResult.ChangedByOtherUser : SupplierResult.DatabaseError;
            }
            catch (Exception)
            {
                return SupplierResult.DatabaseError;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/11
        /// Returns the result of approving a supplier application and adds records to the Supplier Table and SupplierLogin tables
        /// </summary>
        /// <param name="oldSupplierApp">The SupplierApplication object to be updated</param>
        /// <param name="updatedSupplierApp">The SupplierApplication object with the updated information</param>
        /// <param name="userName">The username of the Supplier</param>
        /// <param name="supplyCost">The supplier's portion of ticket proceeds</param>
        /// <returns>An enumerated result depicting pass or fail</returns>
        public SupplierResult ApproveSupplierApplication(SupplierApplication oldSupplierApp, SupplierApplication updatedSupplierApp, string userName, decimal supplyCost)
        {
            try
            {
                //Approving
                //update db with approval, add supplier record, add supplier login
                int numRows = SupplierApplicationAccessor.UpdateSupplierApplication(oldSupplierApp, updatedSupplierApp, userName, supplyCost);

                if (numRows == 3)
                {
                    //refresh cache
                    DataCache._currentSupplierList = SupplierAccessor.GetSupplierList();
                    DataCache._SupplierListTime = DateTime.Now;
                    return SupplierResult.Success;
                }
                return SupplierResult.ChangedByOtherUser;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Justin Pennington
        /// Created: on 2015/04/14
        /// Searches for a string the user inputs
        /// </summary>
        /// <param name="inSearch">The string to be searched for</param>
        /// <returns>
        /// A List object of Supplier objects that contain the search parameter
        /// or an empty list if none are found.
        /// </returns>
        public List<Supplier> searchSupplier(string inSearch)
        {
            if (!inSearch.Equals("") && !inSearch.Equals(null))
            {
                List<Supplier> SearchList = RetrieveSupplierList();
                List<Supplier> myTempList = new List<Supplier>();
                myTempList.AddRange(
                  from inSupplier in SearchList
                  where inSupplier.FirstName.ToUpper().Contains(inSearch.ToUpper()) || inSupplier.LastName.ToUpper().Contains(inSearch.ToUpper()) || inSupplier.CompanyName.ToUpper().Contains(inSearch.ToUpper())
                  select inSupplier);
                return myTempList;

                //Will empty the search list if nothing is found so they will get feedback for typing something incorrectly
            }
            return RetrieveSupplierList();
        }
    }
}