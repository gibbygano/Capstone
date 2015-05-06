using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.Web;

namespace com.WanderingTurtle.Web.Pages
{
    public partial class ViewItemListing : System.Web.UI.Page
    {
        ProductManager _myManager = new ProductManager();
        EventManager _eventManager = new EventManager();
        SupplierManager _supplierManager = new SupplierManager();
        static IEnumerable<ItemListing> _listedLists;
        public bool loggedIn = false;
        public List<Supplier> _suppliers;
        public List<Event> _events;
        public Supplier _currentSupplier = null;
        private decimal minPrice = 0;
        private int supplierID;

        /// <summary>
        /// Matt Lapka
        /// Created 2015/04/16
        /// checks login information then fetches data for supplier listings
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            testLogin();

            if (!Page.IsPostBack)
            {
                try
                {
                    _suppliers = _supplierManager.RetrieveSupplierList();
                    _events = _eventManager.RetrieveEventList();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Event fetching data: " + ex.Message;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                }
            }
            if (Page.IsPostBack)
            {
                var requestTarget = this.Request["__EVENTTARGET"];
                var requestArgs = this.Request["__EVENTARGUMENT"];

                if (requestTarget == "delete")
                {
                    DeleteList(int.Parse(requestArgs));
                    return;
                }
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/04/16
        /// Sets login information based on session data
        /// </summary>
        private void testLogin()
        {
            try
            {
                //attempt to get session value if they are logged in
                loggedIn = (bool)Session["loggedin"];
                _currentSupplier = (Supplier)Session["user"];
                supplierID = _currentSupplier.SupplierID;
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
        /// Created 2015/04/10
        /// Gets listing data from BLL based on supplier id
        /// </summary>
        /// <returns>List of the Item listings for that supplier</returns>
        public IEnumerable<ItemListing> GetLists()
        {
            testLogin();
            try
            {
                var list = _myManager.RetrieveItemListingList(supplierID);

                _listedLists = list.Where(l => l.StartDate > DateTime.Now);
                return _listedLists;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Event fetching event listings: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                return null;
            }
        }

        /// <summary>
        /// Kelsey Blount
        /// created  2015/04/10
        /// Binds data for the list view
        /// </summary>
        private void bindData()
        {
            lvLists.DataSource = _listedLists;
            lvLists.DataBind();
        }

        /// <summary>
        /// Kelsey Blount
        /// created  2015/04/16
        /// Adds error text
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private String addError(String list, String text)
        {
            return list.Length > 0 ? list + "\n" + text : text;
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/04/16
        /// Handles the updating of a listing.  includes input validation, messaging and necessary session variables
        /// </summary>
        public void UpdateList()
        {
            string errorText = "";
            lblError.Text = "";
            int ItemListID = int.Parse(Request.Form["itemID"]);

            try
            {
                ItemListing myList = _listedLists.Where(ev => ev.ItemListID == ItemListID).FirstOrDefault();

                ItemListing newList = new ItemListing(myList.ItemListID, myList.EventID, myList.SupplierID, myList.StartDate, myList.EndDate, myList.Price, myList.MaxNumGuests, myList.MinNumGuests, myList.CurrentNumGuests);

                DateTime start;
                DateTime end;
                if (DateTime.TryParse(Request.Form["start"], out start))
                {
                    newList.StartDate = start;
                }
                else
                {
                    errorText = addError(errorText, "<li>Invalid Start Date. Please use the Calendar.</li>");
                }
                if (DateTime.TryParse(Request.Form["end"], out end))
                {
                    newList.EndDate = end;
                }
                else
                {
                    errorText = addError(errorText, "<li>Invalid End Date. Please use the Calendar.</li>");

                }
                newList.Price = decimal.Parse(Request.Form["price"]);
                newList.MaxNumGuests = int.Parse(Request.Form["max"]);
                newList.MinNumGuests = 0;

                listResult result;

                if (!newList.Price.ToString("G").ValidateDecimal(minPrice))
                {
                    errorText = addError(errorText, "<li>Please add a valid, positive price</li>");
                }
                if (!newList.StartDate.ToString().ValidateDateTime())
                {
                    errorText = addError(errorText, "<li>Please add a valid start date</li>");
                }
                if (!newList.EndDate.ToString().ValidateDateTime(newList.StartDate))
                {
                    errorText = addError(errorText, "<li>Please add a valid end date after your start date</li>");
                }
                if (!newList.MaxNumGuests.ToString().ValidateInt(newList.MinNumGuests))
                {
                    errorText = addError(errorText, "<li>Please add a valid max number of guests greater than zero.</li>");
                }

                if (myList != null && errorText.Length == 0)
                {
                    result = _myManager.EditItemListing(newList, myList);
                    if (result == listResult.NoDateChange)
                    {
                        errorText = addError(errorText, "<li>You cannot change the date after guests have signed up!</li>");
                        
                    }
                    if (result == listResult.NoPriceChange)
                    {
                        errorText = addError(errorText, "<li>You cannot change the price after guests have signed up!</li>");

                    }
                    if (result == listResult.MaxSmallerThanCurrent)
                    {
                        errorText = addError(errorText, "<li>You cannot change the Max Number of Guests to be lower than the number signed up!</li>");
                    }
                    if (result == listResult.StartEndDateError)
                    {
                        errorText = addError(errorText, "<li>Start Date cannot be after End Date!</li>");
                    }
                    if (result == listResult.DateInPast)
                    {
                        errorText = addError(errorText, "<li>Date cannot be in the past!</li>");
                    }
                    if (errorText.Length > 0)
                    {
                        lblMessage.Text = "Please see the following errors: <ul>" + errorText + "</ul>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                    }
                    if (result==listResult.Success)
                    {
                        lblMessage.Text = "Event Updated!";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                    }

                    return;
                }
                else
                {
                    lblMessage.Text = "Please see the following errors: <ul>" + errorText + "</ul>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Event updating listing: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
            }
        }

        /// <summary>
        /// Kelsey Blount
        /// created  2015/04/10
        /// Shows error text
        /// </summary>
        /// <param name="errorMessage"></param>
        private void showError(string errorMessage)
        {
            lblError.Text = errorMessage;
        }

        /// <summary>
        /// Kelsey Blount
        /// created  2015/04/10
        /// Handles archival of a listing
        /// </summary>
        /// <remarks>
        /// Matt Lapka
        /// Updated 2015/04/15
        /// error messages added
        /// </remarks>
        /// <param name="ItemListID">item ID for the listing</param>
        public void DeleteList(int ItemListID)
        {
            try
            {
                ItemListing myList = _listedLists
                .Where(e => e.ItemListID == ItemListID).FirstOrDefault();

                listResult result = _myManager.ArchiveItemListing(myList);
                if (result==listResult.Success)
                {
                    lblMessage.Text = "Event listing successfully deleted.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                }
                else
                {
                    lblMessage.Text = "Cannot delete listing with registered guests.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Event deleting listing: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
            }
        }

        /// <summary>
        /// Kelsey Blount
        /// created  2015/04/10
        /// Binds data for the listing view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLists_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            bindData();
        }
    }
}