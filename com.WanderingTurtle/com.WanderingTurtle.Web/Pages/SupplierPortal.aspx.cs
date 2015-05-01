using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.Web.Pages
{
    public partial class SupplierPortal : System.Web.UI.Page
    {
        private bool loggedIn = false;
        public Supplier _currentSupplier;
        private ProductManager _myManager = new ProductManager();
        private BookingManager _myBookingManager = new BookingManager();
        private static List<ItemListing> _currentItemListings;
        public int currentListingCount = 0;
        public int currentGuestsCount = 0;
        public int current = 0;

        /// <summary>
        /// Sets up the page variables and redirects you to the login page if you havent logged in
        /// </summary>
        /// <remarks>
        /// Created by Matt Lapka
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            try
            {
                //attempt to get session value if they are logged in
                loggedIn = (bool)Session["loggedin"];
                _currentSupplier = (Supplier)Session["user"];
            }
            catch (Exception)
            {
                //if it fails, the user must not have logged in on this
                //session yet, so set it to false
                Session["loggedIn"] = false;
                //send them to the login page
                Response.Redirect("~/login");
            }
            if (!loggedIn)
            {
                //if not logged in, send them to login page
                Response.Redirect("~/login");
            }
        }

        /// <summary>
        /// fills the current suppliers item listing lists and gets the number of guests 
        /// </summary>
        /// <remarks>
        /// Created by Matt Lapka
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
                //get # of listings for supplier
                try
                {
                    _currentItemListings = _myManager.RetrieveItemListingList(_currentSupplier.SupplierID).ToList();
                    var myList = _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);
                    currentListingCount = myList.Count();

                    //get # of guests signed up for the listings
                    foreach (var listing in myList)
                    {
                        currentGuestsCount += listing.CurrentNumGuests;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
        }

        /// <summary>
        /// returns a list of items listings where the date is more recent than today
        /// </summary>
        /// <remarks>
        /// Created by Matt Lapka
        /// </remarks>
        /// <returns>IEnumerable of item listings for the current supplier</returns>
        public IEnumerable<ItemListing> GetItemLists()
        {

                return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);
            
        }

        /// <summary>
        /// This method will get a list of booking numbers based on a supplied ItemListID and will data bind that list to lvDetails or it will put up an error message
        /// </summary>
        /// <remarks>
        /// Created by Matt Lapka
        /// </remarks>
        /// <param name="itemListID"></param>
        public void GetNumbers(int itemListID)
        {
            if (Page.IsPostBack)
            {
                try
                {
                    var list = _myBookingManager.RetrieveBookingNumbers(itemListID);
                    lvDetails.DataSource = list;
                    lvDetails.DataBind();
                }
                catch (Exception ex)
                {
                    Label errorLabel = (Label)Master.FindControl("lblErrorMessage");
                    errorLabel.Text = "Error: " + ex.Message;
                    Control c = Master.FindControl("ErrorMess");
                    c.Visible = true;
                    return;
                }
                Control cc = Master.FindControl("ErrorMess");
                cc.Visible = false;
            }
        }

        /// <summary>
        /// returns a list of items listings where the date is more recent than today if there is no date selected, otherwise it will return a list within the specifed date range.
        /// will also set session variables to the selected dates, which will be used in the aspx page
        /// </summary>
        /// <remarks>
        /// Created by Willam Fritz 
        /// </remarks>
        /// <returns>IEnumerable of item listings for the current supplier within a specified date range</returns>
        public IEnumerable<ItemListing> GetItemListsByDate()
        {
            try
            {
                DateTime From = DateTime.Parse(Request.Form["dateFrom"]);
                DateTime To = DateTime.Parse(Request.Form["dateTo"]);

                Session["dateFrom"] = From.ToShortDateString();
                Session["dateTo"] = To.ToShortDateString();

                if (Request.Form["dateFrom"] != null && Request.Form["dateTo"] != null)
                {
                    return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > From && l.EndDate < To);
                }
                else
                {
                    Session["dateFrom"] = DateTime.Now.ToShortDateString();
                    Session["dateTo"] = null;
                    return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now); 
                }
            }
            catch (Exception)
            {
                Session["dateFrom"] = DateTime.Now.ToShortDateString();
                Session["dateTo"] = null;
                return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);             
            }
        } 

        /// <summary>
        /// This method will calculate the total amount of money that the supplier will get after the events within the selected date range are compleated and payed for
        /// </summary>
        /// <remarks>
        /// Created by William Fritz
        /// </remarks>
        /// <returns>The amount of money the supplier will earn</returns>
        public decimal getTotal()
        {
            decimal _extendedSum = 0;

            IEnumerable<ItemListing> currentListings = GetItemListsByDate();

            foreach (ItemListing i in currentListings)
            {
                _extendedSum += (i.Price * i.CurrentNumGuests);
            }

            return _extendedSum;
        }

        /// <summary>
        /// This method will get the ItemListID from the buttons CommandArgument then will call GetNumbers with the ItemListID. It also displays the Events Details div to visible and changes the others to invisible
        /// </summary>
        /// <remarks>
        /// Created by Matt Lapka
        /// Edited by William Fritz
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnDetails_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            int i = int.Parse(b.CommandArgument);
            GetNumbers(i);
            actions.Style.Add("display", "none");
            leftcontainer.Style.Add("display", "none");
            eventsDetails.Style.Add("display", "block");
            ViewMoneyDets.Style.Add("display", "none");
        }

        /// <summary>
        /// Displays the left container div to visible and changes the others to invisible
        /// </summary>
        /// <remarks>
        /// Created by William Fritz
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnGoBack_Click(object sender, EventArgs e)
        {
            actions.Style.Add("display", "block");
            leftcontainer.Style.Add("display", "block");
            eventsDetails.Style.Add("display", "none");
            ViewMoneyDets.Style.Add("display", "none");
        }

        /// <summary>
        /// displays the view money details div to visible and changes the others to invisible
        /// </summary>
        /// <remarks>
        /// Created by William Fritz
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnViewMoneyDets_Click(object sender, EventArgs e)
        {
            actions.Style.Add("display", "none");
            leftcontainer.Style.Add("display", "none");
            eventsDetails.Style.Add("display", "none");
            ViewMoneyDets.Style.Add("display", "block");
        }

        /// <summary>
        /// will refresh the suppliers Item list by calling the GetItemListsByDate method
        /// </summary>
        /// <remarks>
        /// Created by William Fritz
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnRefreshDate_Click(object sender, EventArgs e)
        {
            GetItemListsByDate();
            ListView1.DataBind();
        }

    }
}