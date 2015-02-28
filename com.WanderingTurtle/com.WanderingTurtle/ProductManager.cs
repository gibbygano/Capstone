using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.BusinessLogic
{
    public class ProductManager
    {
        public ProductManager()
        {

        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Retireves a single "Lists" object using the ListsID
        /// </summary>
        /// <param name="itemListID">the itemListID in string format</param>
        /// <param name="supplierID">the supplierID in string format</param>
        /// <returns>the asked for Lists Object or throws the exception from the DAL if there is no data</returns>
        public Lists RetrieveLists(string supplierID, string itemListID)
        {
            try
            {
                return ListsAccessor.GetLists(supplierID, itemListID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Calls the Data Access Later to get a list of Active Lists
        /// </summary>
        /// <returns>An iterative list of active Lists objects</returns>
        public List<Lists> RetrieveListsList()
        {
            try
            {
                return ListsAccessor.GetListsList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Send a new Lists object to the Data Access Layer to be added to the database
        /// </summary>
        /// <param name="newLists">Lists object that contains the information to be added</param>
        /// <returns>int number of rows affect -- 1 if successful, 0 if not</returns>
        public int AddLists(Lists newLists)
        {
            try
            {
                return ListsAccessor.AddLists(newLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Updates a Lists object by sending the object in its original form and the object with the updated information
        /// </summary>
        /// <param name="newLists">the Lists object containing the new data</param>
        /// <param name="oldLists">the Lists object containing the original data</param>
        /// <returns>int number of rows affected-- should be 1</returns>
        public int EditLists(Lists newLists, Lists oldLists)
        {
            try
            {
                return ListsAccessor.UpdateLists(oldLists, newLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Archive a Lists so it does not appear in active searches
        /// Currently just for show as Lists does not contain an 'Active' field
        /// </summary>
        /// <param name="listsToDelete">Lists object containing the information to delete</param>
        /// <returns>int # of rows affected</returns>
        public int ArchiveLists(Lists listsToDelete)
        {
            try
            {
                return ListsAccessor.DeleteLists(listsToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Get a single ItemListing object from the database
        /// </summary>
        /// <param name="itemListID">the ItemListing ID in string format</param>
        /// <returns>a single ItemListing object meeting the criteria</returns>
        public ItemListing RetrieveItemListing(string itemListID)
        {
            try
            {
                return ItemListingAccessor.GetItemListing(itemListID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Calls the Data Access Later to get a list of Active ItemListings
        /// </summary>
        /// <returns>An iterative list of active ItemListing objects</returns>
        public List<ItemListing> RetrieveItemListingList()
        {
            try
            {
                return ItemListingAccessor.GetItemListingList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Send a new ItemListing object to the Data Access Layer to be added to the database
        /// </summary>
        /// <param name="newItemListing">ItemListing object that contains the information to be added</param>
        /// <returns>int number of rows affect -- 1 if successful, 0 if not</returns>
        public int AddItemListing(ItemListing newItemListing)
        {
            try
            {
                return ItemListingAccessor.AddItemListing(newItemListing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Updates an ItemListing object by sending the object in its original form and the object with the updated information
        /// </summary>
        /// <param name="newItemList">the ItemListing object containing the new data</param>
        /// <param name="oldItemList">the ItemListing object containing the original data</param>
        /// <returns>int number of rows affected-- should be 1</returns>
        public int EditItemListing(ItemListing newItemLists, ItemListing oldItemLists)
        {
            try
            {
                return ItemListingAccessor.UpdateItemListing(newItemLists, oldItemLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Archive an ItemListing so it does not appear in active searches
        /// </summary>
        /// <param name="itemListToDelete">ItemListing object containing the information to delete</param>
        /// <returns>int # of rows affected</returns>
        public int ArchiveItemListing(ItemListing itemListToDelete)
        {
            try
            {
                return ItemListingAccessor.DeleteItemListing(itemListToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
