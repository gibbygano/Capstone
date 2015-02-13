using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Creates an ItemListing Object. Contains specific information about the event listing
    /// </summary>
    public class ItemListing
    {
        public int ItemListID { get; set; }
        public int EventID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int QuantityOffered { get; set; }
        public string ProductSize { get; set; } //not sure if we need this anymore

        public ItemListing()
        {
            //default constructor
        }

        public ItemListing(int itemListID, int eventID, DateTime startDate, DateTime endDate, decimal price, int quantityOffered, string productSize)
        {
            ItemListID = itemListID;
            EventID = eventID;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            QuantityOffered = quantityOffered;
            ProductSize = productSize;
        }
    }
}
