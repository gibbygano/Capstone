
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
    /// Presents user with guest invoice information
    /// </summary>
    public partial class ViewInvoice : Window
    {
        private InvoiceManager myInvoiceManager = new InvoiceManager();
        private HotelGuestManager myGuestManager = new HotelGuestManager();
        private List<BookingDetails> myBookingList;
        private InvoiceDetails invoiceToView;


        /// <summary>
        /// Pat Banks
        /// 2015/02/2015
        /// Displays information for the selected guest's invoice
        /// </summary>
        /// <param name="selectedHotelGuestID">Selected guest's id to retrieve</param>
        public ViewInvoice(InvoiceDetails selectedGuest)
        {
            InitializeComponent();

            //methods that get the form data
            refreshGuestInformation(selectedGuest.HotelGuestID);
            refreshBookingList();

            //fills the list view
            lvCustomerBookings.ItemsSource = myBookingList;
        }

        /// <summary>
        /// Pat Banks
        /// 2015/03/03
        /// Calls the InvoiceManager method that retrieves the guest's invoice information
        /// and stores the information in invoiceToView
        /// </summary>
        /// <param name="selectedHotelGuestID">selected guest's id</param>
        private void refreshGuestInformation(int selectedHotelGuestID)
        {
            //object to store guest's information
            invoiceToView = myInvoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);
            lblGuestNameLookup.Content = invoiceToView.GetFullName;
            lblCheckInDate.Content = invoiceToView.DateOpened.ToString();
            lblRoomNum.Content = invoiceToView.GuestRoomNum.ToString();
        }

        /// <summary>
        /// Pat Banks
        /// 2015/03/03
        /// 
        /// Calls the InvoiceManager method that retrieves a list of booking details for a selected guest
        /// </summary>
        private void refreshBookingList()
        {
            myBookingList = myInvoiceManager.RetrieveBookingDetailsList(invoiceToView.HotelGuestID);
        }

        /// <summary>
        /// Pat Banks
        /// 2015/03/03
        /// 
        /// Opens the AddBooking UI as dialog box
        /// </summary>
        /// <param name="sender">default event parameter</param>
        /// <param name="e">default event parameter</param>
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            AddBooking myBooking = new AddBooking();

            if (myBooking.ShowDialog() == false)
            {
                //fill the booking list after the AddBooking UI closes
                refreshBookingList();
            }
        }

        /// <summary>
        /// Pat Banks
        /// 2015/03/03
        /// 
        /// Opens the EditGuest UI as dialog box
        /// </summary>
        /// <param name="sender">default event parameter</param>
        /// <param name="e">default event parameter</param>
        private void btnEditGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //retrieve the guest information
                HotelGuest selectedGuest = myGuestManager.GetHotelGuest(invoiceToView.HotelGuestID);

                //refreshes guest information after AddEditHotelGuest UI
                if (new AddEditHotelGuest(selectedGuest).ShowDialog() == false)
                {
                    refreshGuestInformation(invoiceToView.HotelGuestID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Pat Banks
        /// 2015/03/03
        /// 
        /// Opens the EditBooking UI as dialog box
        /// </summary>
        /// <param name="sender">default event parameter</param>
        /// <param name="e">default event parameter</param>
        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Pat Banks
        /// 2015/03/03
        /// 
        /// Opens the ArchiveInvoice UI as dialog box
        /// </summary>
        /// <param name="sender">default event parameter</param>
        /// <param name="e">default event parameter</param>
        private void btnCloseInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //opens UI with guest information
                ArchiveInvoice myGuest = new ArchiveInvoice(invoiceToView.HotelGuestID);

                //closes window after successful guest archival
                if (new ArchiveInvoice(invoiceToView.HotelGuestID).ShowDialog() == false)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}