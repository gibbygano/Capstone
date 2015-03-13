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
using com.WanderingTurtle.FormPresentation.Models;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for EditListing.xaml
    /// </summary>
    public partial class EditListing 
    {
        private EventManager myMan = new EventManager();
        private ProductManager prodMan = new ProductManager();
        private ItemListing ListingOrigin = new ItemListing();
        private ItemListing NewListing = new ItemListing();

        public EditListing(ItemListing toEdit)
        {
            InitializeComponent();
            populateFields(toEdit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toEdit"></param>
        private void populateFields(ItemListing toEdit)
        {
            dateStart.Text = toEdit.StartDate.ToShortDateString();
            dateEnd.Text = toEdit.EndDate.ToShortDateString();

            tpStartTime.Value = DateTime.Parse(toEdit.StartDate.ToShortTimeString());
            tpEndTime.Value = DateTime.Parse(toEdit.EndDate.ToShortTimeString());

            lblListingName.Content = toEdit.EventName.ToString();
            lblSupplierName.Content = toEdit.SupplierName.ToString();

            udSeats.Value = toEdit.MaxNumGuests;
            udPrice.Value = (decimal)(toEdit.Price);

            dateStart.DisplayDate = toEdit.StartDate;
            dateEnd.DisplayDate = toEdit.EndDate;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime formStartDate = (DateTime)(dateStart.SelectedDate);
                DateTime formEndDate = (DateTime)(dateEnd.SelectedDate);
                DateTime formStartTime = (DateTime)(tpStartTime.Value);
                DateTime formEndTime = (DateTime)(tpEndTime.Value);
                NewListing.ItemListID = ListingOrigin.ItemListID;

                NewListing.EventID = ListingOrigin.EventID;
                NewListing.StartDate = DateTime.Parse(string.Format("{0} {1}", formStartDate.ToShortDateString(), formStartTime.ToLongTimeString()));
                NewListing.EndDate = DateTime.Parse(string.Format("{0} {1}", formEndDate.ToShortDateString(), formEndTime.ToLongTimeString()));
                NewListing.Price = (decimal)(udPrice.Value);
                NewListing.MaxNumGuests = (int)(udSeats.Value);
                NewListing.CurrentNumGuests = ListingOrigin.CurrentNumGuests;
                NewListing.SupplierID = ListingOrigin.SupplierID;


                int numRows = prodMan.EditItemListing(NewListing, ListingOrigin);
                if (numRows == 1)
                {
                    DialogBox.ShowMessageDialog(this, "Item successfully changed.");
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.ToString());
            }
        }
        
    }
}
