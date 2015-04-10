using com.WanderingTurtle.BusinessLogic;
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
    /// Interaction logic for ViewPendingSuppliers.xaml
    /// </summary>
    public partial class ViewPendingSuppliers
    {
        private List<SupplierApplication> GetPendingSuppliers = new List<SupplierApplication>();
        private SupplierManager supplierManager = new SupplierManager();

        public ViewPendingSuppliers()
        {
            InitializeComponent();
            loadPendingSuppliers();

        }
        

        private static void UpdatePendingSupplier(SupplierApplication selectedItem)
        {
            new AddEditPendingSupplier(selectedItem).ShowDialog();
        }

        private void btnAddPendingSupplier_Click(object sender, RoutedEventArgs e)
        {
            new AddEditPendingSupplier().ShowDialog();
        }

        private void btnUpdatePendingSupplier_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.lvPendingSuppliers.SelectedItem;
            if (selectedItem == null)
            {
                DialogBox.ShowMessageDialog(this, "Please select a row to edit");
                return;
            }
            UpdatePendingSupplier(selectedItem as SupplierApplication);
        }

        private void lvPendingSuppliers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdatePendingSupplier(DataGridHelper.DataGridRow_Click<SupplierApplication>(sender, e));
        }

        private void loadPendingSuppliers()
        {
            try
            {

                GetPendingSuppliers = supplierManager.RetrieveSupplierApplicationList();

            }
            catch (Exception)
            {

                return;
            }

            lvPendingSuppliers.ItemsSource = GetPendingSuppliers;

        }
    }
}