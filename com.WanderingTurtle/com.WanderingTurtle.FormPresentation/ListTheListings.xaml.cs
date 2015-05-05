using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly BookingManager _bookingManager = new BookingManager();
        private readonly ProductManager _productManager = new ProductManager();

        /// <exception cref="ArgumentNullException"><see cref="DataGridContextMenuResult"/> is null. </exception>
        /// <exception cref="ArgumentException"><see cref="DataGridContextMenuResult"/> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ListTheListings()
        {
            InitializeComponent();

            if (Globals.UserToken.Level.Equals(RoleData.Valet) || Globals.UserToken.Level.Equals(RoleData.Concierge))
            {
                BtnAddListing.IsEnabled = false;
                BtnArchiveListing.IsEnabled = false;
                BtnEditListing.IsEnabled = false;
            }

            RefreshData();

            LvListing.SetContextMenu();
        }

        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = sender.ContextMenuClick<ItemListing>(out command);

            if (Globals.UserToken.Level.Equals(RoleData.Valet) || Globals.UserToken.Level.Equals(RoleData.Concierge))
            {
                switch (command)
                {
                    case DataGridContextMenuResult.View:
                        OpenListing(selectedItem, true);
                        break;

                    default:
                        throw new WanderingTurtleException(this, "You do not have access to this function.");
                }
            }
            else
            {
                switch (command)
                {
                    case DataGridContextMenuResult.Add:
                        if (Globals.UserToken.Level.Equals(RoleData.Admin))
                        {
                            OpenListing();
                        }
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
        }

        private void OpenListing(ItemListing selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddEditListing().ShowDialog() == false) return;
                    RefreshData();
                }
                else
                {
                    if (new AddEditListing(selectedItem, readOnly).ShowDialog() == false) return;
                    if (readOnly) return;
                    RefreshData();
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
            ItemListing listingToDelete = LvListing.SelectedItem as ItemListing;

            try
            {
                //need to check if there are any Bookings associated with this listing.
                Debug.Assert(listingToDelete != null, "listingToDelete != null");
                ResultsArchive results = _bookingManager.CheckListingArchive(listingToDelete.ItemListID);

                if (results.Equals(ResultsArchive.CannotArchive))
                {
                    throw new WanderingTurtleException(this, "There are bookings associated with this listing and cannot be archived.");
                }
                MessageDialogResult result = await this.ShowMessageDialog("Are you sure you want to delete this?", "Confirm Delete", MessageDialogStyle.AffirmativeAndNegative);
                switch (result)
                {
                    case MessageDialogResult.Affirmative:
                        var numRows = _productManager.ArchiveItemListing(listingToDelete);
                        if (numRows == listResult.Success)
                        {
                            await this.ShowMessageDialog("Listing successfully deleted.");
                        }
                        RefreshData();
                        break;
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
            OpenListing(LvListing.SelectedItem as ItemListing);
        }

        private void lvListing_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenListing(sender.RowClick<ItemListing>(), true);
        }

        private void RefreshData()
        {
            try
            {
                _myListingList = _productManager.RetrieveItemListingList();
                foreach (ItemListing item in _myListingList)
                {
                    item.Seats = (item.MaxNumGuests - item.CurrentNumGuests);
                }
                LvListing.ItemsSource = _myListingList;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "No database able to be accessed for Listings");
            }
        }

        private void txtSearchListing_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtnSearchListing.Content = TxtSearchListing.Text.Length == 0 ? "Refresh List" : "Search";
        }

        private void btnSearchListing_Click(object sender, RoutedEventArgs e)
        {
            var myList = _productManager.SearchItemLists(TxtSearchListing.Text);
            foreach (ItemListing item in myList)
            {
                item.Seats = (item.MaxNumGuests - item.CurrentNumGuests);
            }
            LvListing.ItemsSource = myList;
            LvListing.Items.Refresh();
        }
    }
}