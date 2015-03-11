using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ArchiveInvoice.xaml
    /// </summary>
    public partial class ArchiveInvoice : Window
    {
        private List<BookingDetails> myBookingList;
        private Invoice invoiceToArchive;
        private Invoice originalInvoice;
        private HotelGuest guestToView;
        InvoiceManager _invoiceManager = new InvoiceManager();
        HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        OrderManager _orderManager = new OrderManager();

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        ///
        /// Constructs a populated form that shows the final charges for a guest and
        /// allows employee to submit to close the invoice
        /// </summary>
        /// <param name="selectedHotelGuestID">Guest selected from the ViewInvoiceUI</param>
        public ArchiveInvoice(int selectedHotelGuestID)
        {
            originalInvoice = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);
            invoiceToArchive = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);

            guestToView = _hotelGuestManager.GetHotelGuest(invoiceToArchive.HotelGuestID);
            myBookingList = _invoiceManager.RetrieveBookingDetailsList(invoiceToArchive.HotelGuestID);

            invoiceToArchive.TotalPaid = _invoiceManager.CalculateTotalDue(myBookingList);

            InitializeComponent();
            lblGuestNameLookup.Content = guestToView.GetFullName;
            lblCheckInDate.Content = invoiceToArchive.DateOpened.ToString();
            lblInvoice.Content = invoiceToArchive.InvoiceID.ToString();
            lblAddress.Content = guestToView.Address1;
            lblCityState.Content = guestToView.CityState.GetZipStateCity;
            lblPhoneNum.Content = guestToView.PhoneNumber;
            lblRoomNum.Content = guestToView.Room;
            lblInvoice.Content = invoiceToArchive.InvoiceID;
            lblTotalPrice.Content = invoiceToArchive.GetTotalFormat;
            lblPhoneNum.Content = guestToView.PhoneNumber;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        ///
        /// Calls methods to archive the associated database records for the selected hotel guest
        /// </summary>
        /// <param name="sender">default event Parameter</param>
        /// <param name="e">default event Parameter</param>
        private void btnFinalizeInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //archive guest's bookings by changing active field to false
                foreach (BookingDetails b in myBookingList)
                {
                    b.Active = false;
                    int numrows = _orderManager.EditBooking(b);
                    if (numrows != 1)
                    {
                        MessageBox.Show("There was a problem archiving the booking");
                    }
                }

                //archive hotel guest
                bool guestArchive = _hotelGuestManager.ArchiveHotelGuest(guestToView, !guestToView.Active);

                if (guestArchive == true)
                {
                    //update invoice record with dateClosed and change active status
                    invoiceToArchive.DateClosed = DateTime.Now;
                    invoiceToArchive.Active = false;

                    bool result = _invoiceManager.ArchiveCurrentGuestInvoice(originalInvoice, invoiceToArchive);

                    //Dialog appears if records were successfully archived
                    if (result == true)
                    {
                        MessageBox.Show("Guest Checkout Complete");
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}