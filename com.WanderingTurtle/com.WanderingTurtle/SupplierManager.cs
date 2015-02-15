using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle
{
    /// <summary>
    /// Manages CRUD Operations for data pertaining to Suppliers
    /// </summary>
    public class SupplierManager
    {
        public SupplierManager()
        {
            //default constructor
        }
        public Supplier RetrieveSupplier(string supplierID)
        {
            try
            {
                return SupplierAccessor.GetSupplier(supplierID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Supplier> RetrieveSupplierList()
        {
            try
            {
                return SupplierAccessor.GetSupplierList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddANewSupplier(Supplier supplierToAdd)
        {
            try
            {
                return SupplierAccessor.AddSupplier(supplierToAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditSupplier(Supplier oldSupplier, Supplier newSupplier)
        {
            try
            {
                return SupplierAccessor.UpdateSupplier(newSupplier, oldSupplier);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ArchiveSupplier(Supplier supplierToDelete)
        {
            try
            {
                return SupplierAccessor.DeleteSupplier(supplierToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Gets a single Supplier Application Record from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="applicationID">string ID of the application to be retrieved</param>
        /// <returns>SupplierApplication object</returns>
        /// Created by Matt Lapka 2/8/15
        public SupplierApplication RetrieveSupplierApplication(string applicationID)
        {
            try
            {
                return SupplierApplicationAccessor.GetSupplierApplication(applicationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Gets a list of Supplier Application Records from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <returns>List of SupplierApplication objects</returns>
        /// Created by Matt Lapka 2/8/15
        public List<SupplierApplication> RetrieveSupplierApplicationList()
        {
            try
            {
                return SupplierApplicationAccessor.GetSupplierApplicationList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Add a single Supplier Application Record to the database
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="newSupplier">Supplier object containing the information of the supplier to be added</param>
        /// <returns>int # of rows affected</returns>
        /// Created by Matt Lapka 2/8/15
        public int AddASupplierApplication(SupplierApplication newSupplierApp)
        {
            try
            {
                return SupplierApplicationAccessor.AddSupplierApplication(newSupplierApp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Updates a Supplier Application Record
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="newSupplier">Supplier object containing the new information of the supplier</param>
        /// <param name="oldSupplier">Supplier object containing the current information of the supplier to be matched to salve concurrency problems</param>
        /// <returns>int # of rows affected</returns>
        /// Created by Matt Lapka 2/8/15
        public int EditSupplierApplication(SupplierApplication oldSupplierApp, SupplierApplication newSupplierApp)
        {
            try
            {
                return SupplierApplicationAccessor.UpdateSupplierApplication(oldSupplierApp, newSupplierApp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
     
    }
}
