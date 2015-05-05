using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
    public enum listResult
    {
        //item could not be found
        NotFound = 0,

        //new event could not be added
        NotAdded,

        NotChanged,

        //worked
        Success,

        //Can change record
        OkToEdit,

        //concurrency error
        ChangedByOtherUser,

        DatabaseError,
        //can't change date with guests signed up
        NoDateChange,
        //max guest can't be smaller than # signed up already
        MaxSmallerThanCurrent,
        //can't change price once guests are signed up
        NoPriceChange,
        //start date can't be after end date
        StartEndDateError,
        //start date can't be in past
        DateInPast
    }

    public class ProductManager
    {
        public ProductManager()
        {
        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Get a single ItemListing object from the database
        /// </summary>
        /// <param name="itemListID">the ItemListing ID in string format</param>
        /// <returns>A single ItemListing object meeting the criteria</returns>
        public ItemListing RetrieveItemListing(string itemListID)
        {
            try
            {
                return ItemListingAccessor.GetItemListing(itemListID);
            }
            catch (Exception ex)
            {
                throw;
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
                double cacheExpirationTime = 5; //how long the cache should live (minutes)
                var now = DateTime.Now;
                if (DataCache._currentItemListingList == null)
                {
                    //data hasn't been retrieved yet. get data, set it to the cache and return the result.
                    DataCache._currentItemListingList = ItemListingAccessor.GetItemListingList();
                    DataCache._ItemListingListTime = now;
                }
                else
                {
                    //check time. If less than 5 min, return cache
                    if (now > DataCache._ItemListingListTime.AddMinutes(cacheExpirationTime))
                    {
                        //get new list from DB
                        DataCache._currentItemListingList = ItemListingAccessor.GetItemListingList();
                        DataCache._ItemListingListTime = now;
                    }
                }
                return DataCache._currentItemListingList;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Send a new ItemListing object to the Data Access Layer to be added to the database
        /// </summary>
        /// <param name="newItemListing">ItemListing object that contains the information to be added</param>
        /// <returns>An int reflecting the number of rows affected -- 1 if successful, 0 if not</returns>
        public listResult AddItemListing(ItemListing newItemListing)
        {
            try
            {
                if (ItemListingAccessor.AddItemListing(newItemListing) == 1)
                {
                    DataCache._currentItemListingList = ItemListingAccessor.GetItemListingList();
                    return listResult.Success;
                }
                return listResult.NotAdded;
            }
            catch (Exception)
            {
                return listResult.DatabaseError;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Updates an ItemListing object by sending the object in its original form and the object with the updated information
        /// </summary>
        /// <param name="newItemList">The ItemListing object containing the new data</param>
        /// <param name="oldItemList">The ItemListing object containing the original data</param>
        /// <returns>An int reflecting the number of rows affected-- should be 1</returns>
        public listResult EditItemListing(ItemListing newItemLists, ItemListing oldItemLists)
        {
            if (newItemLists.CurrentNumGuests > 0)
            {
                if (newItemLists.StartDate != oldItemLists.StartDate || newItemLists.EndDate != oldItemLists.EndDate)
                {
                    return listResult.NoDateChange;
                }

                if (newItemLists.MaxNumGuests < newItemLists.CurrentNumGuests)
                {
                    return listResult.MaxSmallerThanCurrent;
                }
                if(newItemLists.Price != oldItemLists.Price)
                {
                    return listResult.NoPriceChange;
                }
            }

            if (newItemLists.StartDate > newItemLists.EndDate)
            {
                return listResult.StartEndDateError;
            }
            if (newItemLists.StartDate < DateTime.Now)
            {
                return listResult.DateInPast;
            }

            try
            {
                if (ItemListingAccessor.UpdateItemListing(newItemLists, oldItemLists) == 1)
                {
                    DataCache._currentItemListingList = ItemListingAccessor.GetItemListingList();
                    return listResult.Success;
                }
                return listResult.NotChanged;
            }
            catch (Exception)
            {
                return listResult.DatabaseError;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/26
        /// Added logic to determine if an event can be archived
        /// </summary>
        /// <param name="eventID">The eventID to crossreference against</param>
        /// <returns>An Enumerated result depicting OkToArchive or CannotArchive</returns>
        public ResultsArchive CheckToArchiveEvent(int eventID)
        {
            List<ItemListing> list = RetrieveItemListingList();
            int count = list.Count(item => item.EventID == eventID && item.StartDate > DateTime.Now);

            return count == 0 ? ResultsArchive.OkToArchive : ResultsArchive.CannotArchive;
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/14
        /// Archive an ItemListing so it does not appear in active searches
        /// </summary>
        /// <param name="itemListToDelete">ItemListing object containing the information to delete</param>
        /// <returns>An int reflecting number of rows affected</returns>
        public listResult ArchiveItemListing(ItemListing itemListToDelete)
        {
            try
            { 
                if (itemListToDelete.CurrentNumGuests == 0)
                {
                    if (ItemListingAccessor.DeleteItemListing(itemListToDelete) == 1)
                    {
                        DataCache._currentItemListingList = ItemListingAccessor.GetItemListingList();
                        return listResult.Success;
                    }
                }           
                return listResult.NotChanged;
            }
            catch (Exception)
            {
                return listResult.DatabaseError;
            }
        }

        /// <summary>
        /// Justin Pennington
        /// Created:  2015/04/15
        /// Searches for a string the user inputs
        /// </summary>
        /// <param name="inSearch">The string to be searched for</param>
        /// <returns>
        /// A List object containing ItemListing objects which contain the string being searched for
        /// or an empty list if none are found.
        /// </returns>
        public List<ItemListing> SearchItemLists(string inSearch)
        {
            if (!inSearch.Equals("") && !inSearch.Equals(null))
            {
                //Lambda Version
                //return myTempList.AddRange(DataCache._currentEventList.Where(s => s.EventItemName.ToUpper().Contains(inSearch.ToUpper())).Select(s => s));
                //LINQ version
                List<ItemListing> myTempList = new List<ItemListing>();
                myTempList.AddRange(
                  from inItem in DataCache._currentItemListingList
                  where inItem.EventName.ToUpper().Contains(inSearch.ToUpper()) || inItem.SupplierName.ToUpper().Contains(inSearch.ToUpper())
                  select inItem);
                return myTempList;

                //Will empty the search list if nothing is found so they will get feedback for typing something incorrectly
            }
            return DataCache._currentItemListingList;
        }

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/04/16
        /// returns a list of listing from a specific supplier
        /// </summary>
        /// <param name="supplierID">SupplierID to cross reference against</param>
        /// <returns>An IEnumerable object of ItemListing objects</returns>
        public IEnumerable<ItemListing> RetrieveItemListingList(int supplierID)
        {
            try
            {
                double cacheExpirationTime = 5; //how long the cache should live (minutes)
                var now = DateTime.Now;
                if (DataCache._currentItemListingList == null)
                {
                    //data hasn't been retrieved yet. get data, set it to the cache and return the result.
                    DataCache._currentAllItemListingList = ItemListingAccessor.GetAllItemListingList();
                    DataCache._AllItemListingListTime = now;
                }
                else
                {
                    //check time. If less than 5 min, return cache
                    if (now > DataCache._AllItemListingListTime.AddMinutes(cacheExpirationTime))
                    {
                        //get new list from DB
                        DataCache._currentAllItemListingList = ItemListingAccessor.GetAllItemListingList();
                        DataCache._AllItemListingListTime = now;
                    }
                }
                return DataCache._currentAllItemListingList.Where(l => l.SupplierID == supplierID);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}