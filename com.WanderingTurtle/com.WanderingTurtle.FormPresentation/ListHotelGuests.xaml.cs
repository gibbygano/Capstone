using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace com.WanderingTurtle.FormPresentation
{
    public partial class ListHotelGuests : UserControl
    {

        /// <summary>
        /// Created by Pat Banks 2015/02/17
        /// Initializes the UI that displays a list of active hotel guests
        /// </summary>
        public ListHotelGuests()
        {
            InitializeComponent();
            refreshList();
        }

        /// <summary>
        /// Created by Daniel Collingwood  2015-02-18
        /// Repopulates the list of hotel guests to display
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated 2015/03/03
        /// Changed display items for list of guests retrieved from the invoice manager
        /// </remarks>
        private void refreshList()
        {
            try
            {
                var hotelGuestList = InvoiceManager.RetrieveAllInvoiceDetails();
                lvHotelGuestList.ItemsSource = hotelGuestList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrieve Hotel Guest listing from the database. \n" + ex.Message);
            }
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        ///
        /// Opens UI to create a new guest
        /// </summary>
        /// <param name="sender">default event parameters</param>
        /// <param name="e">default event parameters</param>
        private void btnRegisterGuest_Click(object sender, RoutedEventArgs e)
        {
            AddEditHotelGuest addEditHotelGuest = new AddEditHotelGuest();

            //When the UI closes, the Hotel Guest list will refresh
            if (addEditHotelGuest.ShowDialog() == false)
            {
                refreshList();
            }
        }

        /// <summary>
        /// Created by Pat Banks 2015/02/27
        ///
        /// Populates AddEditInvoice UI based on selected guest
        /// </summary>
        /// <param name="sender">default event arguments</param>
        /// <param name="e">default event arguments</param>
        private void btnViewGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceDetails selectedGuest = (InvoiceDetails)lvHotelGuestList.SelectedItem;

                if (selectedGuest == null)
                {
                    MessageBox.Show("Please select a row to edit");
                    return;
                }

                ViewInvoice custInvoice = new ViewInvoice(selectedGuest);

                if (custInvoice.ShowDialog() == false)
                {
                    refreshList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a guest and try again");
            }
        }

        /// <summary>
        /// Brings up AddEditHotelGuest to edit chosen guest.
        /// Created By Rose Steffensmeier 2015/02/26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ApplicationException">
        /// When a guest isn't chosen, the exception will throw.
        /// </exception>
        /// <exception cref="Exception">
        /// Unexpected Exception is thrown.
        /// </exception>
        /// <returns>nothing</returns>
        private void btnUpdateGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HotelGuest thisGuest = (HotelGuest)lvHotelGuestList.SelectedItem;

                if (thisGuest == null)
                    throw new ApplicationException("You must choose a guest.");

                AddEditHotelGuest temp = new AddEditHotelGuest(thisGuest);
                temp.Show();
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Archives a guest, then refreshes the list.
        /// Created by Rose Steffensmeier 2015/02/26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ApplicationException">
        /// When a guest has not been chosen.
        /// </exception>
        private void btnArchiveGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HotelGuest thisGuest = (HotelGuest)lvHotelGuestList.SelectedItem;

                if (thisGuest == null)
                    throw new ApplicationException("You must choose a guest.");

                HotelGuestManager.ArchiveHotelGuest(thisGuest, !thisGuest.Active);
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Class level variables needed for sorting method
        private ListSortDirection _sortDirection;
        private GridViewColumnHeader _sortColumn;

        /// <summary>
        /// This method will sort the listview column in both asending and desending order
        /// Created by Will Fritz 15/2/27
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvHotelGuestListHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.OriginalSource as GridViewColumnHeader;
            if (column == null)
            {
                return;
            }

            if (_sortColumn == column)
            {
                // Toggle sorting direction 
                _sortDirection = _sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
            }

            string header = string.Empty;

            // if binding is used and property name doesn't match header content
            // Rose - while working on Mask, somehow hit a NullReferenceException here, please take a look
            Binding b = _sortColumn.Column.DisplayMemberBinding as Binding;

            if (b != null)
            {
                header = b.Path.Path;
            }
            try
            {
                ICollectionView resultDataView = CollectionViewSource.GetDefaultView(lvHotelGuestList.ItemsSource);
                resultDataView.SortDescriptions.Clear();
                resultDataView.SortDescriptions.Add(new SortDescription(header, _sortDirection));
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("There must be data in the list before you can sort it");
            }
        }
    }
}