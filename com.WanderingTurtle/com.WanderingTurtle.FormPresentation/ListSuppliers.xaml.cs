using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;

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
        /// <exception cref="WanderingTurtleException">Failed to get suppliers list.</exception>
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
        /// <exception cref="WanderingTurtleException">Child window errored during initialization.</exception>
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
        /// created by Pat 15/2/6
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
                var addSupplier = new AddEditSupplier();
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
            ((TabItem) this.Parent).Content = new ListPendingSuppliers();
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
            try
            {
                UpdateSupplier(lvSuppliersList.SelectedItems[0] as Supplier);
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "You Must Select A Supplier Before You Can Update");
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
            btnSearchSupplier.Content = txtSearchSupplier.Text.Length == 0 ? "Refresh List" : "Search";
        }
    }
}