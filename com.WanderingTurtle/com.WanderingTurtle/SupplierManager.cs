using System.Collections.Generic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
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
        /// Gets a single Supplier  from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// Edited by Matt Lapka 2015/03/27
        /// </summary>
        /// <param name="supplierID">string ID of the application to be retrieved</param>
        /// <returns>Supplier object</returns>
        /// Created by Reece Maas 2/18/15
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
                else
                {
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
                        else
                        {
                            throw new ApplicationException("Supplier not found.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Supplier not found");
            }

        }
        /// <summary>
        /// Gets a list of Suppliers  from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// Edited by Matt Lapka 2015/03/27
        /// </summary>
        /// <returns>Supplier List</returns>
        /// Created by Reece Maas 2/18/15
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
                else
                {
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
                    else
                    {
                        return DataCache._currentSupplierList;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("No suppliers in database");
            }

        }
        /// <summary>
        /// Adds a single Supplier to the database
        /// Throws any exceptions caught by the DAL
        /// Edited by Matt Lapka 2015/03/27
        /// </summary>
        /// <param name="newSupplier">Supplier object containing the information of the supplier to be added</param>
        /// <returns>Supplier object</returns>
        /// Created by Reece Maas 2/18/15
        public SupplierResult AddANewSupplier(Supplier supplierToAdd)
        {
            try
            {
                if (SupplierAccessor.AddSupplier(supplierToAdd) == 1)
                {
                    //refresh cache
                    DataCache._currentSupplierList = SupplierAccessor.GetSupplierList();
                    DataCache._SupplierListTime = DateTime.Now;
                    return SupplierResult.Success;
                }
                else
                {
                    return SupplierResult.NotAdded;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return SupplierResult.ChangedByOtherUser;
                }
                return SupplierResult.DatabaseError;
            }
            catch (Exception)
            {
                return SupplierResult.DatabaseError;
            }

        }
        /// <summary>
        /// Updates a Supplier 
        /// Throws any exceptions caught by the DAL
        /// Edited by Matt Lapka 2015/03/27
        /// </summary>
        /// <param name="newSupplier">Supplier object containing the new information of the supplier</param>
        /// <param name="oldSupplier">Supplier object containing the current information of the supplier to be matched to salve concurrency problems</param>
        /// <returns>updated Supplier</returns>
        /// Created by Reece Maas 2/18/15
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
                else
                {
                    return SupplierResult.NotChanged;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return SupplierResult.ChangedByOtherUser;
                }
                return SupplierResult.DatabaseError;
            }
            catch (Exception)
            {

                return SupplierResult.DatabaseError;
            }

        }
        public SupplierResult ArchiveSupplier(Supplier supplierToDelete)
        {
            try
            {
                if (SupplierAccessor.DeleteSupplier(supplierToDelete) == 1)
                {
                    //update cache
                    DataCache._currentSupplierList = SupplierAccessor.GetSupplierList();
                    DataCache._SupplierListTime = DateTime.Now;
                    return SupplierResult.Success;
                }
                else
                {
                    return SupplierResult.NotChanged;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return SupplierResult.ChangedByOtherUser;
                }
                return SupplierResult.DatabaseError;
            }
            catch (Exception)
            {
                return SupplierResult.DatabaseError;
            }

        }
        
        /// <summary>
        /// Matt Lapka
        /// 2015/02/08
        /// Gets a single Supplier Application Record from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="applicationID">string ID of the application to be retrieved</param>
        /// <returns>SupplierApplication object</returns>
        public SupplierApplication RetrieveSupplierApplication(string applicationID)
        {
            try
            {
                return SupplierApplicationAccessor.GetSupplierApplication(applicationID);
            }
            catch (Exception)
            {

                throw new Exception("Application does not exist");
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

                throw new Exception("No applications");
            }

        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Add a single Supplier Application Record to the database
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="newSupplier">Supplier object containing the information of the supplier to be added</param>
        /// <returns>int # of rows affected</returns>
        public SupplierResult AddASupplierApplication(SupplierApplication newSupplierApp)
        {
            try
            {
                if (SupplierApplicationAccessor.AddSupplierApplication(newSupplierApp) == 1)
                {
                    return SupplierResult.Success;
                }
                else
                {
                    return SupplierResult.NotAdded;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return SupplierResult.ChangedByOtherUser;
                }
                return SupplierResult.DatabaseError;
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
        /// <param name="newSupplier">Supplier object containing the new information of the supplier</param>
        /// <param name="oldSupplier">Supplier object containing the current information of the supplier to be matched to salve concurrency problems</param>
        /// <returns>int # of rows affected</returns>
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
                        return SupplierResult.Success;
                    }
                    else
                    {
                        return SupplierResult.ChangedByOtherUser;
                    }
                }
                //Rejecting the application
                else if (updatedSupplierApp.ApplicationStatus.Equals(ApplicationStatus.Rejected.ToString()))
                {
                    //update db with rejection
                    int numRows = SupplierApplicationAccessor.UpdateSupplierApplication(oldSupplierApp, updatedSupplierApp);

                    if (numRows == 1)
                    {
                        return SupplierResult.Success;
                    }
                    else
                    {
                        return SupplierResult.ChangedByOtherUser;
                    }
                }
                else
                {
                    return SupplierResult.NotChanged;
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Concurrency Violation")
                {
                    return SupplierResult.ChangedByOtherUser;
                }
                return SupplierResult.DatabaseError;
            }
            catch (Exception)
            {
                return SupplierResult.DatabaseError;
            }
        }


        public SupplierResult ApproveSupplierApplication(SupplierApplication oldSupplierApp, SupplierApplication updatedSupplierApp, string userName, decimal supplyCost)
        {
            try
            {
                //Approving       
                //update db with approval
                int numRows = SupplierApplicationAccessor.UpdateSupplierApplication(oldSupplierApp, updatedSupplierApp, userName, supplyCost);

                if (numRows == 3)
                {
                    return SupplierResult.Success;
                }
                else
                {
                    return SupplierResult.ChangedByOtherUser;
                }                   
            }
            catch (Exception)
            {
                return SupplierResult.DatabaseError;
            }

        }

        public int deleteTestSupplier(Supplier testSupplier)
        {
            try
            {
                return SupplierAccessor.DeleteTestSupplier(testSupplier);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
