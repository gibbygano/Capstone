﻿using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    public partial class ListHotelGuests : UserControl
    {
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private InvoiceManager _invoiceManager = new InvoiceManager();

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/17
        ///
        /// Initializes the UI that displays a list of active hotel guests
        /// </summary>
        public ListHotelGuests()
        {
            InitializeComponent();
            RefreshGuestList();
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        ///
        /// Opens UI to create a new guest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisterGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddEditHotelGuest addEditHotelGuest = new AddEditHotelGuest();

                //When the UI closes, the Hotel Guest list will refresh
                if (addEditHotelGuest.ShowDialog() == false)
                {
                    RefreshGuestList();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/27
        ///
        /// Populates AddEditInvoice UI based on selected guest
        /// </summary>
        /// <param name="sender">default event arguments</param>
        /// <param name="e">default event arguments</param>
        private void btnViewGuest_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = this.lvHotelGuestList.SelectedItem;

            if (selectedGuest == null)
            {
                throw new WanderingTurtleException(this, "Please select a guest to view.");
            }

            ViewHotelGuest(selectedGuest as InvoiceDetails);
        }

        private void lvHotelGuestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewHotelGuest(DataGridHelper.DataGridRow_Click<InvoiceDetails>(sender, e));
        }

        /// <summary>
        /// Daniel Collingwood
        /// Created: 2015-02-18
        ///
        /// Repopulates the list of hotel guests to display
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated" 2015/03/03
        ///
        /// Changed display items for list of guests retrieved from the invoice manager
        /// </remarks>
        private void RefreshGuestList()
        {
            lvHotelGuestList.ItemsPanel.LoadContent();

            try
            {
                var hotelGuestList = _invoiceManager.RetrieveActiveInvoiceDetails();

                lvHotelGuestList.ItemsSource = hotelGuestList;
                lvHotelGuestList.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Unable to retrieve Hotel Guest listing from the database.");
            }
        }

        private void ViewHotelGuest(InvoiceDetails selectedGuest)
        {
            try
            {
                ViewInvoice custInvoice = new ViewInvoice(selectedGuest);

                if (custInvoice.ShowDialog() == false)
                {
                    RefreshGuestList();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void txtSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnGuestSearch.Content = txtSearchBox.Text.Length == 0 ? "Refresh List" : "Search";
        }

        private void btnGuestSearch_Click(object sender, RoutedEventArgs e)
        {
            var myTempList = _invoiceManager.InvoiceDetailsSearch(txtSearchBox.Text);
            lvHotelGuestList.ItemsSource = myTempList;
            txtSearchBox.Text = "";
        }
    }
}