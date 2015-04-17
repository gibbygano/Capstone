﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ArchiveInvoice.xaml
    /// </summary>
    public partial class ArchiveInvoice
    {
        private List<BookingDetails> myBookingList;
        private Invoice invoiceToArchive;
        private Invoice originalInvoice;
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
                originalInvoice = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);
                invoiceToArchive = _invoiceManager.RetrieveInvoiceByGuest(selectedHotelGuestID);

                guestToView = _hotelGuestManager.GetHotelGuest(invoiceToArchive.HotelGuestID);
                myBookingList = _invoiceManager.RetrieveGuestBookingDetailsList(invoiceToArchive.HotelGuestID);
                FillFormFields(selectedHotelGuestID);
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
        private void FillFormFields(int selectedHotelGuestID)
        {
            invoiceToArchive.TotalPaid = _invoiceManager.CalculateTotalDue(myBookingList);
            lblGuestNameLookup.Content = guestToView.GetFullName;
            lblCheckInDate.Content = invoiceToArchive.DateOpened.ToString(CultureInfo.InvariantCulture);
            lblInvoice.Content = invoiceToArchive.InvoiceID.ToString();
            lblAddress.Content = guestToView.Address1;
            lblCityState.Content = guestToView.CityState.GetZipStateCity;
            lblPhoneNum.Content = guestToView.PhoneNumber;
            lblRoomNum.Content = guestToView.Room;
            lblInvoice.Content = invoiceToArchive.InvoiceID;
            lblTotalPrice.Content = invoiceToArchive.GetTotalFormat;
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
                ResultsArchive result = _invoiceManager.ArchiveCurrentGuestInvoice(invoiceToArchive);

                switch (result)
                {
                    case (ResultsArchive.ChangedByOtherUser):
                        throw new ApplicationException("Record already changed by another user.");

                    case (ResultsArchive.Success):
                        await DialogBox.ShowMessageDialog(this, "Guest checkout complete.");
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
            this.DialogResult = false;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {


            //if (newReportWindow.ShowDialog() == false)
            //{
            //    RefreshEmployeeList();
            //}
        }
    }
}