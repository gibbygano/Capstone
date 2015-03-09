
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

            //fills the list view
            refreshBookingList();
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
            invoiceToView = InvoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);
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
        /// <remarks>
        /// Updated by Pat Banks 2015/03/08
        /// Added info to show the user how many bookings the guest has signed up for.
        /// </remarks>
        private void refreshBookingList()
        {
            lvGuestBookings.ItemsPanel.LoadContent();
            try
            {
                myBookingList = InvoiceManager.RetrieveBookingDetailsList(invoiceToView.HotelGuestID);
                lvGuestBookings.ItemsSource = myBookingList;
                lvGuestBookings.Items.Refresh();
                lblBookingsMessage.Content = "Guest has " + myBookingList.Count + " booking(s).";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrieve booking list from the database. \n" + ex.Message);
            }
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
            AddBooking myBooking = new AddBooking(invoiceToView);

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
                HotelGuest selectedGuest = HotelGuestManager.GetHotelGuest(invoiceToView.HotelGuestID);

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
            if (lvGuestBookings.SelectedItem == null)
            {
                MessageBox.Show("Please select a booking to edit.");
                return;
            }

            BookingDetails outBooking = (BookingDetails)lvGuestBookings.SelectedItem;

            EditBooking editForm = new EditBooking(invoiceToView, outBooking);

            if (editForm.ShowDialog() == false)
            {
                refreshBookingList();
            }
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
            if (CheckFutureBookingDateAndQty() == true)
            {
                MessageBox.Show("Guest has bookings in the future and cannot be checked out.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                //opens UI with guest information                  
                ArchiveInvoice myGuest = new ArchiveInvoice(invoiceToView.HotelGuestID);

                //closes window after successful guest archival
                if (myGuest.ShowDialog() == false)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Created by Pat Banks 2015/03/09
        /// 
        /// Checks if a booking is in the future and has tickets booked
        /// If fails, then guest cannot checkout
        /// </summary>
        /// <returns></returns>
        private bool CheckFutureBookingDateAndQty()
        {
            foreach (BookingDetails b in myBookingList)
            {
                if (b.StartDate > DateTime.Now && b.Quantity > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckBookingQuantity()
        {
            if (myBooking.Quantity == 0)
            {
                return true;
            }
            return false;
        }

        private bool CheckBookingDate()
        {
            if (myBooking.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        /// Created By: Tony Noel, 2015/03/04
        /// <summary>
        /// Cancel booking button to open cancel form.
        /// First attempts to create a BookingDetails object from the lvCustomerBookings,
        /// then passes this to the CancelBooking form if the object creation was successful. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBookingDate())
            {
                MessageBox.Show("Cancellations are not allowed for bookings in the past.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (CheckBookingQuantity())
            {
                MessageBox.Show("This booking has already been cancelled.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
            
            try
            {
                //attempts to create a booking details object with the selected line items
                BookingDetails myBooking = (BookingDetails)lvGuestBookings.SelectedItems[0];
                //opens the ui and passes the booking details object in
                CancelBooking cancel = new CancelBooking(myBooking, invoiceToView);
                
                if (cancel.ShowDialog() == false)
                {
                    refreshBookingList();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("To cancel a booking, please select the desired booking first.", ex.Message);
            }
        }
    }
}