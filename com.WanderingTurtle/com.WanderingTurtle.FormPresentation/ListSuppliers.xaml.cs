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
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.FormPresentation
{

    /// <summary>
    /// Interaction logic for ListSuppliers.xaml
    /// </summary>
    public partial class ListSuppliers : UserControl
    {
        private SupplierManager _manager = new SupplierManager();
        private List<Supplier> _suppliers;
        public static ListSuppliers Instance;

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
        /// opens the AddSupplier window
        /// ceated by Pat 15/2/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Window AddSupplier = new AddSupplier();
            AddSupplier.Show();
        }

        /// <summary>
        /// Will get selected supplier and delete it (archive)
        /// Created by Will Fritz 15/2/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    Supplier supplierToDelete = (Supplier)lvSuppliersList.SelectedItems[0];
                    
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Confirm Delete", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        _manager.ArchiveSupplier(supplierToDelete);
                    }
                    FillList();
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("You Must Select A Supplier Before You Can Delete");
                }
        }

        /// <summary>
        /// Will get selected supplier and fill the add/edit tab with info
        /// Created by Will Fritz 15/2/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Supplier supplierToUpdate = (Supplier)lvSuppliersList.SelectedItems[0];
                AddSupplier addSupplier = new AddSupplier();                
                addSupplier.Show();
                addSupplier.FillUpdateList(supplierToUpdate);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("You Must Select A Supplier Before You Can Update");
            }
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
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("there was an error accessing the database");
            }

        }
    }
}
