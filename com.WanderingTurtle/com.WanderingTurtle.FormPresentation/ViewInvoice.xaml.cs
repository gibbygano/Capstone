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
    public partial class ViewInvoice : Window
    {
        public InvoiceManager myInvoiceManager = new InvoiceManager();
        public List<BookingDetails> myBookingList;

        /// <summary>
        /// Pat Banks
        /// Created:  2015/02/2015
        /// Displays information for the selected guest's invoice
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="SelectedInvoice">retrieves InvoiceDetails from the form</param>
        public ViewInvoice(int selectedHotelGuestID)
        {
            InitializeComponent();
            var invoiceToView = new InvoiceDetails();

            invoiceToView = myInvoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);

            lblGuestNameLookup.Content = invoiceToView.GetFullName;
            lblGuestID.Content = invoiceToView.HotelGuestID.ToString();
            lblInvoiceID.Content = invoiceToView.InvoiceID.ToString();
            
            //fill the booking list
            myBookingList = myInvoiceManager.RetrieveBookingDetailsList(invoiceToView.HotelGuestID);
            lvCustomerBookings.ItemsSource = myBookingList;
        }

        private void btnAddBookingAdd_Click(object sender, RoutedEventArgs e)
        {
            AddBooking myBooking = new AddBooking();
            

            if (myBooking.ShowDialog() == false)
            {
                //myBookingList = myInvoiceManager.RetrieveBookingDetailsList(HotelGuestID);
            }
        }

    }
}
