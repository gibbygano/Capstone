using System;
using System.Collections.Generic;
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
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;
using System.ComponentModel;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListTheListings.xaml
    /// </summary>
    public partial class ListTheListings : UserControl
    {

        private ProductManager prodMan = new ProductManager();
        List<ItemListing> myListingList;

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
                MessageBox.Show("No database able to be accessed for Listings");
                //MessageBox.Show(ex.ToString());
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void btnArchiveListing_click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemListing ListingToDelete = (ItemListing)lvListing.SelectedItems[0];
                MessageBox.Show(prodMan.ArchiveItemListing(ListingToDelete).ToString());

                refreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("There must be data in the list before you can sort it");
            }
        }

        private void btnEditListing_click(object sender, RoutedEventArgs e)
        {
            ItemListing ListingEdit = (ItemListing)lvListing.SelectedItem;
            Window EditListings = new EditListing(ListingEdit);
            //Commented out by Justin Penningtonon 3/10/2015 4:02 AM causes errors due to ShowDailog only being able to be used on hidden 
            //AddItemListings.Show();
            if (EditListings.ShowDialog() == false)
            {
                refreshData();
            }

        }
    }

}

