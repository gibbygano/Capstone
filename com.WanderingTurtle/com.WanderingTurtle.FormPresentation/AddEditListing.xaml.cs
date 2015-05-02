using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddItemListings.xaml
    /// </summary>
    public partial class AddEditListing
    {
        public ItemListing CurrentItemListing { get; private set; }
        private EventManager _eventManager = new EventManager();
        private ProductManager _productManager = new ProductManager();
        private SupplierManager _supplierManager = new SupplierManager();

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/05
        /// Initializes the AddEditListing screen
        /// Combined the Edit/Add screens
        /// </summary>
        public AddEditListing()
        {
            Setup();
            Title = "Add a new Listing";
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/05
        /// Initializes the AddEditListing screen if it is edit or readonly
        /// Combined the Edit/Add screens
        /// </summary>
        /// <exception cref="WanderingTurtleException">Occurs making components readonly.</exception>
        public AddEditListing(ItemListing currentItemListing, bool ReadOnly = false)
        {
            CurrentItemListing = currentItemListing;
            Setup();
            Title = "Editing Listing: " + CurrentItemListing.EventName;

            eventCbox.IsEnabled = false;
            supplierCbox.IsEnabled = false;

            if (ReadOnly) { WindowHelper.MakeReadOnly(Content as Panel, btnCancel); }
        }

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/02/18
        /// validates the user input and passes data to Business logic
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated:  2015/04/05
        /// Combined Edit and Add screens
        /// </remarks>
        private async void AddItemListing()
        {
            if (!Validator()) return;
            ItemListing _NewListing = new ItemListing();

            try
            {
                DateTime formStartDate = (DateTime)(dateStart.SelectedDate);
                DateTime formEndDate = (DateTime)(dateEnd.SelectedDate);

                DateTime formStartTime = (DateTime)(tpStartTime.Value);
                DateTime formEndTime = (DateTime)(tpEndTime.Value);

                //date is your existing Date object, time is the nullable DateTime object from your TimePicker
                _NewListing.StartDate = DateTime.Parse(string.Format("{0} {1}", formStartDate.ToShortDateString(), formStartTime.ToLongTimeString()));
                _NewListing.EndDate = DateTime.Parse(string.Format("{0} {1}", formEndDate.ToShortDateString(), formEndTime.ToLongTimeString()));

                if (_NewListing.StartDate > _NewListing.EndDate)
                {
                    throw new WanderingTurtleException(this, "End Date must be after Start Date");
                }

                _NewListing.EventID = ((Event)eventCbox.SelectedItem).EventItemID;
                _NewListing.SupplierID = ((Supplier)supplierCbox.SelectedItem).SupplierID;
                _NewListing.Price = (decimal)(udPrice.Value);
                _NewListing.MaxNumGuests = (int)(udSeats.Value);
            }
            catch (Exception)
            {
                throw new WanderingTurtleException(this, "Please enter valid start and end dates.");
            }

            try
            {
                _productManager.AddItemListing(_NewListing);
                await this.ShowMessageDialog("Listing successfully added!");
                DialogResult = true;
                Close();
            }
            catch (Exception)
            {
                throw new WanderingTurtleException(this, "Error adding the Item Listing.");
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Added button to allow cancel of the form function.  Combined Edit and Add forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Added button to allow Reset of the form fields.  Combined Edit and Add forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            populateFields();
        }

        /// Miguel Santana
        /// Created 2015/04/06
        /// Logic rearranged when combining the add and edit forms.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentItemListing == null)
            { AddItemListing(); }
            else
            { UpdateItemListing(); }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Method is used to populate fields when editing the listing, otherwise, brings up a blank form.
        /// </summary>
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
                    if (CurrentItemListing.EventID.Equals(item.EventItemID))
                    { eventCbox.SelectedItem = item; }
                }
                foreach (Supplier item in supplierCbox.Items)
                {
                    if (CurrentItemListing.SupplierName.Equals(item.CompanyName))
                    { supplierCbox.SelectedItem = item; }
                }

                udSeats.Value = CurrentItemListing.MaxNumGuests;
                udPrice.Value = (double?)CurrentItemListing.Price;
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Method is used to initialize the screen to the default presentation.
        /// Merged add and edit ui screens
        /// </summary>
        private void Setup()
        {
            InitializeComponent();
            try
            {
                eventCbox.Items.Clear();
                eventCbox.ItemsSource = DataCache._currentEventList;
                eventCbox.DisplayMemberPath = "EventItemName";
                eventCbox.SelectedValue = "EventItemID";

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

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/02/10
        /// Takes input from form to update the item listing
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated:  2015/02/19
        /// fields updated to reflect all required data
        /// Added spinners and calendar for user input restrictions
        /// </remarks>
        private async void UpdateItemListing()
        {
            try
            {
                DateTime formStartDate = (DateTime)(dateStart.SelectedDate);
                DateTime formEndDate = (DateTime)(dateEnd.SelectedDate);
                DateTime formStartTime = (DateTime)(tpStartTime.Value);
                DateTime formEndTime = (DateTime)(tpEndTime.Value);
                ItemListing NewListing = new ItemListing
                {
                    ItemListID = CurrentItemListing.ItemListID,
                    EventID = CurrentItemListing.EventID,
                    StartDate = DateTime.Parse(string.Format("{0} {1}", formStartDate.ToShortDateString(), formStartTime.ToLongTimeString())),
                    EndDate = DateTime.Parse(string.Format("{0} {1}", formEndDate.ToShortDateString(), formEndTime.ToLongTimeString())),
                    Price = (decimal)(udPrice.Value),
                    MaxNumGuests = (int)(udSeats.Value),
                    CurrentNumGuests = CurrentItemListing.CurrentNumGuests,
                    SupplierID = CurrentItemListing.SupplierID
                };

                var numRows = _productManager.EditItemListing(NewListing, CurrentItemListing);

                if (numRows == listResult.Success)
                {
                    await this.ShowMessageDialog("Item successfully changed.");
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "There was an error updating the Item Listing.");
            }
        }

        /// <summary>
        /// Hunter Lind
        /// Created 2015/02/16
        /// Validates user input
        /// </summary>
        /// <returns>true if valid</returns>
        private bool Validator()
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