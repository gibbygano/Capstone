using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
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
        private BookingManager myManager = new BookingManager();
        private string userMessage = "";

        public List<ItemListingDetails> currentItemListings;

        public ItemListingDetails selectedItemListing;
        public decimal extendedPrice;
        public decimal totalPrice;
        public decimal discount;
        public HotelGuest foundGuest;
        public int ticketQty;
        public string guestName;
        public string eventName;
        public DateTime date;

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/11
        /// Loads web pages for hotel guest and udpates session variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession)
            {
                ResetVariables();

            }
            else
            {
                //update variables with Session variables
                try
                {
                    foundGuest = (HotelGuest)Session["foundGuest"];
                    ticketQty = (int)Session["ticketQty"];
                    selectedItemListing = (ItemListingDetails)Session["selectedItemListing"];
                    extendedPrice = (decimal)Session["extendedPrice"];
                    totalPrice = (decimal)Session["totalPrice"];
                    discount = (decimal)Session["discount"];
                }
                catch
                {
                    //Running on the assumption that if there's an exception it's due to the (int) case on ticketQty failing due to a null session object.
                    ResetVariables();
                }
                
            }
        }

        /// <summary>
        /// Arik Chadima
        /// Created: 2015/4/30
        /// Refactored->Extracted method for reuse.
        /// </summary>
        private void ResetVariables()
        {
            foundGuest = null;
            ticketQty = 0;
            guestName = "";
            eventName = "";
            date = DateTime.Now;
            discount = 0;
            totalPrice = 0;
            extendedPrice = 0m;
            selectedItemListing = null;

            //create new session variables
            Session["foundGuest"] = foundGuest;
            Session["ticketQty"] = ticketQty;
            Session["selectedItemListing"] = selectedItemListing;
            Session["extendedPrice"] = extendedPrice;
            Session["totalPrice"] = totalPrice;
            Session["discount"] = discount;
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/08
        /// Binds data for the grid view to the current list of event offerings
        /// </summary>
        private void bindData()
        {
            gvListings.DataSource = currentItemListings;
            gvListings.DataBind();
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/08
        /// retrieves the list of event offerings for the grid view
        /// </summary>
        /// <returns>
        /// List of ItemListingDetails
        /// </returns>
        public IEnumerable<ItemListingDetails> GetCurrentListings()
        {
            try
            {
                currentItemListings = myManager.RetrieveActiveItemListingDetailsList();
                return currentItemListings;
            }
            catch (Exception)
            {
                showError("Error Retrieving List");
                return null;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created:  2015/05/05
        /// Shows user errors
        /// </summary>
        /// <param name="message"></param>
        private void showError(string message)
        {
            lblOtherMessage.Text = message;
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "showthis", "showMyMessage();", true);
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/05/05
        /// Shows user errors
        /// </summary>
        /// <param name="message"></param>
        private void showMessage(string message)
        {
            userMessage = message;
            lblMessage.Text = userMessage;
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/05/05
        /// Makes grid rows accessible for data tables
        /// </summary>
        /// <param name="grid"></param>
        public static void MakeAccessible(GridView grid)
        {
            if (grid.Rows.Count <= 0) return;
            grid.UseAccessibleHeader = true;
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (grid.ShowFooter)
                grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/05/05
        /// Makes grid rows accessible for data tables
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            MakeAccessible(gvListings);
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/11
        /// Handles submit functionality of a hotel guest adding an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblOtherMessage.Text = "";
            string errors = "";
            //if something isn't selected - throw error
            if (gvListings.SelectedValue == null)
            {
                errors += "<li>Please select an event</li>";

            } 
            //gets quantity from the quantity field
            if (!txtGuestTickets.Text.ValidateInt(1))
            {
                errors += "<li>You must enter a valid number of tickets.</li>";
                txtGuestTickets.Text = "";

            }
            else
            {
                if (Int32.Parse(txtGuestTickets.Text) > getSelectedItem().QuantityOffered)
                {
                    errors += "<li>You cannot request more tickets than available</li>";
                    txtGuestTickets.Text = "";
                }
            }
            if (!txtGuestPin.Text.ValidateAlphaNumeric())
            {
                    errors += "<li>You must enter a valid pin.</li>";
                    txtGuestTickets.Text = "";
            }
            if (errors.Length > 0)
            {
                showError("Please fix the following errors: <ul>" + errors + "</ul>");
                return;
            }
            try
            {
                string inPin = txtGuestPin.Text;

                //see if pin was found = if not there will be an exception
                foundGuest = myManager.CheckValidPIN(inPin);
            }
            catch (Exception)
            {
                showError("You must enter a valid pin.");
            }

            gatherFormInformation();
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/11
        /// Gathers form data to send to business logic
        /// </summary>
        private void gatherFormInformation()
        {
            try
            {
                ticketQty = Int32.Parse(txtGuestTickets.Text);

                //calculate values for the tickets
                extendedPrice = myManager.CalcExtendedPrice(selectedItemListing.Price, ticketQty);
                totalPrice = myManager.CalcTotalCharge(discount, extendedPrice);

                guestName = foundGuest.GetFullName;
                eventName = selectedItemListing.EventName;
                date = selectedItemListing.StartDate;

                Session["foundGuest"] = foundGuest;
                Session["ticketQty"] = ticketQty;
                Session["selectedItemListing"] = selectedItemListing;
                Session["extendedPrice"] = extendedPrice;
                Session["totalPrice"] = totalPrice;
                Session["discount"] = discount;

                //Need confirmation from guest before can officially submit
                //hide the submit panel and show the confirmation panel
                confirmDetails.Style.Add("display", "block");
                ticketRequest.Style.Add("display", "none");
            }
            catch (Exception ax)
            {
                showError("Error: " + ax.Message);
                return;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/11
        /// Clears fields after add
        /// </summary>
        private void clearFields()
        {
            txtGuestTickets.Text = "";
            txtGuestPin.Text = "";
            gvListings.SelectedIndex = -1;
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/11
        /// gets selected item from the data grid
        /// </summary>
        /// <returns></returns>
        private ItemListingDetails getSelectedItem()
        {
            int itemID = (int)gvListings.SelectedDataKey.Value;
            selectedItemListing = myManager.RetrieveItemListingDetailsList(itemID);
            return selectedItemListing;
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/11
        /// event handling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvListings_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            bindData();
        }

        /// <summary>
        /// Pat Banks
        /// created 2015/04/16
        /// event handling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvListings_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maxTickets = getSelectedItem().QuantityOffered;
            hfGuestMaxTickets.Value = maxTickets.ToString();
        }

        /// <summary>
        /// Pat Banks
        /// created:  2015/04/22
        /// Need confirmation from guest before can officially submit the booking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Booking webBookingToAdd = new Booking((int)foundGuest.HotelGuestID, 100, selectedItemListing.ItemListID, ticketQty, DateTime.Now, selectedItemListing.Price, extendedPrice, discount, totalPrice);

            ResultsEdit addResult = myManager.AddBookingResult(webBookingToAdd);

            switch (addResult)
            {
                case ResultsEdit.Success:
                    showError("Thank You, " + foundGuest.GetFullName + ". <br>You have successfully signed up for:\n" + selectedItemListing.EventName + ".");
                    clearFields();
                    gvListings.DataBind();
                    confirmDetails.Style.Add("display", "none");
                    ticketRequest.Style.Add("display", "block");
                    break;

                case ResultsEdit.ListingFull:
                    showError("Sorry, that event is full!");
                    break;
                case ResultsEdit.DatabaseError:
                    showError("Sorry, there was a problem registering for this event.\nPlease contact the front desk.");
                    break;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/22
        /// handles click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearFields();
            confirmDetails.Style.Add("display", "none");
        }
    }
}