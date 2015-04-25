using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListPendingSuppliers.xaml
    /// </summary>
    public partial class ListPendingSuppliers : IDataGridContextMenu
    {
        private List<SupplierApplication> GetPendingSuppliers = new List<SupplierApplication>();
        private SupplierManager supplierManager = new SupplierManager();

        /// <exception cref="ArgumentNullException"><see cref="DataGridContextMenuResult"/> is null. </exception>
        /// <exception cref="ArgumentException"><see cref="DataGridContextMenuResult"/> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ListPendingSuppliers()
        {
            InitializeComponent();
            loadPendingSuppliers();

            lvPendingSuppliers.SetContextMenu(this, DataGridContextMenuResult.View, DataGridContextMenuResult.Edit);
        }

        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = sender.ContextMenuClick<SupplierApplication>(out command);
            switch (command)
            {
                case DataGridContextMenuResult.View:
                    OpenPendingSupplier(selectedItem, true);
                    break;

                case DataGridContextMenuResult.Edit:
                    OpenPendingSupplier(selectedItem);
                    break;

                default:
                    throw new WanderingTurtleException(this, "Error processing context menu");
            }
        }

        private void OpenPendingSupplier(SupplierApplication selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddEditPendingSupplier().ShowDialog() == false) return;
                    loadPendingSuppliers();
                }
                else
                {
                    if (new AddEditPendingSupplier(selectedItem, readOnly).ShowDialog() == false) return;
                    if (readOnly) return;
                    loadPendingSuppliers();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void btnUpdatePendingSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (lvPendingSuppliers.SelectedItem != null)
                OpenPendingSupplier(lvPendingSuppliers.SelectedItem as SupplierApplication);
        }

        private void btnViewApprovedSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem)Parent).Content = new ListSuppliers();
        }

        private void loadPendingSuppliers()
        {
            lvPendingSuppliers.ItemsPanel.LoadContent();

            try
            {
                GetPendingSuppliers = supplierManager.RetrieveSupplierApplicationList();
                lvPendingSuppliers.ItemsSource = GetPendingSuppliers;
                lvPendingSuppliers.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void lvPendingSuppliers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenPendingSupplier(sender.RowClick<SupplierApplication>(), true);
        }
    }
}