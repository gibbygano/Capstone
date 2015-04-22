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
    public partial class ViewInvoice : IDataGridContextMenu
    {
        private InvoiceDetails _CurrentInvoice { get; set; }

        private BookingManager _bookingManager = new BookingManager();
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private InvoiceManager _invoiceManager = new InvoiceManager();
        private List<BookingDetails> myBookingList;

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/2015
        /// Displays information for the selected guest's invoice
        /// </summary>
        /// <param name="selectedGuest">Selected guest to retrieve</param>
        /// <exception cref="ArgumentNullException"><paramref name="(DataGridContextMenuResult)" /> is null. </exception>
        /// <exception cref="ArgumentException"><paramref name="(DataGridContextMenuResult)" /> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="OverflowException"><paramref name="(menuItem.Header)" /> is outside the range of the underlying type of <paramref name="(DataGridContextMenuResult)" />.</exception>
        public ViewInvoice(InvoiceDetails selectedGuest)
        {
            InitializeComponent();

            //fills the guest data
            refreshGuestInformation(selectedGuest.HotelGuestID);

            //fills the list view
            refreshBookingList();

            Title = "Viewing Guest: " + _CurrentInvoice.GetFullName;
            lvGuestBookings.SetContextMenu(this);
        }

        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = sender.ContextMenuClick<BookingDetails>(out command);
            switch (command)
            {
                case DataGridContextMenuResult.Add:
                    OpenBookingDetail();
                    break;

                case DataGridContextMenuResult.View:
                    OpenBookingDetail(selectedItem, true);
                    break;

                case DataGridContextMenuResult.Edit:
                    OpenBookingDetail(selectedItem);
                    break;

                case DataGridContextMenuResult.Delete:
                    CancelBooking();
                    break;

                default:
                    throw new WanderingTurtleException(this, "Error processing context menu");
            }
        }

        private void OpenBookingDetail(BookingDetails selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    new AddBooking(_CurrentInvoice).ShowDialog();
                    refreshBookingList();
                }
                else
                {
                    if (readOnly)
                    {
                        new EditBooking(_CurrentInvoice, selectedItem, true).ShowDialog();
                        return;
                    }
                    //check if selected item can be edited
                    switch (_bookingManager.CheckToEditBooking(selectedItem))
                    {
                        case (ResultsEdit.CannotEditTooOld):
                            throw new WanderingTurtleException(this, "Bookings in the past cannot be edited.");
                        case (ResultsEdit.Cancelled):
                            throw new WanderingTurtleException(this, "This booking has been cancelled and cannot be edited.");

                        case (ResultsEdit.OkToEdit):
                            if (new EditBooking(_CurrentInvoice, selectedItem).ShowDialog() == false) return;
                            DialogResult = true;
                            refreshBookingList();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void CancelBooking()
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
                        CancelBooking cancel = new CancelBooking((BookingDetails)lvGuestBookings.SelectedItem, _CurrentInvoice);

                        if (cancel.ShowDialog() == false)
                        {
                            DialogResult = true;
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
        /// Opens the AddBooking UI as dialog box
        /// </summary>
        /// <param name="sender">default event parameter</param>
        /// <param name="e">default event parameter</param>
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            OpenBookingDetail();
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
            switch (_invoiceManager.CheckToArchiveInvoice(_CurrentInvoice, myBookingList))
            {
                case (ResultsArchive.CannotArchive):
                    throw new WanderingTurtleException(this, "Guest has bookings in the future and cannot be checked out.", "Warning");

                case (ResultsArchive.OkToArchive):

                    try
                    {
                        //opens UI with guest information
                        ArchiveInvoice myGuest = new ArchiveInvoice(_CurrentInvoice.HotelGuestID);

                        if (myGuest.ShowDialog() == false) return;
                        DialogResult = true;
                        Close();
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
            CancelBooking();
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
            OpenBookingDetail(lvGuestBookings.SelectedItem as BookingDetails);
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
                HotelGuest selectedGuest = _hotelGuestManager.GetHotelGuest(_CurrentInvoice.HotelGuestID);

                //refreshes guest information after AddEditHotelGuest UI
                if (new AddEditHotelGuest(selectedGuest).ShowDialog() == false)
                {
                    refreshGuestInformation(_CurrentInvoice.HotelGuestID);
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void lvGuestBookings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenBookingDetail(sender.RowClick<BookingDetails>(), true);
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
                myBookingList = _invoiceManager.RetrieveGuestBookingDetailsList(_CurrentInvoice.HotelGuestID);

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
                _CurrentInvoice = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);

                lblGuestNameLookup.Content = _CurrentInvoice.GetFullName;
                lblCheckInDate.Content = _CurrentInvoice.DateOpened.ToString(CultureInfo.InvariantCulture);
                lblRoomNum.Content = _CurrentInvoice.GuestRoomNum;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Unable to retrieve guest information from the database.");
            }
        }
    }
}
