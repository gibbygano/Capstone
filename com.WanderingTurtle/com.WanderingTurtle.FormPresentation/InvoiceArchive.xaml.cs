using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        /// <param name="selectedHotelGuestID"></param>
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
            try
            {
                ResultsArchive result = _invoiceManager.ArchiveCurrentGuestInvoice(_CurrentInvoice);

                switch (result)
                {
                    case (ResultsArchive.ChangedByOtherUser):
                        throw new ApplicationException("Record already changed by another user.");

                    case (ResultsArchive.Success):
                        await DialogBox.ShowMessageDialog(this, "Guest checkout complete.");
                        DialogResult = true;
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/07
        /// Handles click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintableInvoice newReportWindow = new PrintableInvoice((int)guestToView.HotelGuestID);

            if (newReportWindow.ShowDialog() == false)
            {
                //RefreshEmployeeList();
            }
        }
    }
}
