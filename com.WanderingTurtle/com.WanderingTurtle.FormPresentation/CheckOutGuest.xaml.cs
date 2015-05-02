using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ArchiveInvoice.xaml
    /// </summary>
    public partial class ArchiveInvoice
    {
        private Invoice _currentInvoice { get; set; }

        private List<BookingDetails> _myBookingList;
        private HotelGuest _guestToView;
        private InvoiceManager _invoiceManager = new InvoiceManager();
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private BookingManager _bookingManager = new BookingManager();

        /// <summary>
        /// Pat Banks
        /// Created:  2015/03/03
        /// Constructs a populated form that shows the final charges for a guest and
        /// allows employee to submit to close the invoice
        /// </summary>
        /// <param name="selectedHotelGuestID">Guest selected from the ViewInvoiceUI</param>
        /// <exception cref="WanderingTurtleException">Child window errored during initialization.</exception>
        public ArchiveInvoice(int selectedHotelGuestID)
        {
            try
            {
                InitializeComponent();
                _currentInvoice = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);
                Title = "Archiving Invoice";

                _guestToView = _hotelGuestManager.GetHotelGuest(_currentInvoice.HotelGuestID);
                _myBookingList = _invoiceManager.RetrieveGuestBookingDetailsList(_currentInvoice.HotelGuestID);
                FillFormFields();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/03/03
        /// Populates form fields
        /// </summary>
        private void FillFormFields()
        {
            _currentInvoice.TotalPaid = _invoiceManager.CalculateTotalDue(_myBookingList);
            lblGuestNameLookup.Content = _guestToView.GetFullName;
            lblCheckInDate.Content = _currentInvoice.DateOpened.ToString(CultureInfo.InvariantCulture);
            lblInvoice.Content = _currentInvoice.InvoiceID.ToString();
            lblAddress.Content = _guestToView.Address1;
            lblCityState.Content = _guestToView.CityState.GetZipStateCity;
            lblPhoneNum.Content = _guestToView.PhoneNumber;
            lblRoomNum.Content = _guestToView.Room;
            lblInvoice.Content = _currentInvoice.InvoiceID;
            lblTotalPrice.Content = _currentInvoice.GetTotalFormat;
            lblPhoneNum.Content = _guestToView.PhoneNumber;
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/03/03
        /// Calls methods to archive the associated database records for the selected hotel guest
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated:  2015/03/19
        /// Moved decision logic to Invoice Manager
        /// 
        /// Miguel Santana
        /// Updated:  2015/04/28
        /// Added message box to notify user that report is being generated
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFinalizeInvoice_Click(object sender, RoutedEventArgs e)
        {
            Exception exception = null;
            var controller = await this.ShowProgressAsync("Checkout Guest", "Please wait...");
            await TaskEx.Delay(1000);
            try
            {
                var result = _invoiceManager.ArchiveGuestInvoice(_currentInvoice.HotelGuestID);
                controller.SetMessage("Guest has been checked out!");
                await TaskEx.Delay(2000);
                switch (result)
                {
                    case (ResultsArchive.ChangedByOtherUser):
                        throw new ApplicationException("Record already changed by another user.");

                    case (ResultsArchive.Success):
                        controller.SetMessage("Opening Printable Invoice.");
                        await TaskEx.Delay(10);
                        var invoice = new PrintableInvoice((int)_guestToView.HotelGuestID);
                        await controller.CloseAsync();
                        invoice.ShowDialog();
                        break;

                    default:
                        throw new ApplicationException("Unknown exception checkout out guest.");
                }
            }
            catch (Exception ex) { exception = ex; }
            if (exception != null)
            {
                await controller.CloseAsync();
                throw new WanderingTurtleException(this, exception);
            }
            await this.ShowMessageDialog("Guest checkout complete.");
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/03/07
        /// Handles click event that closes the ui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}