﻿using System;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession)
            {
                foundGuest = null;
                ticketQty= 0;
                guestName="";
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
                Session["discount"] =discount;

            }
            else
            {
                //update variables with Session variables
                foundGuest = (HotelGuest)Session["foundGuest"];
                ticketQty = (int)Session["ticketQty"];
                selectedItemListing = (ItemListingDetails)Session["selectedItemListing"];
                extendedPrice = (decimal)Session["extendedPrice"];
                totalPrice = (decimal)Session["totalPrice"];
                discount = (decimal)Session["discount"];
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
            gvListings.DataSource = currentItemListings;
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
                currentItemListings = myManager.RetrieveActiveItemListings();
                return currentItemListings;
            }
            catch (Exception)
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving List\")</SCRIPT>");
                return null;
            }
        }


        private void showMessage(string message)
        {
            userMessage = message;
            lblMessage.Text = userMessage;
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
            selectedItemListing = getSelectedItem();

            //gets quantity from the quantity field
            if (!Validator.ValidateInt(txtGuestTickets.Text, 1))
            {
                lblMessage.Text = "You must enter a valid number of tickets.";
                return;
            }
            else if (Int32.Parse(txtGuestTickets.Text) > getSelectedItem().QuantityOffered)
            {
                lblMessage.Text = "You cannot request more tickets than available";
                return;
            }

            if (!Validator.ValidateAlphaNumeric(txtGuestPin.Text))
            {
                lblMessage.Text = "You must enter a valid pin.";
                return;
            }

            try
            {
                string inPin = txtGuestPin.Text;

                //see if pin was found = if not there will be an exception

                foundGuest = myManager.checkValidPIN(inPin);

                ticketQty = Int32.Parse(txtGuestTickets.Text);

                //calculate values for the tickets
                extendedPrice = myManager.calcExtendedPrice(selectedItemListing.Price, ticketQty);
                totalPrice = myManager.calcTotalCharge(discount, extendedPrice);

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
            selectedItemListing = myManager.RetrieveEventListing(itemID);
            return selectedItemListing;
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

        //Need confirmation from guest before can officially submit
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Booking webBookingToAdd = new Booking((int)foundGuest.HotelGuestID, 100, selectedItemListing.ItemListID, ticketQty, DateTime.Now, selectedItemListing.Price, extendedPrice, discount, totalPrice);

            ResultsEdit addResult = myManager.AddBookingResult(webBookingToAdd);

            switch (addResult)
            {
                case ResultsEdit.Success:
                    lblMessage.Text = ("Thank You, " + foundGuest.GetFullName + ". You have successfully signed up for " + selectedItemListing.EventName + ".");
                    clearFields();
                    gvListings.DataBind();
                    confirmDetails.Style.Add("display", "none");
                    ticketRequest.Style.Add("display", "block");
                    break;

                case ResultsEdit.ListingFull:
                    lblMessage.Text = "full";
                    break;
                case ResultsEdit.DatabaseError:
                    lblMessage.Text = "db error";
                    break;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearFields();
            confirmDetails.Style.Add("display", "none");
        }
    }
}