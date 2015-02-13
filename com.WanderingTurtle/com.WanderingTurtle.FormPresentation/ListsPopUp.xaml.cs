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
    /// Interaction logic for ListsPopUp.xaml
    /// </summary>
    public partial class ListsPopUp : Window
    {
        public static ListsPopUp Instance;
        private ProductMangager _prodMang = new ProductMangager();
        private SupplierManger _supplierMang = new SupplierManger();
        private List<ItemListing> _itemListings;
        private List<Supplier> _suppliers;
        private bool _isEdit = false; //used to decide weather it will create or edit a lists object
        private Lists _oldLists;

        public ListsPopUp()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillLists();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }

        //check if listings and supplier has been selected and creates new lists item if _isEdit = false and vise versa
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemListing itemListing = (ItemListing)lvList.SelectedItems[0];
                Supplier supplier = (Supplier)cboSelectSupplier.SelectedItem;

                Lists newLists = new Lists();
                newLists.ItemListingID = itemListing.ItemListingID;
                newLists.SupplierID = supplier.SupplierID;
                newLists.DateListed = new DateTime();

                if (_isEdit == false)
                {
                    _prodMang.AddLists(newLists);
                }
                else 
                {
                    _prodMang.EditLists(_oldLists, newLists);
                }

                //opens old window and closes this one
                if (ListsView.Instance == null)
                {
                    var listsWin = new ListsView();
                    listsWin.Show();
                }
                else
                {
                    var listsWin = ListsView.Instance;
                    listsWin.BringIntoView();
                    listsWin.Show();
                    listsWin.FillList();

                    System.Media.SystemSounds.Asterisk.Play();
                }
                this.Close();

            }
            catch (Exception)
            {
                lblError.Content = "You Must Select a ItemListing and a Supplier";
            }
        }


        ////////////////////////Custome Methods/////////////////////////

        //fills the list view and combo box with data
        //created by Will Fritz 2/11/15
        public void FillLists()
        {
            _itemListings = _prodMang.RetrieveItemListingList();
            _suppliers = _supplierMang.RetriveSupplierList();
            
            lvList.Items.Clear();
            lvList.ItemsSource = _itemListings;

            cboSelectSupplier.ItemsSource = _suppliers;
        }

        //makes the form edit a current lists item, and will select the old values in the form
        //created by will fritz 2/11/15
        public void isEdit(Lists oldLists)
        {
            _isEdit = true;
            _oldLists = oldLists;

            for (int x = 0; x < _suppliers.Count; x++)
            {
                if (oldLists.SupplierID == _suppliers[x].SupplierID)
                {
                    cboSelectSupplier.SelectedIndex = x;
                }
            }

            for (int x = 0; x < _itemListings.Count; x++)
            {
                if (oldLists.ItemListID == _itemListings[x].ItemListID)
                {
                    lvList.SelectedIndex = x;
                }
            }
        }
        //makes the form add a lists item
        //created by will fritz 2/11/15
        public void isAdd()
        {
            _isEdit = false;
        }
    }
}
