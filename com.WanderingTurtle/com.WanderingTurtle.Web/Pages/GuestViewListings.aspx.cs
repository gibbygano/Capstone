using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.Web;
using System.Data;
using System.Drawing;

namespace com.WanderingTurtle.Web.Pages
{
    public partial class GuestViewListings : System.Web.UI.Page
    {
        BookingManager _myManager = new BookingManager();
        List<ItemListingDetails> _currentItemListings;
        private string _errorMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        private void bindData()
        {
            gvListings.DataSource = _currentItemListings;
            gvListings.DataBind();
            
        }


        public IEnumerable<ItemListingDetails> GetCurrentListings()
        {
            try
            {
                _currentItemListings = _myManager.RetrieveActiveItemListings();
                return _currentItemListings;

            }
            catch (Exception)
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving List\")</SCRIPT>");
                return null;
            }
        }

        private void showError(string message)
        {
            _errorMessage = message;
            lblError.Text = _errorMessage;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if something isn't selected - throw error

            gatherFormInformation();

        }

        private void gatherFormInformation()
        {
            bool stop = false;
            int errorCount = 0;

            decimal extendedPrice, totalPrice, discount;
            ItemListingDetails selectedItemListing = getSelectedItem();

            //gets quantity from the quantity field
            if(!Validator.ValidateInt(txtGuestTickets.Text,1))
            {
                txtGuestTickets.ToolTip = "You must enter a valid number of tickets.";
                txtGuestTickets.BorderColor = Color.Red;
                stop = true;
                errorCount++;
            }

            if (!Validator.ValidateInt(txtGuestPin.Text, 1))
            {
                txtGuestPin.ToolTip = "You must enter a valid pin.";
                txtGuestTickets.BorderColor = Color.Red;
                stop = true;
                errorCount++;
            }

            if (stop)
            {
                showError("You have " + errorCount + " errors that need to be fixed.");
                return;
            }
            else
            {
                txtGuestTickets.BorderColor = Color.Black;
                txtGuestTickets.BorderColor = Color.Black;

                try
                {
                    int inPin = Int32.Parse(txtGuestPin.Text);

                    //see if it is a valid pin
                    ResultsEdit answer = _myManager.checkValidPIN(inPin);
                    
                    int qty = Int32.Parse(txtGuestTickets.Text);

                    //get discount from form - web form is 0
                    discount = 0;

                    //calculate values for the tickets
                    extendedPrice = _myManager.calcExtendedPrice(selectedItemListing.Price, qty);
                    totalPrice = _myManager.calcTotalCharge(discount, extendedPrice);

                    switch (answer)
                    {
                        case ResultsEdit.NotFound:
                            lblError.Text = "PIN not found.";
                            break;

                        case ResultsEdit.OkToEdit:

                            Booking webBookingToAdd = new Booking(999, 100, selectedItemListing.ItemListID, qty, DateTime.Now, selectedItemListing.Price, extendedPrice, discount, totalPrice);
                            addWebBooking(inPin, webBookingToAdd);
                            break;

                        default:
                            break;
                    }

                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }

        private void addWebBooking(int inPin, Booking webBookingToAdd)
        {
            //add the booking & return result
            int guestID = _myManager.FindGuest(inPin);
            
            //update the id from the pin
            webBookingToAdd.GuestID = guestID;
            
            ResultsEdit addResult = _myManager.AddBookingResult(webBookingToAdd);

            switch (addResult)
            {
                case ResultsEdit.Success:
                    lblError.Text = "You have successfully signed up for the event.";
                    clearFields();

          //Need to refresh list - this doesn't work   
          //gvListings.DataSource = _currentItemListings;
                    gvListings.DataBind();
                    break;

                case ResultsEdit.ListingFull:
                    lblError.Text = "full";
                    break;
                case ResultsEdit.DatabaseError:
                    lblError.Text = "db error";
                    break;
            }
        }

        private void clearFields()
        {
            txtGuestTickets.Text = "";
            txtGuestPin.Text = "";
        }
    
        private ItemListingDetails getSelectedItem()
        {
            int itemID = (int)gvListings.SelectedDataKey.Value;

            ItemListingDetails selected = _myManager.RetrieveEventListing(itemID);

            return selected;
        }

        protected void lvListings_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            bindData();
        }

    }
}