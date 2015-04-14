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
        public Supplier _currentSupplier = null;
        private ProductManager _myManager = new ProductManager();
        private List<ItemListing> _currentItemListings;
        public int currentListingCount = 0;
        public int currentGuestsCount = 0;


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
            if (!Page.IsPostBack)
            {
                //get # of listings for supplier
                try
                {
                    _currentItemListings = _myManager.RetrieveItemListingList();
                    var myList = _currentItemListings.Where(l => l.SupplierID == _currentSupplier.SupplierID);
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

            


        }
    }
}