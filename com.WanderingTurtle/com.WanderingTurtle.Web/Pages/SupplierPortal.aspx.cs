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
        private bool getDetails;
        private static bool showDateError = false;
        Label errorLabel;

        /// <summary>
        /// Matt Lapka
        /// Created 2015/04/13
        /// Sets login information based on session data
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            errorLabel = (Label)Master.FindControl("lblErrorMessage");
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
        /// Matt Lapka
        /// Created 2015/04/13
        /// fills the current suppliers item listing lists and gets the number of guests 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showDateError = false;
            }
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
        /// Matt Lapka
        /// Created 2015/04/13
        /// returns a list of items listings where the date is more recent than today
        /// </summary>
        /// <returns>IEnumerable of item listings for the current supplier</returns>
        public IEnumerable<ItemListing> GetItemLists()
        {
            return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/04/14
        /// This method will get a list of booking numbers based on a supplied ItemListID and will data bind that list to lvDetails or it will put up an error message
        /// </summary>
        /// <param name="itemListID"> Item listingID for the desired item</param>
        public void GetNumbers(int itemListID)
        {
            if (Page.IsPostBack)
            {
                if (getDetails)
                {
                    try
                    {
                        var list = _myBookingManager.RetrieveBookingNumbers(itemListID);
                        lvDetails.DataSource = list;
                        lvDetails.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Willam Fritz 
        /// Created 2015/04/24
        /// returns a list of items listings where the date is more recent than today if there is no date selected, otherwise it will return a list within the specifed date range.
        /// will also set session variables to the selected dates, which will be used in the aspx page
        /// </summary>
        /// <returns>IEnumerable of item listings for the current supplier within a specified date range</returns>
        public IEnumerable<ItemListing> GetItemListsByDate()
        {
            lblDateError.Text = "";
            try
            {
                DateTime From;
                DateTime To;
                if (DateTime.TryParse(Request.Form["dateFrom"], out From))
                {
                    Session["dateFrom"] = From.ToShortDateString();
                }
                else
                {
                    if (Request.Form["dateFrom"] != "")
                    {
                        //only show this if the correct page is being shown
                        if (showDateError)
                        {
                            lblMessage.Text = "Invalid Date";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                        }
                    }
                    Session["dateFrom"] = DateTime.Now.ToShortDateString();
                    Session["dateTo"] = null;
                    return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);
                }
                if (DateTime.TryParse(Request.Form["dateTo"], out To))
                {
                    Session["dateTo"] = To.ToShortDateString();
                }
                else
                {
                    if (Request.Form["dateFrom"] != "")
                    {
                        if (showDateError)
                        {
                            lblMessage.Text = "Invalid Date";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                        }
                    }
                    Session["dateFrom"] = DateTime.Now.ToShortDateString();
                    Session["dateTo"] = null;
                    return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);
                }

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
        /// Willam Fritz 
        /// Created 2015/04/24
        /// This method will calculate the total amount of money that the supplier will 
        /// receive after the events within the selected date range are completed and paid for
        /// </summary>
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
        /// Matt Lapka
        /// Created 2015/04/14
        /// This method will get the ItemListID from the buttons CommandArgument 
        /// then will call GetNumbers with the ItemListID. It also displays the 
        /// Events Details div to visible and changes the others to invisible
        /// </summary>
        /// <remarks>
        /// William Fritz
        /// updated 2015/04/17
        /// adding printing capabilities
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnDetails_Click(object sender, EventArgs e)
        {
            getDetails = true;
            var b = (Button)sender;
            int i = int.Parse(b.CommandArgument);
            GetNumbers(i);
            actions.Style.Add("display", "none");
            leftcontainer.Style.Add("display", "none");
            eventsDetails.Style.Add("display", "block");
            ViewMoneyDets.Style.Add("display", "none");
        }

        /// <summary>
        /// Willam Fritz 
        /// Created 2015/04/24
        /// Displays the left container div to visible and changes the others to invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnGoBack_Click(object sender, EventArgs e)
        {
            showDateError = false;
            Control cc = Master.FindControl("ErrorMess");
            cc.Visible = false;
            actions.Style.Add("display", "block");
            leftcontainer.Style.Add("display", "block");
            eventsDetails.Style.Add("display", "none");
            ViewMoneyDets.Style.Add("display", "none");
        }

        /// <summary>
        /// Willam Fritz 
        /// Created 2015/04/24
        /// displays the view money details div to visible and changes the others to invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnViewMoneyDets_Click(object sender, EventArgs e)
        {
            showDateError = true;
            getDetails = true;
            actions.Style.Add("display", "none");
            leftcontainer.Style.Add("display", "none");
            eventsDetails.Style.Add("display", "none");
            ViewMoneyDets.Style.Add("display", "block");
        }

        /// <summary>
        /// Willam Fritz 
        /// Created 2015/04/24
        /// refreshes the suppliers Item list by calling the GetItemListsByDate method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnRefreshDate_Click(object sender, EventArgs e)
        {
            GetItemListsByDate();
            ListView1.DataBind();
        }
    }
}