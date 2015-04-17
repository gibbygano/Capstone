﻿using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
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
using System.Windows.Shapes;

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
            else { UpdatePendingSupplier(selectedItem as SupplierApplication); }

            
       }

        private void btnViewApprovedSuppliers_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as TabItem).Content = new ListSuppliers();
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