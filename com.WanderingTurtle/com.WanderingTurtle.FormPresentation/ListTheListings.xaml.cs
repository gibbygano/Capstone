using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
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
    /// <summary>
    /// Interaction logic for ListTheListings.xaml
    /// </summary>
    public partial class ListTheListings : UserControl
    {
        private ProductManager prodMan = new ProductManager();
        private List<ItemListing> myListingList;

        public ListTheListings()
        {
            InitializeComponent();
            refreshData();
        }

        private void refreshData()
        {
            try
            {
                myListingList = prodMan.RetrieveItemListingList();
                foreach (ItemListing item in myListingList)
                {
                    item.Seats = (item.MaxNumGuests - item.CurrentNumGuests);
                }
                lvListing.ItemsSource = myListingList;
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "No database able to be accessed for Listings");
            }
        }

        private void btnAddListing_Click(object sender, RoutedEventArgs e)
        {
            Window AddItemListings = new AddItemListing();
            //Commented out by Justin Penningtonon 3/10/2015 4:02 AM causes errors due to ShowDailog only being able to be used on hidden
            //AddItemListings.Show();
            if (AddItemListings.ShowDialog() == false)
            {
                refreshData();
            }
        }

        private async void btnArchiveListing_click(object sender, RoutedEventArgs e)
        {
            ItemListing ListingToDelete = (ItemListing)lvListing.SelectedItem;
            if (ListingToDelete == null)
            {
                await DialogBox.ShowMessageDialog(this, "Please select a row to delete.");
                return;
            }
            Exception _ex = null;
            try
            {
                int numRows;
                MessageDialogResult result = await DialogBox.ShowMessageDialog(this, "Are you sure you want to delete this?", "Confirm Delete", MessageDialogStyle.AffirmativeAndNegative);
                switch (result)
                {
                    case MessageDialogResult.Affirmative:
                        numRows = prodMan.ArchiveItemListing(ListingToDelete);
                        if (numRows == 1)
                        {
                            await DialogBox.ShowMessageDialog(this, "Listing successfully deleted.");
                        }
                        refreshData();
                        break;
                }
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            if (_ex != null) { await DialogBox.ShowMessageDialog(this, _ex.Message); _ex = null; }
        }

        private void btnEditListing_click(object sender, RoutedEventArgs e)
        {
            ItemListing ListingEdit = (ItemListing)lvListing.SelectedItem;
            if (ListingEdit == null)
            {
                DialogBox.ShowMessageDialog(this, "Please select a row to edit");
                return;
            }

            Window EditListings = new EditListing(ListingEdit);

            //Commented out by Justin Penningtonon 3/10/2015 4:02 AM causes errors due to ShowDailog only being able to be used on hidden
            //AddItemListings.Show();
            if (EditListings.ShowDialog() == false)
            {
                refreshData();
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
        private void lvListingsListHeaderClick(object sender, RoutedEventArgs e)
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
            Binding b = _sortColumn.Column.DisplayMemberBinding as Binding;

            if (b != null)
            {
                header = b.Path.Path;
            }

            try
            {
                ICollectionView resultDataView = CollectionViewSource.GetDefaultView(lvListing.ItemsSource);
                resultDataView.SortDescriptions.Clear();
                resultDataView.SortDescriptions.Add(new SortDescription(header, _sortDirection));
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "There must be data in the list before you can sort it");
            }
        }
    }
}