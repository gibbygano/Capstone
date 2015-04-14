using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

// Worked on by:
///Hunter
////Fritz
/////Matthew 10:15
namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddItemListings.xaml
    /// </summary>
    public partial class AddEditListing
    {
        private EventManager _eventManager = new EventManager();
        private ProductManager _productManager = new ProductManager();
        private SupplierManager _supplierManager = new SupplierManager();

        //populates our Combo box for the user to pick from
        public AddEditListing()
        {
            Setup();
        }

        public AddEditListing(ItemListing CurrentItemListing, bool ReadOnly = false)
        {
            this.CurrentItemListing = CurrentItemListing;
            Setup();

            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] { BtnCancel }); }
        }

        public ItemListing CurrentItemListing { get; private set; }

        private void addItemListing()
        {
            ItemListing _NewListing = new ItemListing();
            if (!Valdiator()) return;

            try
            {
                DateTime formStartDate = (DateTime)(dateStart.SelectedDate);
                DateTime formEndDate = (DateTime)(dateEnd.SelectedDate);

                DateTime formStartTime = (DateTime)(tpStartTime.Value);
                DateTime formEndTime = (DateTime)(tpEndTime.Value);

                _NewListing.EventID = Int32.Parse(eventCbox.SelectedValue.ToString());
                _NewListing.SupplierID = Int32.Parse(supplierCbox.SelectedValue.ToString());

                //date is your existing Date object, time is the nullable DateTime object from your TimePicker
                _NewListing.StartDate = DateTime.Parse(string.Format("{0} {1}", formStartDate.ToShortDateString(), formStartTime.ToLongTimeString()));
                _NewListing.EndDate = DateTime.Parse(string.Format("{0} {1}", formEndDate.ToShortDateString(), formEndTime.ToLongTimeString()));

                _NewListing.Price = (decimal)(udPrice.Value);
                _NewListing.MaxNumGuests = (int)(udSeats.Value);
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }

            try
            {
                _productManager.AddItemListing(_NewListing);
                DialogBox.ShowMessageDialog(this, "Listing successfully added!");
                this.Close();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error adding the Item Listing.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            populateFields();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentItemListing == null)
            { addItemListing(); }
            else
            { updateItemListing(); }
        }

        private void populateFields()
        {
            if (CurrentItemListing == null)
            {
                dateStart.Text = null;
                dateEnd.Text = null;

                tpStartTime.Value = null;
                tpEndTime.Value = null;

                eventCbox.SelectedItem = null;
                supplierCbox.SelectedItem = null;

                udSeats.Value = 10;
                udPrice.Value = 0;
            }
            else
            {
                dateStart.Text = CurrentItemListing.StartDate.ToShortDateString();
                dateEnd.Text = CurrentItemListing.EndDate.ToShortDateString();

                tpStartTime.Value = DateTime.Parse(CurrentItemListing.StartDate.ToShortTimeString());
                tpEndTime.Value = DateTime.Parse(CurrentItemListing.EndDate.ToShortTimeString());

                foreach (Event item in eventCbox.Items)
                {
                    if (CurrentItemListing.EventName.Equals(item.EventItemName))
                    { eventCbox.SelectedItem = item; }
                }
                foreach (Supplier item in supplierCbox.Items)
                {
                    if (CurrentItemListing.SupplierName.Equals(item.CompanyName))
                    { supplierCbox.SelectedItem = item; }
                }
                //eventCbox.SelectedItem = CurrentItemListing.EventName.ToString();
                //lblSupplierName.Content = CurrentItemListing.SupplierName.ToString();

                udSeats.Value = CurrentItemListing.MaxNumGuests;
                udPrice.Value = (double?)CurrentItemListing.Price;
            }
        }

        private void Setup()
        {
            InitializeComponent();
            try
            {
                eventCbox.Items.Clear();
                eventCbox.ItemsSource = _eventManager.RetrieveEventList();
                supplierCbox.Items.Clear();
                supplierCbox.ItemsSource = _supplierManager.RetrieveSupplierList();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }

            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Now);
            dateStart.BlackoutDates.Add(cdr);
            dateEnd.BlackoutDates.Add(cdr);

            populateFields();
        }

        private void updateItemListing()
        {
            try
            {
                DateTime formStartDate = (DateTime)(dateStart.SelectedDate);
                DateTime formEndDate = (DateTime)(dateEnd.SelectedDate);
                DateTime formStartTime = (DateTime)(tpStartTime.Value);
                DateTime formEndTime = (DateTime)(tpEndTime.Value);
                ItemListing NewListing = new ItemListing();
                NewListing.ItemListID = CurrentItemListing.ItemListID;
                NewListing.EventID = CurrentItemListing.EventID;
                NewListing.StartDate = DateTime.Parse(string.Format("{0} {1}", formStartDate.ToShortDateString(), formStartTime.ToLongTimeString()));
                NewListing.EndDate = DateTime.Parse(string.Format("{0} {1}", formEndDate.ToShortDateString(), formEndTime.ToLongTimeString()));
                NewListing.Price = (decimal)(udPrice.Value);
                NewListing.MaxNumGuests = (int)(udSeats.Value);
                NewListing.CurrentNumGuests = CurrentItemListing.CurrentNumGuests;
                NewListing.SupplierID = CurrentItemListing.SupplierID;

                var numRows = _productManager.EditItemListing(NewListing, CurrentItemListing);
                if (numRows == ProductManager.listResult.Success)
                {
                    DialogBox.ShowMessageDialog(this, "Item successfully changed.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "There was an error updating the Item Listing.");
            }
        }

        private bool Valdiator()
        {
            if (eventCbox.SelectedIndex.Equals(-1))
            {
                throw new InputValidationException(eventCbox, "Please select an Event to List!");
            }

            if (supplierCbox.SelectedIndex.Equals(-1))
            {
                throw new InputValidationException(supplierCbox, "Please select a supplier!");
            }

            if (dateStart.Text == null || dateEnd.Text == null)
            {
                throw new InputValidationException((dateStart.Text == null) ? dateStart : dateEnd, "Please select a date");
            }

            if (tpStartTime.Value == null || tpEndTime.Value == null)
            {
                throw new InputValidationException((tpStartTime.Value == null) ? tpStartTime : tpEndTime, "Please select a time");
            }

            if (udPrice.Value == 0)
            {
                throw new InputValidationException(udPrice, "Please indicate a price for tickets");
            }

            if (udSeats.Value == 0)
            {
                throw new InputValidationException(udSeats, "Please indicate number of seats for the event");
            }
            return true;
        }
    }
}