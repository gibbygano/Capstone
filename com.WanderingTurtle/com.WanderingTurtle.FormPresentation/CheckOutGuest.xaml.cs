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
        private Invoice CurrentInvoice { get; set; }

        private readonly List<BookingDetails> _myBookingList;
        private readonly HotelGuest _guestToView;
        private readonly InvoiceManager _invoiceManager = new InvoiceManager();
        private readonly HotelGuestManager _hotelGuestManager = new HotelGuestManager();

        /// <summary>
        /// Pat Banks
        /// Created:  2015/03/03
        /// Constructs a populated form that shows the final charges for a guest and
        /// allows employee to submit to close the invoice
        /// </summary>
        /// <param name="selectedHotelGuestId">Guest selected from the ViewInvoiceUI</param>
        /// <exception cref="WanderingTurtleException">Child window errored during initialization.</exception>
        public ArchiveInvoice(int selectedHotelGuestId)
        {
            try
            {
                InitializeComponent();
                CurrentInvoice = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestId);
                Title = "Archiving Invoice";

                _guestToView = _hotelGuestManager.GetHotelGuest(CurrentInvoice.HotelGuestID);
                _myBookingList = _invoiceManager.RetrieveGuestBookingDetailsList(CurrentInvoice.HotelGuestID);
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
            CurrentInvoice.TotalPaid = _invoiceManager.CalculateTotalDue(_myBookingList);
            LblGuestNameLookup.Content = _guestToView.GetFullName;
            LblCheckInDate.Content = CurrentInvoice.DateOpened.ToString(CultureInfo.InvariantCulture);
            LblInvoice.Content = CurrentInvoice.InvoiceID.ToString();
            LblAddress.Content = _guestToView.Address1;
            LblCityState.Content = _guestToView.CityState.GetZipStateCity;
            LblPhoneNum.Content = _guestToView.PhoneNumber;
            LblRoomNum.Content = _guestToView.Room;
            LblInvoice.Content = CurrentInvoice.InvoiceID;
            LblTotalPrice.Content = CurrentInvoice.GetTotalFormat;
            LblEmail.Content = _guestToView.EmailAddress;
            LblPhoneNum.Content = _guestToView.PhoneNumber;
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
                var result = _invoiceManager.ArchiveGuestInvoice(CurrentInvoice.HotelGuestID);
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