using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for EditBooking.xaml
    /// </summary>
    public partial class EditBooking : Window
    {
        public InvoiceDetails inInvoice;
        public BookingDetails inBookingDetails;
        List<BookingDetails> outInvList = new List<BookingDetails>();
        Booking newBooking;

        public EditBooking(InvoiceDetails inInvoice, BookingDetails inBookingDetails)
        {
            this.inInvoice = inInvoice;
            this.inBookingDetails = inBookingDetails;

            InitializeComponent();

            outInvList.Add(inBookingDetails);

            lblGeneralEditBooking.Content = "Editing Booking #" + inBookingDetails.BookingID;
            lblEditBookingGuestName.Content = inInvoice.GetFullName;
            tbEditBookingQuantity.Text = inBookingDetails.Quantity.ToString();
            tbEditBookingDiscount.Text = inBookingDetails.Discount.ToString();

            lvEditBookingListItems.ItemsSource = outInvList;
        }

        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {
            int quantity;
            string inQuantity;
            string inDiscount;
            int discount;

            inQuantity = tbEditBookingQuantity.Text;

            if(!int.TryParse(inQuantity, out quantity))
            {
                MessageBox.Show("Please enter a whole number for quantity.");
                return;
            }

            if (quantity < 1)
            {
                MessageBox.Show("Please use cancel instead of setting quantity 0");
                return;
            }

            inDiscount = tbEditBookingDiscount.Text;
            int.TryParse(inDiscount, out discount);

            if (inDiscount == null)
            {
                discount = 0;
            }
            else if(discount > 100)
            {
                MessageBox.Show("Discount cannot exceed 100.");
                tbEditBookingDiscount.Clear();
                tbEditBookingDiscount.Focus();
                return;
            }


            inBookingDetails.Quantity = quantity;

            inBookingDetails.ExtendedPrice = calcExtendedPrice(inBookingDetails.TicketPrice, discount);
//ProductManager myProdMan = new ProductManager();
//ItemListing originalListItem = myProdMan.RetrieveItemListing(myBooking.ItemListID.ToString());

//int newNumGuests = originalListItem.CurrentNumGuests - myBooking.Quantity;

  //int result1 = OrderManager.updateNumberOfGuests(myBooking.ItemListID, originalListItem.CurrentNumGuests, newNumGuests);

            newBooking = (Booking)inBookingDetails;

            OrderManager.EditBooking(newBooking);

            MessageBox.Show("Booking changed successfully.");

            this.Close();
        }

        private decimal calcExtendedPrice(decimal price, decimal discount)
        {
            decimal extendedPrice;

            extendedPrice = ((100 - discount)/ 100) * price;

            return extendedPrice;
        }
    }
}
