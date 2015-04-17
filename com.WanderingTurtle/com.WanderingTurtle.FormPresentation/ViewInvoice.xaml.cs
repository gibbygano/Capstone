using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Presents user with guest invoice information
    /// </summary>
    public partial class ViewInvoice
    {
        private BookingManager _bookingManager = new BookingManager();
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private InvoiceManager _invoiceManager = new InvoiceManager();
        private InvoiceDetails invoiceToView;
        private List<BookingDetails> myBookingList;

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/2015
        ///
        /// Displays information for the selected guest's invoice
        /// </summary>
        /// <param name="selectedGuest">Selected guest to retrieve</param>
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
        /// Opens the AddBooking UI as dialog box
        /// </summary>
        /// <param name="sender">default event parameter</param>
        /// <param name="e">default event parameter</param>
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddBooking myBooking = new AddBooking(invoiceToView);

                if (myBooking.ShowDialog() == false)
                {
                    //fill the booking list after the AddBooking UI closes
                    refreshBookingList();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
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
                    throw new WanderingTurtleException(this, "Guest has bookings in the future and cannot be checked out.", "Warning");

                case (ResultsArchive.OkToArchive):

                    try
                    {
                        //opens UI with guest information
                        ArchiveInvoice myGuest = new ArchiveInvoice(invoiceToView.HotelGuestID);

                        if (myGuest.ShowDialog() == false)
                        {
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WanderingTurtleException(this, ex);
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
                throw new WanderingTurtleException(this, "Please select a booking to cancel.");
            }

            //check if selected item can be cancelled
            ResultsEdit result = _bookingManager.CheckToEditBooking((BookingDetails)lvGuestBookings.SelectedItem);

            switch (result)
            {
                case (ResultsEdit.CannotEditTooOld):
                    throw new WanderingTurtleException(this, "Bookings in the past cannot be cancelled.", "Warning");

                case (ResultsEdit.Cancelled):
                    throw new WanderingTurtleException(this, "This booking has already been cancelled.", "Warning");

                case (ResultsEdit.OkToEdit):
                    try
                    {
                        //opens the ui and passes the booking details object in
                        CancelBooking cancel = new CancelBooking((BookingDetails)lvGuestBookings.SelectedItem, invoiceToView);

                        if (cancel.ShowDialog() == false)
                        {
                            refreshBookingList();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WanderingTurtleException(this, ex);
                    }
                    break;
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
                throw new WanderingTurtleException(this, "Please select a booking to edit.");
            }

            EditBooking(bookingToEdit);
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
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void EditBooking(BookingDetails bookingToEdit, bool ReadOnly = false)
        {
            //check if selected item can be edited
            ResultsEdit result = _bookingManager.CheckToEditBooking(bookingToEdit);

            switch (result)
            {
                case (ResultsEdit.CannotEditTooOld):
                    throw new WanderingTurtleException(this, "Bookings in the past cannot be edited.");
                case (ResultsEdit.Cancelled):
                    throw new WanderingTurtleException(this, "This booking has been cancelled and cannot be edited.");

                case (ResultsEdit.OkToEdit):
                    try
                    {
                        EditBooking editForm = new EditBooking(invoiceToView, (BookingDetails)lvGuestBookings.SelectedItem, ReadOnly);

                        if (editForm.ShowDialog() == false)
                        {
                            refreshBookingList();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WanderingTurtleException(this, ex);
                    }
                    break;
            }
        }

        private void lvGuestBookings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditBooking(DataGridHelper.DataGridRow_Click<BookingDetails>(sender, e), true);
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
                throw new WanderingTurtleException(this, ex, "Unable to retrieve booking list from the database.");
            }
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
                lblCheckInDate.Content = invoiceToView.DateOpened.ToString(CultureInfo.InvariantCulture);
                lblRoomNum.Content = invoiceToView.GuestRoomNum;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Unable to retrieve guest information from the database.");
            }
        }
    }
}