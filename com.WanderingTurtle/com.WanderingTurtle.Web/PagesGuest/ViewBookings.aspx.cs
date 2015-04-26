using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace com.WanderingTurtle.Web.PagesGuest
{
    public partial class ViewBookings : System.Web.UI.Page
    {
        public HotelGuest foundGuest;
        public List<BookingDetails> bookings;
        public decimal totalPrice;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Session.IsNewSession)
            {
                foundGuest = (HotelGuest)Session["ViewBookingsGuest"];
                try 
	            {
                    InvoiceManager im = new InvoiceManager();
                    bookings = im.RetrieveGuestBookingDetailsList((int)foundGuest.HotelGuestID);
                    gvBookings.DataSource = bookings;
                    gvBookings.DataBind();
                    totalPrice = bookings.Sum(x => x.ExtendedPrice);
	            }
	            catch (Exception)
	            {
		            
	            }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            BookingManager hgm = new BookingManager();
            if (Validator.ValidateAlphaNumeric(txtLogin.Text, 6, 6))
            {
                try
                {
                    foundGuest = hgm.checkValidPIN(txtLogin.Text);
                    Session["ViewBookingsGuest"] = foundGuest;
                    InvoiceManager im = new InvoiceManager();
                    bookings = im.RetrieveGuestBookingDetailsList((int)foundGuest.HotelGuestID);
                    gvBookings.DataSource = bookings;
                    gvBookings.DataBind();
                    totalPrice = bookings.Sum(x => x.TotalCharge);
                    loginDiv.Visible = false;
                    guestDetailsDiv.Visible = true;

                }
                catch (ApplicationException ax)
                {
                    lblError.Text = "The pin you entered is invalid.";
                }
                catch (Exception ex)
                {
                    lblError.Text = "There was an error fetching data.";
                }
            }
        }

    }
}