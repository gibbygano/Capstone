using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace com.WanderingTurtle.Web
{
    /// <summary>
    /// Web facing form for Suppliers to add a new Event.
    /// Created by: Matt Lapka 2015/02/22
    /// Key points:
    /// Supplier must be signed in and an approved Supplier
    /// Event Name cannot be the same as another one
    /// </summary>
    public partial class SupplierAddEvent : System.Web.UI.Page
    {
        private EventManager _myEventManager = new EventManager();
        private string _errorMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            //notes -- need the if statement to ensure this only gets run the firs time
            //enableeventvalidation needs to be false
            //autopostback needs to be true
            if (!Page.IsPostBack)
            {
                try
                {
                    var eventTypes = _myEventManager.RetrieveEventTypeList();
                    eventTypes.Insert(0, new EventType { EventName = "Choose One..." });
                    comboEventTypeList.DataSource = eventTypes;
                    comboEventTypeList.DataTextField = "EventName";
                    comboEventTypeList.DataValueField = "EventTypeID";
                    comboEventTypeList.DataBind();

                }
                catch (Exception ex)
                {
                    showError(ex.Message);

                }
            }

        }

        protected void radTransportation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void radOnSite_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (radOnSite.SelectedValue == "True")
            {
                //set transportation to false and keep it hidden because tranportation is not needed
                lblTransportation.Visible = false;
                radTransportation.SelectedValue = "False";
                radTransportation.Visible = false;
            }
            else
            {
                //make transportation question visible
                radTransportation.Visible = true;
                lblTransportation.Visible = true;
                //reset to unselected
                radTransportation.SelectedIndex = -1;
            }
        }

        private void showError(string message)
        {
            _errorMessage = message;
            lblError.Text = _errorMessage;
        }

        protected void btnSubmitEvent_Click(object sender, EventArgs e)
        {
            bool stop = false;
            int errorCount = 0;
            //validate!
            if (!Validator.ValidateAlphaNumeric(txtEventName.Text, 1))
            {
                txtEventName.ToolTip = "You must enter a valid event name!";
                txtEventName.BorderColor = Color.Red;
                stop = true;
                errorCount++;
            }
            if (comboEventTypeList.SelectedIndex < 1)
            {
                comboEventTypeList.ToolTip = "You must select an event type!";
                comboEventTypeList.BorderColor = Color.Red;
                stop = true;
                errorCount++;
            }
            if (String.IsNullOrEmpty(txtDescription.Text))
            {
                txtDescription.ToolTip = "Please enter a brief description";
                txtDescription.BorderColor = Color.Red;
                stop = true;
                errorCount++;
            }
            if (radOnSite.SelectedIndex < 0)
            {
                radOnSite.ToolTip = "Please select if this will be an on site event.";
                radOnSite.BorderColor = Color.Red;
                stop = true;
                errorCount++;
            }
            if (radTransportation.SelectedIndex < 0)
            {
                radTransportation.ToolTip = "Please indicate whether or not you will provide transportation to the event.";
                radTransportation.BorderColor = Color.Red;
                stop = true;
                //only add error count if this questions is visible
                if (radTransportation.Visible)
                {
                    errorCount++;
                }
            }
            if (stop)
            {
                showError("You have " + errorCount + " errors that need to be fixed.");
                return;
            }            
            else
            {

                //reset border colors
                txtEventName.BorderColor = Color.Black;
                txtDescription.BorderColor = Color.Black;
                comboEventTypeList.BorderColor = Color.Black;
                radTransportation.BorderColor = Color.Black;
                radOnSite.BorderColor = Color.Black;


                try
                {
                    //create new Event
                    Event currentEvent = new Event();

                    currentEvent.EventItemName = txtEventName.Text;
                    currentEvent.Description = txtDescription.Text;
                    int typeID = 0;
                    //we know that the selected value (event type id) is an int so no need to validate
                    int.TryParse(comboEventTypeList.SelectedValue, out typeID);
                    currentEvent.EventTypeID = typeID;
                    bool onsite;
                    bool transportation;
                    bool.TryParse(radOnSite.SelectedValue, out onsite);
                    bool.TryParse(radTransportation.SelectedValue, out transportation);
                    currentEvent.OnSite = onsite;
                    currentEvent.Transportation = transportation;

                    /*****************TEST DATA****************************************/
                    //these are added for testing purposes until Event can be reworked.
                    //need to be removed once that is fixed
                    /*
                    currentEvent.EventStartDate = DateTime.Now;
                    currentEvent.EventEndDate = DateTime.Now;
                    currentEvent.PricePerPerson = (decimal)15.00;
                    currentEvent.MinNumGuests = 0;
                    currentEvent.MaxNumGuests = 15;
                     * */
                    /******************************************************************/
                    int rows = _myEventManager.AddNewEvent(currentEvent);
                    if (rows == 1)
                    {
                        Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Event Added Successfully!\")</SCRIPT>");
                    }
                    clearForm();
                }
                catch (Exception ex)
                {
                    showError(ex.Message);
                }
            }
        }

        private void clearForm()
        {
            txtEventName.Text = "";
            txtDescription.Text = "";
            comboEventTypeList.SelectedIndex = 0;
            radOnSite.SelectedIndex = -1;
            radTransportation.SelectedIndex = -1;
            radTransportation.Visible = false;
            lblTransportation.Visible = false;
            showError("");
        }


    }
}