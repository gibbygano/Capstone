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
        protected void Page_Load(object sender, EventArgs e)
        {
                //get # of listings for supplier
                try
                {
                    _currentItemListings = _myManager.RetrieveItemListingList();
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

        public IEnumerable<ItemListing> GetItemLists()
        {

                return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);
            
        }

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

        public IEnumerable<ItemListing> GetItemListsByDate()
        {
            try
            {
                DateTime From = DateTime.Parse(Request.Form["dateFrom"]);
                DateTime To = DateTime.Parse(Request.Form["dateTo"]);

                if (Request.Form["dateFrom"] != null && Request.Form["dateTo"] != null)
                {
                    return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > From && l.EndDate < To);
                }
                else
                {
                    return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);
                }
            }
            catch (Exception)
            {
                return _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID && l.StartDate > DateTime.Now);
            }
        } 

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

        public void btnDetails_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            int i = int.Parse(b.CommandArgument);
            GetNumbers(i);
            actions.Style.Add("display", "none");
            leftcontainer.Style.Add("display", "none");
            eventsDetails.Style.Add("display", "block");
            ViewMoneyDets.Style.Add("display", "none");
            //Response.Write("<script> showDetails(); </script>"); 
        }

        public void btnGoBack_Click(object sender, EventArgs e)
        {
            actions.Style.Add("display", "block");
            leftcontainer.Style.Add("display", "block");
            eventsDetails.Style.Add("display", "none");
            ViewMoneyDets.Style.Add("display", "none");
        }

        public void btnViewMoneyDets_Click(object sender, EventArgs e)
        {
            actions.Style.Add("display", "none");
            leftcontainer.Style.Add("display", "none");
            eventsDetails.Style.Add("display", "none");
            ViewMoneyDets.Style.Add("display", "block");
        }

        public void btnRefreshDate_Click(object sender, EventArgs e)
        {
            GetItemListsByDate();
            ListView1.DataBind();
        }

    }
}