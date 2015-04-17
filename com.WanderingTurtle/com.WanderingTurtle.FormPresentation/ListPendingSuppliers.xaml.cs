using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListPendingSuppliers.xaml
    /// </summary>
    public partial class ListPendingSuppliers
    {
        private List<SupplierApplication> GetPendingSuppliers = new List<SupplierApplication>();
        private SupplierManager supplierManager = new SupplierManager();

        public ListPendingSuppliers()
        {
            InitializeComponent();
            loadPendingSuppliers();
        }

        private void UpdatePendingSupplier(SupplierApplication selectedItem, bool ReadOnly = false)
        {
            try
            {
                if (new AddEditPendingSupplier(selectedItem, ReadOnly).ShowDialog() == false)
                {
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
            var selectedItem = this.lvPendingSuppliers.SelectedItem;
            if (selectedItem == null)
            {
                throw new WanderingTurtleException(this, "Please select a row to edit");
            }
            UpdatePendingSupplier(selectedItem as SupplierApplication);
        }

        private void btnViewApprovedSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem) this.Parent).Content = new ListSuppliers();
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
            UpdatePendingSupplier(DataGridHelper.DataGridRow_Click<SupplierApplication>(sender, e), true);
        }
    }
}