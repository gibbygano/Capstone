using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    public partial class ListHotelGuests : IDataGridContextMenu
    {
        private readonly InvoiceManager _invoiceManager = new InvoiceManager();

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/17
        /// Initializes the UI that displays a list of active hotel guests
        /// </summary>
        /// <exception cref="ArgumentNullException"><see cref="DataGridContextMenuResult"/> is null. </exception>
        /// <exception cref="ArgumentException"><see cref="DataGridContextMenuResult"/> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ListHotelGuests()
        {
            InitializeComponent();
            RefreshGuestList();

            LvHotelGuestList.SetContextMenu(DataGridContextMenuResult.Add, DataGridContextMenuResult.View);
        }

        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = sender.ContextMenuClick<InvoiceDetails>(out command);
            switch (command)
            {
                case DataGridContextMenuResult.Add:
                    OpenHotelGuest();
                    break;

                case DataGridContextMenuResult.View:
                    OpenHotelGuest(selectedItem);
                    break;

                default:
                    throw new WanderingTurtleException(this, "Error processing context menu");
            }
        }

        private void OpenHotelGuest(InvoiceDetails selectedItem = null)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddEditHotelGuest().ShowDialog() == false) return;
                    RefreshGuestList();
                }
                else
                {
                    if (new ViewInvoice(selectedItem).ShowDialog() == false) return;
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
        /// Created: 2015/03/03
        ///
        /// Opens UI to create a new guest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisterGuest_Click(object sender, RoutedEventArgs e)
        {
            OpenHotelGuest();
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
            OpenHotelGuest(LvHotelGuestList.SelectedItem as InvoiceDetails);
        }

        private void lvHotelGuestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenHotelGuest(sender.RowClick<InvoiceDetails>());
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
            LvHotelGuestList.ItemsPanel.LoadContent();

            try
            {
                var hotelGuestList = _invoiceManager.RetrieveActiveInvoiceDetails();

                LvHotelGuestList.ItemsSource = hotelGuestList;
                LvHotelGuestList.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Unable to retrieve Hotel Guest listing from the database.");
            }
        }

        private void txtSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtnGuestSearch.Content = TxtSearchBox.Text.Length == 0 ? "Refresh List" : "Search";
        }

        private void btnGuestSearch_Click(object sender, RoutedEventArgs e)
        {
            var myTempList = _invoiceManager.InvoiceDetailsSearch(TxtSearchBox.Text);
            LvHotelGuestList.ItemsSource = myTempList;
            TxtSearchBox.Text = "";
        }
    }
}