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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Arik Chadima 
        /// Created: 2015/4/24
        /// 
        /// Checks the user's pin and if it is valid, shows what bookings they've selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            BookingManager hgm = new BookingManager();
            if (Validator.ValidateAlphaNumeric(txtLogin.Text, 6, 6))
            {
                try
                {
                    var foundGuest = hgm.CheckValidPIN(txtLogin.Text);

                    InvoiceManager im = new InvoiceManager();
                    var bookings = im.RetrieveGuestBookingDetailsList((int)foundGuest.HotelGuestID);
                    repBookings.DataSource = bookings;
                    repBookings.DataBind();
                    var totalPrice = bookings.Sum(x => x.TotalCharge);
                    GuestFullName.Text = foundGuest.GetFullName;
                    Address1.Text = foundGuest.Address1;
                    Address2.Text = foundGuest.Address2;
                    var cityState = foundGuest.CityState;
                    CityStateZip.Text = cityState.City + ", " + cityState.State + "  " + cityState.Zip;
                    EmailAddress.Text = foundGuest.EmailAddress;
                    PhoneNumber.Text = foundGuest.PhoneNumber;
                    TotalPrice.Text = String.Format("{0:c}", totalPrice);
                    loginDiv.Visible = false;
                    guestDetailsDiv.Visible = true;

                }
                catch (ApplicationException)
                {
                    lblError.Text = "The pin entered is not active or does not exist.";
                    lblError.Visible = true;
                    txtLogin.Text = "";
                }
                catch (Exception)
                {
                    lblError.Text = "There was an error fetching data.";
                    lblError.Visible = true;
                    txtLogin.Text = "";
                }
            }
            else
            {
                lblError.Text = "The pin you enterred was not formatted correctly.  It must be 5 alphanumeric characters.";
                lblError.Visible = true;
            }
        }

    }
}