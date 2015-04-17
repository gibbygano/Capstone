using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.Web;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;

namespace com.WanderingTurtle.Web.Pages
{
    public partial class GuestViewListings : System.Web.UI.Page
    {
        BookingManager _myManager = new BookingManager();
        List<ItemListingDetails> _currentItemListings;
        private string _userMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/08
        /// 
        /// Binds data for the grid view to the current list of event offerings
        /// </summary>
        private void bindData()
        {
            gvListings.DataSource = _currentItemListings;
            gvListings.DataBind();
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/08
        /// 
        /// retrieves the list of event offerings for the grid view
        /// </summary>
        /// <returns>
        /// List of ItemListingDetails
        /// </returns>
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


        private void showMessage(string message)
        {
            _userMessage = message;
            lblMessage.Text = _userMessage;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if something isn't selected - throw error
            if (gvListings.SelectedValue == null)
            {
                lblMessage.Text = "Please select an event";
                return;
            }
            else
            {
                gatherFormInformation();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void gatherFormInformation()
        {
            decimal extendedPrice, totalPrice, discount;
            ItemListingDetails selectedItemListing = getSelectedItem();

            //gets quantity from the quantity field
            if (!Validator.ValidateInt(txtGuestTickets.Text, 1))
            {
                lblMessage.Text = "You must enter a valid number of tickets.";
                return;
            }

            if (!Validator.ValidateInt(txtGuestPin.Text, 1))
            {
                lblMessage.Text = "You must enter a valid pin.";
                return;
            }

            try
            {
                string inPin = txtGuestPin.Text;

                //see if it is a valid pin = if not there will be an exception

                HotelGuest foundGuest = _myManager.checkValidPIN(inPin);

                int qty = Int32.Parse(txtGuestTickets.Text);

                //get discount from form - web form is 0
                discount = 0;

                //calculate values for the tickets
                extendedPrice = _myManager.calcExtendedPrice(selectedItemListing.Price, qty);
                totalPrice = _myManager.calcTotalCharge(discount, extendedPrice);

                int guestID = (int)foundGuest.HotelGuestID;

                Booking webBookingToAdd = new Booking(guestID, 100, selectedItemListing.ItemListID, qty, DateTime.Now, selectedItemListing.Price, extendedPrice, discount, totalPrice);

                ResultsEdit addResult = _myManager.AddBookingResult(webBookingToAdd);

                switch (addResult)
                {
                    case ResultsEdit.Success:
                        lblMessage.Text = ("Thank You, " + foundGuest.GetFullName + ". You have successfully signed up for " + selectedItemListing.EventName + ".");
                        clearFields();
                        gvListings.DataBind();
                        break;

                    case ResultsEdit.ListingFull:
                        lblMessage.Text = "full";
                        break;
                    case ResultsEdit.DatabaseError:
                        lblMessage.Text = "db error";
                        break;
                }
            }
            catch (SqlException)
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Incorrect PIN\")</SCRIPT>");

            }
            catch (Exception)
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"There was an error\")</SCRIPT>");

            }
        }

        private void clearFields()
        {
            txtGuestTickets.Text = "";
            txtGuestPin.Text = "";
            gvListings.SelectedIndex = -1;
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

        protected void gvListings_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maxTickets = getSelectedItem().QuantityOffered;
            hfGuestMaxTickets.Value = maxTickets.ToString();
        }

    }
}