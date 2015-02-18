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

        public ListSuppliers()
        {
            InitializeComponent();
            FillList();
        }


        //Fills the list view with the suppliers with a fresh list of suppliers
        //created by Will Fritz 2/6/15
        //edited by will fritz 2/15/15
        public void FillList()
        {
            try
            {
                _suppliers = _manager.RetrieveSupplierList();
            }
            catch (Exception)
            {
                lblError.Content = "there was an error accessing the database";
            }

            lvSuppliersList.Items.Clear();
            lvSuppliersList.ItemsSource = _suppliers;
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Window AddSupplier = new AddSupplier();
            AddSupplier.Show();
        }      

    }
}
