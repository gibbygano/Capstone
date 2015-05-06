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
        private List<SupplierApplication> _getPendingSuppliers = new List<SupplierApplication>();
        private readonly SupplierManager _supplierManager = new SupplierManager();

        /// <exception cref="ArgumentNullException"><see cref="DataGridContextMenuResult"/> is null. </exception>
        /// <exception cref="ArgumentException"><see cref="DataGridContextMenuResult"/> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ListPendingSuppliers()
        {
            InitializeComponent();
            LoadPendingSuppliers();

            LvPendingSuppliers.SetContextMenu(DataGridContextMenuResult.View, DataGridContextMenuResult.Edit);
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/15
        /// Logic for context menus
        /// </summary>
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

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/15
        /// opens pending supplier dialog
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="readOnly"></param>
        private void OpenPendingSupplier(SupplierApplication selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddEditPendingSupplier().ShowDialog() == false) return;
                    LoadPendingSuppliers();
                }
                else
                {
                    if (new AddEditPendingSupplier(selectedItem, readOnly).ShowDialog() == false) return;
                    if (readOnly) return;
                    LoadPendingSuppliers();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/10
        /// Opens pending supplier approval window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdatePendingSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (LvPendingSuppliers.SelectedItem != null)
                OpenPendingSupplier(LvPendingSuppliers.SelectedItem as SupplierApplication);
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/10
        /// Changes the suppliers content from viewing pending suppliers to viewing approved suppliers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewApprovedSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem)Parent).Content = new ListSuppliers();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/10
        /// Changes the suppliers content from viewing approved suppliers to viewing pending suppliers
        /// </summary>
        private void LoadPendingSuppliers()
        {
            LvPendingSuppliers.ItemsPanel.LoadContent();

            try
            {
                _getPendingSuppliers = _supplierManager.RetrieveSupplierApplicationList();
                LvPendingSuppliers.ItemsSource = _getPendingSuppliers;
                LvPendingSuppliers.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// created:  2015/04/10
        /// Opens pending suppliers with a double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvPendingSuppliers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenPendingSupplier(sender.RowClick<SupplierApplication>(), true);
        }
    }
}