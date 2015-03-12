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
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for EditListing.xaml
    /// </summary>
    public partial class EditListing : Window
    {
        private EventManager myMan = new EventManager();
        private ProductManager prodMan = new ProductManager();
        private ItemListing ListingOrigin = new ItemListing();
        private ItemListing NewListing = new ItemListing();

        public EditListing(ItemListing toEdit)
        {
            InitializeComponent();
            List<Event> myList = myMan.RetrieveEventList();
            try
            {
                cboxEventTypes.Items.Clear();
                cboxEventTypes.ItemsSource = myList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            txtCurrentGuests.Text = toEdit.CurrentNumGuests.ToString();
            txtMaxGuests.Text = toEdit.MaxNumGuests.ToString();
            txtMinGuests.Text = toEdit.MinNumGuests.ToString();
            txtPrice.Text = toEdit.Price.ToString();
            txtStartDate.Text = toEdit.StartDate.ToString();
            txtEndDate.Text = toEdit.EndDate.ToString();


            ListingOrigin.ItemListID = toEdit.ItemListID;
            ListingOrigin.StartDate = toEdit.StartDate;
            ListingOrigin.EndDate = toEdit.EndDate;
            ListingOrigin.EventID = toEdit.EventID;
            ListingOrigin.Price = toEdit.Price;
            ListingOrigin.MaxNumGuests = toEdit.MaxNumGuests;
            ListingOrigin.MinNumGuests = toEdit.MinNumGuests;
            ListingOrigin.CurrentNumGuests = toEdit.CurrentNumGuests;
            ListingOrigin.SupplierID = toEdit.SupplierID;


    

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewListing.SupplierID = ListingOrigin.SupplierID;
                NewListing.EventID = ListingOrigin.EventID;

                if (Validator.ValidateDecimal(txtPrice.Text))
                {
                    NewListing.Price = Decimal.Parse(txtPrice.Text);
                }
                else
                {
                    throw new Exception("Please enter a valid price");
                }
                if (Validator.ValidateInt(txtCurrentGuests.Text))
                {
                    NewListing.CurrentNumGuests = Int32.Parse(txtCurrentGuests.Text);
                }
                else
                {
                    throw new Exception("Current Number of Guests is invalid.");
                }
                if (Validator.ValidateInt(txtMinGuests.Text))
                {
                    NewListing.MinNumGuests = Int32.Parse(txtMinGuests.Text);
                }
                else
                {
                    throw new Exception("Minimum Number of Guests is invalid");
                }
                if (Validator.ValidateInt(txtMaxGuests.Text))
                {
                    NewListing.MaxNumGuests = Int32.Parse(txtMaxGuests.Text);
                }
                else
                {
                    throw new Exception("Maximum Number of Guests is invalid");
                }
                if (Validator.ValidateDateTime(txtStartDate.Text))
                {
                    NewListing.StartDate = DateTime.Parse(txtStartDate.Text);
                }
                else
                {
                    throw new Exception("Start Date is invalid");
                }
                if (Validator.ValidateDateTime(txtEndDate.Text))
                {
                    NewListing.EndDate = DateTime.Parse(txtEndDate.Text);
                }
                else
                {
                    throw new Exception("Start Date is invalid");
                }
                MessageBox.Show(ListingOrigin.Price.ToString());
                prodMan.EditItemListing(NewListing, ListingOrigin);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
    }
}
