using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Presents user with guest invoice information
    /// </summary>
    public partial class ViewInvoice : IDataGridContextMenu
    {
        private InvoiceDetails CurrentInvoice { get; set; }

        private readonly BookingManager _bookingManager = new BookingManager();
        private readonly HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private readonly InvoiceManager _invoiceManager = new InvoiceManager();
        private List<BookingDetails> _bookingDetailsList;
        private bool Result = false;

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/2015
        /// Displays information for the selected guest's invoice
        /// </summary>
        /// <param name="selectedGuest">Selected guest to retrieve</param>
        /// <exception cref="ArgumentNullException"><see cref="DataGridContextMenuResult"/> is null. </exception>
        /// <exception cref="ArgumentException"><see cref="DataGridContextMenuResult"/> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ViewInvoice(InvoiceDetails selectedGuest)
        {
            InitializeComponent();

            //fills the guest data
            RefreshGuestInformation(selectedGuest.HotelGuestID);

            //fills the list view
            RefreshBookingList();

            Title = "Viewing Guest: " + CurrentInvoice.GetFullName;
            LvGuestBookings.SetContextMenu();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/20
        /// Pops up context menus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="WanderingTurtleException">Error getting specified parent.</exception>
        public void ContextMenuItemClick(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/06
        /// added read-only capability to the ui
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="readOnly"></param>
        private void OpenBookingDetail(BookingDetails selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddBooking(CurrentInvoice).ShowDialog() == false) return;
                    RefreshBookingList();
                }
                else
                {
                    if (readOnly)
                    {
                        new EditBooking(CurrentInvoice, selectedItem, true).ShowDialog();
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
                            if (new EditBooking(CurrentInvoice, selectedItem).ShowDialog() == false) return;
                            RefreshBookingList();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Tony Noel
        /// Created:  2015/03/20
        /// UI to confirm details for cancelling a booking
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/19
        /// Moved logic checks to invoice manager - checkToArchiveInvoice
        /// </remarks>
        private void CancelBooking()
        {
            //check if something was selected
            if (LvGuestBookings.SelectedItem == null)
            {
                throw new WanderingTurtleException(this, "Please select a booking to cancel.");
            }

            //check if selected item can be cancelled
            ResultsEdit result = _bookingManager.CheckToEditBooking((BookingDetails)LvGuestBookings.SelectedItem);

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
                        CancelBooking cancel = new CancelBooking((BookingDetails)LvGuestBookings.SelectedItem, CurrentInvoice);

                        if (cancel.ShowDialog() == false) return;
                        RefreshBookingList();
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
        /// Opens the ArchiveInvoice UI as dialog box
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/19
        /// Moved logic checks to invoice manager - checkToArchiveInvoice
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArchiveInvoice_Click(object sender, RoutedEventArgs e)
        {
            //check if invoice can be closed
            switch (_invoiceManager.CheckToArchiveInvoice(CurrentInvoice, _bookingDetailsList))
            {
                case (ResultsArchive.CannotArchive):
                    throw new WanderingTurtleException(this, "Guest has bookings in the future and cannot be checked out.", "Warning");

                case (ResultsArchive.OkToArchive):

                    try
                    {
                        //opens UI with guest information
                        ArchiveInvoice myGuest = new ArchiveInvoice(CurrentInvoice.HotelGuestID);

                        if (myGuest.ShowDialog() == false) return;
                        Result = true;
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
        /// Cancel booking button to open cancel form.
        /// First attempts to create a BookingDetails object from the lvCustomerBookings,
        /// then passes this to the CancelBooking form if the object creation was successful.
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/19
        /// Moved logic checks to booking manager CheckToCancelBooking
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
        /// Opens the EditBooking UI as dialog box
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/19
        /// Moved logic checks to Business Logic Layer - CheckToEditBooking
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {
            OpenBookingDetail(LvGuestBookings.SelectedItem as BookingDetails);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        /// Opens the EditGuest UI as dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //retrieve the guest information
                HotelGuest selectedGuest = _hotelGuestManager.GetHotelGuest(CurrentInvoice.HotelGuestID);

                //refreshes guest information after AddEditHotelGuest UI
                if (new AddEditHotelGuest(selectedGuest).ShowDialog() == false) return;
                RefreshGuestInformation(CurrentInvoice.HotelGuestID);
                Result = true;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/22
        /// Opens read only view of the booking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvGuestBookings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenBookingDetail(sender.RowClick<BookingDetails>(), true);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        /// Calls the InvoiceManager method that retrieves a list of booking details for a selected guest
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/08
        /// Added info to show the user how many bookings the guest has signed up for.
        /// </remarks>
        private void RefreshBookingList()
        {
            LvGuestBookings.ItemsPanel.LoadContent();
            try
            {
                _bookingDetailsList = _invoiceManager.RetrieveGuestBookingDetailsList(CurrentInvoice.HotelGuestID);

                LvGuestBookings.ItemsSource = _bookingDetailsList;
                LvGuestBookings.Items.Refresh();

                //check if bookings have been cancelled
                int bookingCount = _bookingDetailsList.Count(booking => booking.Quantity > 0);

                if (_bookingDetailsList.Count == 0)
                {
                    LblBookingsMessage.Content = "No bookings scheduled.";
                }
                else if (_bookingDetailsList.Count > 0 && bookingCount == 0)
                {
                    LblBookingsMessage.Content = "All bookings have been cancelled.";
                }
                else
                {
                    LblBookingsMessage.Content = "Guest has " + bookingCount + " booking(s).";
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Unable to retrieve booking list from the database.");
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        /// Calls the InvoiceManager method that retrieves the guest's invoice information
        /// and stores the information in invoiceToView
        /// </summary>
        /// <param name="selectedHotelGuestId">selected guest's id</param>
        private void RefreshGuestInformation(int selectedHotelGuestId)
        {
            try
            {
                //object to store guest's information
                CurrentInvoice = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestId);

                LblGuestNameLookup.Content = CurrentInvoice.GetFullName;
                LblCheckInDate.Content = CurrentInvoice.DateOpened.ToString(CultureInfo.InvariantCulture);
                LblRoomNum.Content = CurrentInvoice.GuestRoomNum;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Unable to retrieve guest information from the database.");
            }
        }

        private void MetroWindow_Closing(object sender, EventArgs e)
        {
            DialogResult = Result;
        }
    }
}