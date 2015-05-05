using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.Web;

namespace com.WanderingTurtle.Web.Pages
{
    public partial class SupplierViewEvents : System.Web.UI.Page
    {
        EventManager _myManager = new EventManager();
        ProductManager _myprodMan = new ProductManager();
        List<Event> _listedEvents;
        public List<EventType> _eventTypes;
        public string current;
        public string transport;
        public string onsite;
        public string nameError = "";
        public string descError = "";
        private bool loggedIn = false;
        private bool search = false;
        private string searchTerm = "";

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            try
            {
                //attempt to get session value if they are logged in
                loggedIn = (bool)Session["loggedin"];
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

            if (Page.IsPostBack)
            {
                //retrieve event types for update panel
                try
                {
                    _eventTypes = _myManager.RetrieveEventTypeList();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Event fetching data: " + ex.Message;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                }
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

            }
        }
        private void bindData()
        {
            lvEvents.DataSource = _listedEvents;
            lvEvents.DataBind();
        }
        public IEnumerable<Event> GetEvents()
        {
            lblError.Text = "";
            try
            {
                _listedEvents = _myManager.RetrieveEventList();
                foreach (Event x in _listedEvents)
                {
                    //sets string values for trasportation and onsite properties
                    x.setFields();
                }
                if (IsPostBack)
                {
                    return _listedEvents.Where(e => e.EventItemName.ToLower().Contains(searchTerm) || e.Description.ToLower().Contains(searchTerm));
                }
                return _listedEvents;
            }
            catch (Exception ex)
            {

                lblMessage.Text = "Event fetching data: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                return null;
            }
        }
        public void FormValidate(int eventItemID)
        {

        }
        public void UpdateEvent(int eventItemID)
        {
            int errorCount = 0;
            try
            {
                Event myEvent = _listedEvents
                .Where(ev => ev.EventItemID == eventItemID).FirstOrDefault();

                Event newEvent = new Event(myEvent.EventItemID, myEvent.EventItemName, myEvent.Transportation, myEvent.EventTypeID, myEvent.OnSite, myEvent.ProductID, myEvent.Description, myEvent.Active);

                if (String.Format("{0}", Request.Form["name"]).Trim().ValidateAlphaNumeric(1, 255))
                {
                    newEvent.EventItemName = String.Format("{0}", Request.Form["name"]).Trim();

                }
                else
                {
                    errorCount++;
                    showError("You must enter a valid Event Name!");
                    return;


                }
                //Created programatically so don't need to be validated
                newEvent.EventTypeID = int.Parse(Request.Form["type"]);
                newEvent.Transportation = bool.Parse(Request.Form["transport"]);
                newEvent.OnSite = bool.Parse(Request.Form["onsite"]);
                if (String.Format("{0}", Request.Form["description"]).Trim().ValidateCompanyName(1, 255))
                {
                    newEvent.Description = String.Format("{0}", Request.Form["description"]).Trim();
                }
                else
                {
                    errorCount++;
                    showError("You must enter a valid Description!" + String.Format("{0}", Request.Form["description"]).Trim());
                    return;
                }


                lblError.Text = "";
                EventManager.EventResult result;
                if (myEvent != null)
                {
                    result = _myManager.EditEvent(myEvent, newEvent);
                    return;
                }



            }
            catch (Exception ex)
            {
                lblMessage.Text = "Event Updating Event: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
            }

        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/03/07
        /// 
        /// Sets the error message displayed on the page.
        /// Updated 2015/05/03
        /// Now uses jQuery Dialog boxes
        /// </summary>
        /// <param name="errorMessage"></param>
        private void showError(string errorMessage)
        {
            //set the message
            lblMessage.Text = errorMessage;
            //call the script to open the dialog box and show the message
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
        }
        public void DeleteEvent(int eventItemID)
        {
            //no longer used
            try
            {
                Event myEvent = _listedEvents
                .Where(e => e.EventItemID == eventItemID).FirstOrDefault();

                EventManager.EventResult result = _myManager.ArchiveAnEvent(myEvent);
            }
            catch (Exception)
            {

                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Deleting Event\")</SCRIPT>");
            }
        }

        protected void lvEvents_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //this.dpEvents.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData();
        }

        protected void lvEvents_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            if (!e.NewValues["EventItemName"].ToString().ValidateAlphaNumeric(1, 255))
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"You must ender a valid name!\")</SCRIPT>");
                e.Cancel = true;
            }

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            //gets the info about the event from the button clicks
            var b = (Button)sender;
            int itemID = int.Parse(b.CommandArgument);
            //get exact event info
            var myEvent = _listedEvents.Where(l => l.EventItemID == itemID).FirstOrDefault();
            //set session variable
            Session["Event"] = myEvent;
            //hide list of events
            theLists.Style.Add("display", "none");
            //show add listing form
            addListing.Style.Add("display", "block");
            lblEventName.Text = myEvent.EventItemName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblAddError.Text = "";
            Event myEvent;
            Supplier mySupplier;
            try
            {
                myEvent = (Event)Session["Event"];
                mySupplier = (Supplier)Session["user"];
            }
            catch (Exception)
            {
                return;
            }
            string errors = "";
            DateTime start;
            DateTime end;
            //validate form
            if (String.IsNullOrEmpty(Request.Form["startdate"]))
            {
                errors += "<li>You must enter a valid starting date!</li>";
            }
            if (String.IsNullOrEmpty(Request.Form["enddate"]))
            {
                errors += "<li>You must enter a valid ending date!</li>";
            }
            if (String.IsNullOrEmpty(Request.Form["price"]) || !Request.Form["price"].ValidateDouble(0.01))
            {
                errors += "<li>You must enter a valid price!</li>";
            }
            if (String.IsNullOrEmpty(Request.Form["tickets"]) || !Request.Form["tickets"].ValidateDouble(1))
            {
                errors += "<li>You must enter a valid number of tickets available</li>";
            }
            //make new item listing
            ItemListing myListing = new ItemListing();

            if (DateTime.TryParse(Request.Form["startdate"], out start))
            {
                myListing.StartDate = start;
            }
            else
            {
                errors += "<li>Invalid Date. Please use the Calendar.</li>";
            }
            if (DateTime.TryParse(Request.Form["enddate"], out end))
            {
                myListing.EndDate = end;
            }
            else
            {
                errors += "<li>Invalid Date. Please use the Calendar.</li>";
            }
            //check date values
            if (start < DateTime.Now)
            {
                errors += "<li>You cannot list an event in the past.</li>";
            }

            //if errors, stop and display them
            if (errors.Length > 0)
            {
                showError("Please see the following errors: <ul> " + errors + "</ul>");
                return;
            }



            //add values to listing            
            myListing.EventID = myEvent.EventItemID;
            myListing.Price = (decimal)double.Parse(Request.Form["price"]);
            myListing.MaxNumGuests = int.Parse(Request.Form["tickets"]);




            myListing.SupplierID = mySupplier.SupplierID;

            if (_myprodMan.AddItemListing(myListing) == listResult.Success)
            {
                addListing.Style.Add("display", "none");
                theLists.Style.Add("display", "block");
                showError("Event Listing Added!");
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            addListing.Style.Add("display", "none");
            theLists.Style.Add("display", "block");
        }

    }

}