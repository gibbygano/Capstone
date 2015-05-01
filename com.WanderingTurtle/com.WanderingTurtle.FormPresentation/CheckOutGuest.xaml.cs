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
        private Invoice _CurrentInvoice { get; set; }

        private List<BookingDetails> myBookingList;
        private HotelGuest guestToView;
        private InvoiceManager _invoiceManager = new InvoiceManager();
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private BookingManager _bookingManager = new BookingManager();

        /// <summary>
        /// Created by Pat Banks 2015/03/03
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
                _CurrentInvoice = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);
                Title = "Archiving Invoice";

                guestToView = _hotelGuestManager.GetHotelGuest(_CurrentInvoice.HotelGuestID);
                myBookingList = _invoiceManager.RetrieveGuestBookingDetailsList(_CurrentInvoice.HotelGuestID);
                FillFormFields();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        /// Populates form fields
        /// </summary>
        private void FillFormFields()
        {
            _CurrentInvoice.TotalPaid = _invoiceManager.CalculateTotalDue(myBookingList);
            lblGuestNameLookup.Content = guestToView.GetFullName;
            lblCheckInDate.Content = _CurrentInvoice.DateOpened.ToString(CultureInfo.InvariantCulture);
            lblInvoice.Content = _CurrentInvoice.InvoiceID.ToString();
            lblAddress.Content = guestToView.Address1;
            lblCityState.Content = guestToView.CityState.GetZipStateCity;
            lblPhoneNum.Content = guestToView.PhoneNumber;
            lblRoomNum.Content = guestToView.Room;
            lblInvoice.Content = _CurrentInvoice.InvoiceID;
            lblTotalPrice.Content = _CurrentInvoice.GetTotalFormat;
            lblPhoneNum.Content = guestToView.PhoneNumber;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        ///
        /// Calls methods to archive the associated database records for the selected hotel guest
        /// </summary>
        /// <remarks>
        /// Updated by Pat Banks 2015/03/19
        /// Moved decision logic to Invoice Manager
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
                var result = _invoiceManager.ArchiveGuestInvoice(_CurrentInvoice.HotelGuestID);
                controller.SetMessage("Guest has been checked out!");
                await TaskEx.Delay(2000);
                switch (result)
                {
                    case (ResultsArchive.ChangedByOtherUser):
                        throw new ApplicationException("Record already changed by another user.");

                    case (ResultsArchive.Success):
                        controller.SetMessage("Opening Printable Invoice.");
                        await TaskEx.Delay(10);
                        var invoice = new PrintableInvoice((int)guestToView.HotelGuestID);
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
        /// Created by Pat Banks 2015/03/07
        /// Handles click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}