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
         /// <summary>
        /// Gets a single Supplier  from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="supplierID">string ID of the application to be retrieved</param>
        /// <returns>Supplier object</returns>
        /// Created by Reece Maas 2/18/15
        public Supplier RetrieveSupplier(string supplierID)
        {
			return SupplierAccessor.GetSupplier(supplierID);
        }
         /// <summary>
        /// Gets a list of Suppliers  from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <returns>Supplier List</returns>
        /// Created by Reece Maas 2/18/15
        public List<Supplier> RetrieveSupplierList()
        {
			return SupplierAccessor.GetSupplierList();
        }
        /// <summary>
        /// Adds a single Supplier to the database
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="newSupplier">Supplier object containing the information of the supplier to be added</param>
        /// <returns>Supplier object</returns>
        /// Created by Reece Maas 2/18/15
        public int AddANewSupplier(Supplier supplierToAdd)
        {
			return SupplierAccessor.AddSupplier(supplierToAdd);
        }
        /// <summary>
        /// Updates a Supplier 
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="newSupplier">Supplier object containing the new information of the supplier</param>
        /// <param name="oldSupplier">Supplier object containing the current information of the supplier to be matched to salve concurrency problems</param>
        /// <returns>updated Supplier</returns>
        /// Created by Reece Maas 2/18/15
        public int EditSupplier(Supplier oldSupplier, Supplier newSupplier)
        {
			return SupplierAccessor.UpdateSupplier(newSupplier, oldSupplier);
        }
        public int ArchiveSupplier(Supplier supplierToDelete)
        {
			return SupplierAccessor.DeleteSupplier(supplierToDelete);
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
			return SupplierApplicationAccessor.GetSupplierApplication(applicationID);
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Gets a list of Supplier Application Records from the Data Access layer
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <returns>List of SupplierApplication objects</returns>
        public List<SupplierApplication> RetrieveSupplierApplicationList()
        {
			return SupplierApplicationAccessor.GetSupplierApplicationList();
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Add a single Supplier Application Record to the database
        /// Throws any exceptions caught by the DAL
        /// </summary>
        /// <param name="newSupplier">Supplier object containing the information of the supplier to be added</param>
        /// <returns>int # of rows affected</returns>
        public int AddASupplierApplication(SupplierApplication newSupplierApp)
        {
			return SupplierApplicationAccessor.AddSupplierApplication(newSupplierApp);
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
        public int EditSupplierApplication(SupplierApplication oldSupplierApp, SupplierApplication newSupplierApp)
        {
			return SupplierApplicationAccessor.UpdateSupplierApplication(oldSupplierApp, newSupplierApp);
        }
    }
}
