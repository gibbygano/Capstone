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
        IEnumerable<ItemListing> _listedLists;
        public bool loggedIn = false;
        public List<Supplier> _suppliers;
        public List<Event> _events;
        public Supplier _currentSupplier = null;
        private decimal minPrice = 0;
        private int supplierID;

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
                    //nothing
                }
            }
        }

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

        public IEnumerable<ItemListing> GetLists()
        {
            testLogin();
            try
            {
                var list = _myManager.RetrieveItemListingList(supplierID);

                _listedLists = list;
                return _listedLists;
            }
            catch (Exception e)
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving List" + e.Message + "\")</SCRIPT>");
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

        private String addError(String list, String text)
        {
            return list.Length > 0 ? list + ", " + text : text;
        }
        public void UpdateList(int ItemListID)
        {
            string errorText = "";
            lblError.Text = "";

            try
            {
                ItemListing myList = _listedLists.Where(ev => ev.ItemListID == ItemListID).FirstOrDefault();

                ItemListing newList = new ItemListing(myList.ItemListID, myList.EventID, myList.SupplierID, myList.StartDate, myList.EndDate, myList.Price, myList.MaxNumGuests, myList.MinNumGuests, myList.CurrentNumGuests);

                newList.StartDate = DateTime.Parse(Request.Form["start"]);
                newList.EndDate = DateTime.Parse(Request.Form["end"]);
                newList.Price = decimal.Parse(Request.Form["price"]);
                newList.MaxNumGuests = int.Parse(Request.Form["max"]);
                newList.MinNumGuests = int.Parse(Request.Form["min"]);

                listResult result;

                if (!Validator.ValidateDecimal(newList.Price.ToString("G"), minPrice))
                {
                    errorText = addError(errorText, "Please add a valid, positive price");
                }
                if (!Validator.ValidateDateTime(newList.StartDate.ToString()))
                {
                    errorText = addError(errorText, "Please add a valid start date");
                }
                if (!Validator.ValidateDateTime(newList.EndDate.ToString(), newList.StartDate))
                {
                    errorText = addError(errorText, "Please add a valid end date after your start date");
                }
                if (!Validator.ValidateInt(newList.MaxNumGuests.ToString(), newList.MinNumGuests))
                {
                    errorText = addError(errorText, "Please add a valid max number of guests higher than your minimum");
                }
                if (!Validator.ValidateInt(newList.MinNumGuests.ToString(), 0, newList.MaxNumGuests))
                {
                    errorText = addError(errorText, "Please add a valid min number of guests lower than your maximum, higher than zero");
                }

                if (myList != null && errorText.Length == 0)
                {
                    result = _myManager.EditItemListing(newList, myList);
                    return;
                }
                else
                {
                    lblError.Text = errorText;
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

                listResult result = _myManager.ArchiveItemListing(myList);
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