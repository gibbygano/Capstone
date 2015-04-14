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
        List<ItemListing> _listedLists;
        public List<Supplier> _suppliers;
        public List<Event> _events;
        public DateTime startTime;
        public DateTime endTime;
        public int eventItemID;
        public double price;
        public int supplierID;
        public int currentNumberofGuests;
        public int maxNumberofGuests;
        public int minNumberofGuests;
        private bool loggedIn = false;
        public Supplier _currentSupplier = null;


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
            if (!Page.IsPostBack)
            {
                _suppliers = _supplierManager.RetrieveSupplierList();
                _events = _eventManager.RetrieveEventList();
            }
        }

        public IEnumerable<ItemListing> GetLists()
        {
            try
            {
                _listedLists = _myManager.RetrieveItemListingList();
                return _listedLists;
            }
            catch (Exception e)
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving List\")</SCRIPT>");
                return null;
            }
        }
        private void bindData()
        {
            lvLists.DataSource = _listedLists;
            lvLists.DataBind();
        }
      
        public void FormValidate(int eventItemID)
        {
           
        }
        public void UpdateList(int ItemListID)
        {
            bool stop = false;
            int errorCount = 0;

            try
            {
                ItemListing myList = _listedLists.Where(ev => ev.ItemListID == ItemListID).FirstOrDefault();

                ItemListing newList = new ItemListing(myList.ItemListID, myList.EventID, myList.SupplierID, myList.StartDate, myList.EndDate, myList.Price, myList.MaxNumGuests, myList.MinNumGuests, myList.CurrentNumGuests);

                newList.StartDate = DateTime.Parse(Request.Form["start"]);
                newList.EndDate = DateTime.Parse(Request.Form["end"]);
                newList.Price = decimal.Parse(Request.Form["price"]);
                newList.MaxNumGuests = int.Parse(Request.Form["max"]);
                newList.MinNumGuests = int.Parse(Request.Form["min"]);

                //TODO: eventID, SupplierID
                lblError.Text = "";
                ProductManager.listResult result;
                if (myList != null)
                {
                    result = _myManager.EditItemListing(newList, myList);
                    return;
                }
            }
            catch (Exception ex) 
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Updating Event" + ex.Message + "\" )</SCRIPT>");
            }
        }
     
        private void showError(string errorMessage)
        {
            lblError.Text = errorMessage;
        }
        public void DeleteList(int ItemListID)
        {
            try
            {
                ItemListing myList = _listedLists
                .Where(e => e.ItemListID == ItemListID).FirstOrDefault();

                ProductManager.listResult result = _myManager.ArchiveItemListing(myList);
            }
            catch (Exception)
            {

                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Deleting Event\")</SCRIPT>");
            }
        }
        public void InsertList()
        {

        }

        protected void lvLists_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            bindData();
        }
    }

}