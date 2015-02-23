using System;
using System.Collections.Generic;
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
            
            if (radOnSite.SelectedValue=="True")
            {
                //set transportation to false and keep it hidden because tranportation is not needed
                lblTransportation.Visible = false;
                radTransportation.SelectedValue = "False";
                radTransportation.Visible = false;
            }
            else
            {
                showError(radOnSite.SelectedValue.ToString());
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
            //validate!
            if (!Validator.ValidateAlphaNumeric(txtEventName.Text,1))
            {
                showError("You must enter an event name!");
                return;
            }
            else if (comboEventTypeList.SelectedIndex < 1)
            {
                showError("You must select an event type!");
                return;
            }
            else if (!Validator.ValidateAlphaNumeric(txtDescription.Text, 1))
            {
                showError("Please enter a brief description");
                return;
            }
            else if (radOnSite.SelectedIndex < 0)
            {
                showError("Please select if this will be an on site event.");
                return;
            }
            else if (radTransportation.SelectedIndex <0)
            {
                showError("Please indicate whether or not you will provide transportation to the event.");
                return;
            }
            else
            {
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
                    currentEvent.EventStartDate = DateTime.Now;
                    currentEvent.EventEndDate = DateTime.Now;
                    currentEvent.PricePerPerson = (decimal)15.00;
                    currentEvent.MinNumGuests = 0;
                    currentEvent.MaxNumGuests = 15;
                    /******************************************************************/
                    int rows = _myEventManager.AddNewEvent(currentEvent);
                    if (rows == 1)
                    {
                        showError("Event Successfully added!");
                     }

                }
                catch (Exception ex)
                {
                    showError(ex.Message);
                }
                
                
            }
            

        }
    }
}