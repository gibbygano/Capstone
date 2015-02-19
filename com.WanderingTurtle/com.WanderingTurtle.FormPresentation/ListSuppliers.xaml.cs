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

        public ListSuppliers()
        {
            InitializeComponent();
            FillList();
            Instance = this;
        }

        //opens the AddSupplier window
        //ceated by Pat
        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Window AddSupplier = new AddSupplier();
            AddSupplier.Show();
        }

        //Will get selected supplier and delete it (archive)
        //created by Will Fritz 2/6/15
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Supplier supplierToDelete = (Supplier)lvSuppliersList.SelectedItems[0];
                _manager.ArchiveSupplier(supplierToDelete);
                FillList();
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message;//"You Must Select A Supplier Before You Can Delete";
            }

        }

        //Will get selected supplier and fill the add/edit tab with info
        //created by Will Fritz 2/6/15
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Supplier supplierToUpdate = (Supplier)lvSuppliersList.SelectedItems[0];
                AddSupplier addSupplier = new AddSupplier();
                addSupplier.FillUpdateList(supplierToUpdate);
                addSupplier.Show();
            }
            catch (Exception)
            {
                lblError.Content = "You Must Select A Supplier Before You Can Update";
            }
        }

        //Fills the list view with the suppliers with a fresh list of suppliers
        //created by Will Fritz 2/6/15
        //edited by will fritz 2/19/15
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
                lblError.Content = "there was an error accessing the database";
            }

        }
    }
}
