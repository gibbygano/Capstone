using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Presents user with guest invoice information
    /// </summary>
    public partial class ViewInvoice
    {
        private List<BookingDetails> myBookingList;
        private InvoiceDetails invoiceToView;
        private InvoiceManager _invoiceManager = new InvoiceManager();
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private BookingManager _bookingManager = new BookingManager();

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/2015
        /// 
        /// Displays information for the selected guest's invoice
        /// </summary>
        /// <param name="selectedHotelGuestID">Selected guest's id to retrieve</param>
        public ViewInvoice(InvoiceDetails selectedGuest)
        {
            InitializeComponent();

            //fills the guest data
            refreshGuestInformation(selectedGuest.HotelGuestID);

            //fills the list view
            refreshBookingList();
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        /// 
        /// Calls the InvoiceManager method that retrieves the guest's invoice information
        /// and stores the information in invoiceToView
        /// </summary>
        /// <param name="selectedHotelGuestID">selected guest's id</param>
        private void refreshGuestInformation(int selectedHotelGuestID)
        {
            try
            {
                //object to store guest's information
                invoiceToView = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);

                lblGuestNameLookup.Content = invoiceToView.GetFullName;
                lblCheckInDate.Content = invoiceToView.DateOpened.ToString();
                lblRoomNum.Content = invoiceToView.GuestRoomNum.ToString();
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "Unable to retrieve guest information from the database.");
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        ///
        /// Calls the InvoiceManager method that retrieves a list of booking details for a selected guest
        /// </summary>
        /// <remarks>
        /// Pat Banks 
        /// Updated: 2015/03/08
        /// Added info to show the user how many bookings the guest has signed up for.
        /// </remarks>
        private void refreshBookingList()
        {
            lvGuestBookings.ItemsPanel.LoadContent();
            try
            {
                myBookingList = _invoiceManager.RetrieveGuestBookingDetailsList(invoiceToView.HotelGuestID);

                lvGuestBookings.ItemsSource = myBookingList;
                lvGuestBookings.Items.Refresh();
                lblBookingsMessage.Content = "Guest has " + myBookingList.Count + " booking(s).";
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "Unable to retrieve booking list from the database.");
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
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
        /// Created: 2015/03/03
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
                HotelGuest selectedGuest = _hotelGuestManager.GetHotelGuest(invoiceToView.HotelGuestID);

                //refreshes guest information after AddEditHotelGuest UI
                if (new AddEditHotelGuest(selectedGuest).ShowDialog() == false)
                {
                    refreshGuestInformation(invoiceToView.HotelGuestID);
                }
            }
            catch (ApplicationException ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message);
            }
            catch (SqlException ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message);
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message);
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        ///
        /// Opens the EditBooking UI as dialog box
        /// </summary>
        /// <remarks>
        /// Pat Banks 
        /// Updated: 2015/03/19
        /// 
        /// Moved logic checks to Business Logic Layer - CheckToEditBooking
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingDetails bookingToEdit = (BookingDetails)lvGuestBookings.SelectedItem;

            //check form input error
            if (lvGuestBookings.SelectedItem == null)
            {
                DialogBox.ShowMessageDialog(this, "Please select a booking to edit.");
                return;
            }

            //check if selected item can be edited
            ResultsEdit result = _bookingManager.CheckToEditBooking(bookingToEdit);
            
            switch (result)
            {
                case (ResultsEdit.CannotEditTooOld):
                    DialogBox.ShowMessageDialog(this, "Bookings in the past cannot be edited.");
                    break;
                case (ResultsEdit.Cancelled):
                    DialogBox.ShowMessageDialog(this, "This booking has been cancelled and cannot be edited.");
                    break;
                case (ResultsEdit.OkToEdit):
                    EditBooking editForm =  new EditBooking(invoiceToView, (BookingDetails)lvGuestBookings.SelectedItem);

                    if (editForm.ShowDialog() == false)
                    {
                        refreshBookingList();
                    }
                    break;
            }
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/04
        /// 
        /// Cancel booking button to open cancel form.
        /// First attempts to create a BookingDetails object from the lvCustomerBookings,
        /// then passes this to the CancelBooking form if the object creation was successful.
        /// </summary>
        /// <remarks>
        /// Pat Banks 
        /// Updated: 2015/03/19
        /// 
        /// Moved logic checks to booking manager CheckToEditBooking
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            //check if something was selected
            if (lvGuestBookings.SelectedItem == null)
            {
                DialogBox.ShowMessageDialog(this, "Please select a booking to cancel.");
                return;
            }

            //check if selected item can be cancelled
            ResultsEdit result = _bookingManager.CheckToEditBooking((BookingDetails)lvGuestBookings.SelectedItem);

            switch (result)
            {
                case (ResultsEdit.CannotEditTooOld):
                    DialogBox.ShowMessageDialog(this, "Bookings in the past cannot be cancelled.", "Warning");
                    break;
                case (ResultsEdit.Cancelled):
                    DialogBox.ShowMessageDialog(this, "This booking has already been cancelled.", "Warning");
                    break;
                case (ResultsEdit.OkToEdit):
                    //opens the ui and passes the booking details object in
                    CancelBooking cancel = new CancelBooking((BookingDetails)lvGuestBookings.SelectedItem, invoiceToView);

                    if (cancel.ShowDialog() == false)
                    {
                        refreshBookingList();
                    }
                    break;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        ///
        /// Opens the ArchiveInvoice UI as dialog box
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/19
        /// 
        /// Moved logic checks to invoice manager - checkToArchiveInvoice
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArchiveInvoice_Click(object sender, RoutedEventArgs e)
        {
            //check if invoice can be closed
            ResultsArchive result = _invoiceManager.CheckToArchiveInvoice(invoiceToView, myBookingList);

            switch (result)
            {
                case (ResultsArchive.CannotArchive):
                    DialogBox.ShowMessageDialog(this, "Guest has bookings in the future and cannot be checked out.", "Warning");
                    break;
                case (ResultsArchive.OkToArchive):
                    //opens UI with guest information
                    ArchiveInvoice myGuest = new ArchiveInvoice(invoiceToView.HotelGuestID);

                    bool? res = myGuest.ShowDialog();

                    //closes window after successful guest archival
                    if (res.HasValue && res.Value)
                    {
                        Close();
                    }
                    else
                    {
                        return;
                    }
                    break;
            }
        }
    }
}