using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListSuppliers.xaml
    /// </summary>
    public partial class ListSuppliers : IDataGridContextMenu
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
        /// <exception cref="WanderingTurtleException">Child window errored during initialization.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="(DataGridContextMenuResult)" /> is null. </exception>
        /// <exception cref="ArgumentException"><paramref name="(DataGridContextMenuResult)" /> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        public ListSuppliers()
        {
            InitializeComponent();
            FillList();
            Instance = this;

            lvSuppliersList.SetContextMenu(this);
        }

        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = DataGridHelper.ContextMenuClick<Supplier>(sender, out command);
            switch (command)
            {
                case DataGridContextMenuResult.Add:
                    OpenSupplier();
                    break;

                case DataGridContextMenuResult.View:
                    OpenSupplier(selectedItem, true);
                    break;

                case DataGridContextMenuResult.Edit:
                    OpenSupplier(selectedItem);
                    break;

                case DataGridContextMenuResult.Archive:
                    ArchiveSupplier();
                    break;

                default:
                    throw new WanderingTurtleException(this, "Error processing context menu");
            }
        }

        private void OpenSupplier(Supplier selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddEditSupplier().ShowDialog() == false) return;
                    FillList();
                }
                else
                {
                    if (new AddEditSupplier(selectedItem, readOnly).ShowDialog() == false) return;
                    if (readOnly) return;
                    FillList();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private async void ArchiveSupplier()
        {
            try
            {
                Supplier supplierToDelete = (Supplier)lvSuppliersList.SelectedItems[0];
                MessageDialogResult result =
                    await
                        DialogBox.ShowMessageDialog(this, "Are you sure you want to delete?", "Confirm Delete",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    _manager.ArchiveSupplier(supplierToDelete);
                }
                FillList();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "You Must Select A Supplier Before You Can Delete");
            }
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
            OpenSupplier();
        }

        /// <summary>
        /// Will get selected supplier and delete it (archive)
        /// Created by Will Fritz 15/2/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ArchiveSupplier();
        }

        private void btnPendingSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem)this.Parent).Content = new ListPendingSuppliers();
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
                OpenSupplier(lvSuppliersList.SelectedItems[0] as Supplier);
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "You Must Select A Supplier Before You Can Update");
            }
        }

        private void lvSuppliersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenSupplier(DataGridHelper.RowClick<Supplier>(sender), true);
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