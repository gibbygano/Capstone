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
                DialogBox.ShowMessageDialog(this, ex.Message, "There was an error accessing the database");
            }
        }

        private static void UpdateSupplier(Supplier supplierToUpdate)
        {
            new AddSupplier(supplierToUpdate).ShowDialog();
            //addSupplier.FillUpdateList(supplierToUpdate);
        }

        /// <summary>
        /// opens the AddSupplier window
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
            AddSupplier addSupplier;
            addSupplier = new AddSupplier();
            addSupplier.ShowDialog();
        }

        /// <summary>
        /// Will get selected supplier and delete it (archive)
        /// Created by Will Fritz 15/2/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Exception _ex = null;
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
            { _ex = ex; }
            if (_ex != null) { await DialogBox.ShowMessageDialog(this, _ex.Message, "You Must Select A Supplier Before You Can Delete"); _ex = null; }
        }

        private void btnPendingSuppliers_Click(object sender, RoutedEventArgs e)
        {
            new ViewPendingSuppliers().ShowDialog();
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
                DialogBox.ShowMessageDialog(this, ex.Message, "You Must Select A Supplier Before You Can Update");
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
                DialogBox.ShowMessageDialog(this, ex.Message, "There must be data in the list before you can sort it");
            }
        }

        private void lvSuppliersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IInputElement element = e.MouseDevice.DirectlyOver;
            if (element != null && element is FrameworkElement)
            {
                if (((FrameworkElement)element).Parent is DataGridCell)
                {
                    var grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        UpdateSupplier(grid.SelectedItem as Supplier);
                    }
                }
            }
        }
    }
}