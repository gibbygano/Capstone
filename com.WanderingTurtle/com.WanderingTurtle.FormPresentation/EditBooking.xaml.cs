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
        public OrderManager orderManager = new OrderManager();
        List<BookingDetails> outInvList = new List<BookingDetails>();
        Booking newBooking;

        public EditBooking(InvoiceDetails inInvoice, BookingDetails inBookingDetails)
        {
            this.inInvoice = inInvoice;
            this.inBookingDetails = inBookingDetails;

            InitializeComponent();

            outInvList.Add(inBookingDetails);

            lblGeneralEditBooking.Content = "Editing Booking #" + inBookingDetails.BookingID;
            lblEditBookingEmpID.Content = inBookingDetails.EmployeeID;
            lblEditBookingGuestName.Content = inInvoice.GetFullName;
            tbEditBookingQuantity.Text = inBookingDetails.Quantity.ToString();

            lvEditBookingListItems.ItemsSource = outInvList;
        }

        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {
            int quantity;
            string inQuantity;

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
            
            inBookingDetails.Quantity = quantity;

            newBooking = (Booking)inBookingDetails;

            orderManager.EditBooking(newBooking);

            MessageBox.Show("Booking changed successfully.");

            this.Close();
        }
    }
}
