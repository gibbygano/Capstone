using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListTheListings.xaml
    /// </summary>
    public partial class ListTheListings : IDataGridContextMenu
    {
        private List<ItemListing> _myListingList;
        private BookingManager _bookingManager = new BookingManager();
        private ProductManager _productManager = new ProductManager();

        /// <exception cref="ArgumentNullException"><see cref="DataGridContextMenuResult"/> is null. </exception>
        /// <exception cref="ArgumentException"><see cref="DataGridContextMenuResult"/> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ListTheListings()
        {
            InitializeComponent();
            refreshData();

            lvListing.SetContextMenu(this);
        }

        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = sender.ContextMenuClick<ItemListing>(out command);
            switch (command)
            {
                case DataGridContextMenuResult.Add:
                    OpenListing();
                    break;

                case DataGridContextMenuResult.View:
                    OpenListing(selectedItem, true);
                    break;

                case DataGridContextMenuResult.Edit:
                    OpenListing(selectedItem);
                    break;

                case DataGridContextMenuResult.Delete:
                    ArchiveListing();
                    break;

                default:
                    throw new WanderingTurtleException(this, "Error processing context menu");
            }
        }

        private void OpenListing(ItemListing selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddEditListing().ShowDialog() == false) return;
                    refreshData();
                }
                else
                {
                    if (new AddEditListing(selectedItem, readOnly).ShowDialog() == false) return;
                    if (readOnly) return;
                    refreshData();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }


        /// <summary>
        /// Pat Banks
        /// Updated 2015/04/26
        /// added logic so that a listing with guests booked cannot be archived.
        /// </summary>
        private async void ArchiveListing()
        {
            ItemListing ListingToDelete = lvListing.SelectedItem as ItemListing;
            
            try
            {
                //need to check if there are any Bookings associated with this listing.
                ResultsArchive results = _bookingManager.CheckListingArchive(ListingToDelete.ItemListID);

                if (results.Equals(ResultsArchive.CannotArchive))
                {
                    throw new WanderingTurtleException(this, "There are bookings associated with this listing and cannot be archived.");
                }
                else
                {
                    MessageDialogResult result = await this.ShowMessageDialog("Are you sure you want to delete this?", "Confirm Delete", MessageDialogStyle.AffirmativeAndNegative);
                    switch (result)
                    {
                        case MessageDialogResult.Affirmative:
                            var numRows = _productManager.ArchiveItemListing(ListingToDelete);
                            if (numRows == listResult.Success)
                            {
                                await this.ShowMessageDialog("Listing successfully deleted.");
                            }
                            refreshData();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void btnAddListing_Click(object sender, RoutedEventArgs e)
        {
            OpenListing();
        }

        private void btnArchiveListing_click(object sender, RoutedEventArgs e)
        {
            ArchiveListing();
        }

        private void btnEditListing_click(object sender, RoutedEventArgs e)
        {
            OpenListing(lvListing.SelectedItem as ItemListing);
        }

        private void lvListing_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenListing(sender.RowClick<ItemListing>(), true);
        }

        private void refreshData()
        {
            try
            {
                _myListingList = _productManager.RetrieveItemListingList();
                foreach (ItemListing item in _myListingList)
                {
                    item.Seats = (item.MaxNumGuests - item.CurrentNumGuests);
                }
                lvListing.ItemsSource = _myListingList;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "No database able to be accessed for Listings");
            }
        }

        private void txtSearchListing_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSearchListing.Content = txtSearchListing.Text.Length == 0 ? "Refresh List" : "Search";
        }

        private void btnSearchListing_Click(object sender, RoutedEventArgs e)
        {
            var myList = _productManager.SearchItemLists(txtSearchListing.Text);
            foreach (ItemListing item in myList)
            {
                item.Seats = (item.MaxNumGuests - item.CurrentNumGuests);
            }
            lvListing.ItemsSource = myList;
            lvListing.Items.Refresh();
        }
    }
}