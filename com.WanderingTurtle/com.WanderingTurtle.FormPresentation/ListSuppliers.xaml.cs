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
    /// Interaction logic for ListSuppliers.xaml
    /// </summary>
    public partial class ListSuppliers : UserControl
    {
        public static ListSuppliers Instance;
        private SupplierManager _manager = new SupplierManager();
        private GridViewColumnHeader _sortColumn;

        //Class level variables needed for sorting method
        private ListSortDirection _sortDirection;

        private List<Supplier> _suppliers;

        /// <summary>
        /// This will fill the list of suppliers and set this object to the "Instance variable"
        /// Created by will fritz 15/2/6
        /// </summary>
        public ListSuppliers()
        {
            InitializeComponent();
            FillList();
            Instance = this;
        }

        /// <summary>
        /// Fills the list view with the suppliers with a fresh list of suppliers
        /// created by Will Fritz 15/2/6
        /// </summary>
        /// <remarks>
        /// edited by will fritz 15/2/19
        /// </remarks>
        public void FillList()
        {
            try
            {
                lvSuppliersList.ItemsSource = null;
                _suppliers = _manager.RetrieveSupplierList();
                lvSuppliersList.Items.Clear();
                lvSuppliersList.ItemsSource = _suppliers;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "There was an error accessing the database");
            }
        }

        private void UpdateSupplier(Supplier supplierToUpdate, bool ReadOnly = false)
        {
            try
            {
                new AddEditSupplier(supplierToUpdate, ReadOnly).ShowDialog();
                //addSupplier.FillUpdateList(supplierToUpdate);
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// opens the AddEditSupplier window
        /// ceated by Pat 15/2/6
        /// </summary>
        /// <remarks>
        /// Edited to make it a singleton pattern
        /// By Will Fritz 15/3/6
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddEditSupplier addSupplier;
                addSupplier = new AddEditSupplier();
                addSupplier.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Will get selected supplier and delete it (archive)
        /// Created by Will Fritz 15/2/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Supplier supplierToDelete = (Supplier)lvSuppliersList.SelectedItems[0];
                MessageDialogResult result = await DialogBox.ShowMessageDialog(this, "Are you sure you want to delete?", "Confirm Delete", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    _manager.ArchiveSupplier(supplierToDelete);
                }
                FillList();
            }
            catch (Exception ex)
            { throw new WanderingTurtleException(this, ex, "You Must Select A Supplier Before You Can Delete"); }
        }

        private void btnPendingSuppliers_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as TabItem).Content = new ListPendingSuppliers();
        }

        /// <summary>
        /// Will get selected supplier and fill the add/edit tab with info
        /// Created by Will Fritz 15/2/6
        /// </summary>
        /// <remarks>
        /// Edited to make it a singleton pattern
        /// By Will Fritz 15/3/6
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItem = this.lvSuppliersList.SelectedItem;

            //if(selectedItem == null)
            //{
            //     throw new WanderingTurtleException(this, "You Must Select A Supplier Before You Can Update");
            //}
            //else
            //{
            //    UpdateSupplier(selectedItem as Supplier);
            //}

            try
            {
                UpdateSupplier(lvSuppliersList.SelectedItems[0] as Supplier);
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "You Must Select A Supplier Before You Can Update");
            }
        }

        /// <summary>
        /// This method will sort the listview column in both asending and desending order
        /// Created by Will Fritz 15/2/27
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSupplierListHeaderClick(object sender, RoutedEventArgs e)
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
                ICollectionView resultDataView = CollectionViewSource.GetDefaultView(lvSuppliersList.ItemsSource);
                resultDataView.SortDescriptions.Clear();
                resultDataView.SortDescriptions.Add(new SortDescription(header, _sortDirection));
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "There must be data in the list before you can sort it");
            }
        }

        private void lvSuppliersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateSupplier(DataGridHelper.DataGridRow_Click<Supplier>(sender, e), true);
        }

        private void btnSearchSupplier_Click(object sender, RoutedEventArgs e)
        {
            var myList = _manager.searchSupplier(txtSearchSupplier.Text);
            lvSuppliersList.ItemsSource = myList;
            lvSuppliersList.Items.Refresh();
        }

        private void txtSearchSupplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtSearchSupplier.Text.Length == 0)
            {
                btnSearchSupplier.Content = "Refresh List";
            }
            else
            {
                btnSearchSupplier.Content = "Search";
            }
        }
    }
}