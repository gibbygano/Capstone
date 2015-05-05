using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddItemListings.xaml
    /// </summary>
    public partial class AddEditListing
    {
        public ItemListing CurrentItemListing { get; private set; }
        private readonly ProductManager _productManager = new ProductManager();
        private readonly SupplierManager _supplierManager = new SupplierManager();

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
        public AddEditListing(ItemListing currentItemListing, bool readOnly = false)
        {
            CurrentItemListing = currentItemListing;
            Setup();
            Title = "Editing Listing: " + CurrentItemListing.EventName;

            EventCbox.IsEnabled = false;
            SupplierCbox.IsEnabled = false;

            if (readOnly) { (Content as Panel).MakeReadOnly(BtnCancel); }
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
            ItemListing newListing = new ItemListing();

            try
            {
                DateTime formStartDate = (DateTime)(DateStart.SelectedDate);
                DateTime formEndDate = (DateTime)(DateEnd.SelectedDate);

                DateTime formStartTime = (DateTime)(TpStartTime.Value);
                DateTime formEndTime = (DateTime)(TpEndTime.Value);

                //date is your existing Date object, time is the nullable DateTime object from your TimePicker
                newListing.StartDate = DateTime.Parse(string.Format("{0} {1}", formStartDate.ToShortDateString(), formStartTime.ToLongTimeString()));
                newListing.EndDate = DateTime.Parse(string.Format("{0} {1}", formEndDate.ToShortDateString(), formEndTime.ToLongTimeString()));

                if (newListing.StartDate > newListing.EndDate)
                {
                    throw new WanderingTurtleException(this, "End Date must be after Start Date");
                }

                newListing.EventID = ((Event)EventCbox.SelectedItem).EventItemID;
                newListing.SupplierID = ((Supplier)SupplierCbox.SelectedItem).SupplierID;
                newListing.Price = (decimal)(UdPrice.Value);
                newListing.MaxNumGuests = (int)(UdSeats.Value);
            }
            catch (Exception)
            {
                throw new WanderingTurtleException(this, "Please enter valid start and end dates.");
            }

            try
            {
                _productManager.AddItemListing(newListing);
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
        /// Created: 2015/04/06
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
        /// Created: 2015/04/06
        /// Added button to allow Reset of the form fields.  Combined Edit and Add forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            PopulateFields();
        }

        /// Miguel Santana
        /// Created: 2015/04/06
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
        /// Created: 2015/04/06
        /// Method is used to populate fields when editing the listing, otherwise, brings up a blank form.
        /// </summary>
        private void PopulateFields()
        {
            if (CurrentItemListing == null)
            {
                DateStart.Text = null;
                DateEnd.Text = null;

                TpStartTime.Value = null;
                TpEndTime.Value = null;

                EventCbox.SelectedItem = null;
                SupplierCbox.SelectedItem = null;

                UdSeats.Value = 10;
                UdPrice.Value = 0;
            }
            else
            {
                DateStart.Text = CurrentItemListing.StartDate.ToShortDateString();
                DateEnd.Text = CurrentItemListing.EndDate.ToShortDateString();

                TpStartTime.Value = DateTime.Parse(CurrentItemListing.StartDate.ToShortTimeString());
                TpEndTime.Value = DateTime.Parse(CurrentItemListing.EndDate.ToShortTimeString());

                foreach (Event item in EventCbox.Items)
                {
                    if (CurrentItemListing.EventID.Equals(item.EventItemID))
                    { EventCbox.SelectedItem = item; }
                }
                foreach (Supplier item in SupplierCbox.Items)
                {
                    if (CurrentItemListing.SupplierName.Equals(item.CompanyName))
                    { SupplierCbox.SelectedItem = item; }
                }

                UdSeats.Value = CurrentItemListing.MaxNumGuests;
                UdPrice.Value = (double?)CurrentItemListing.Price;
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/04/06
        /// Method is used to initialize the screen to the default presentation.
        /// Merged add and edit ui screens
        /// </summary>
        private void Setup()
        {
            InitializeComponent();
            try
            {
                EventCbox.Items.Clear();
                EventCbox.ItemsSource = DataCache._currentEventList;
                EventCbox.DisplayMemberPath = "EventItemName";
                EventCbox.SelectedValue = "EventItemID";

                SupplierCbox.Items.Clear();
                SupplierCbox.ItemsSource = _supplierManager.RetrieveSupplierList();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }

            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Now);
            DateStart.BlackoutDates.Add(cdr);
            DateEnd.BlackoutDates.Add(cdr);
            PopulateFields();
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
                DateTime formStartDate = (DateTime)(DateStart.SelectedDate);
                DateTime formEndDate = (DateTime)(DateEnd.SelectedDate);
                DateTime formStartTime = (DateTime)(TpStartTime.Value);
                DateTime formEndTime = (DateTime)(TpEndTime.Value);
                ItemListing newListing = new ItemListing
                {
                    ItemListID = CurrentItemListing.ItemListID,
                    EventID = CurrentItemListing.EventID,
                    StartDate = DateTime.Parse(string.Format("{0} {1}", formStartDate.ToShortDateString(), formStartTime.ToLongTimeString())),
                    EndDate = DateTime.Parse(string.Format("{0} {1}", formEndDate.ToShortDateString(), formEndTime.ToLongTimeString())),
                    Price = (decimal)(UdPrice.Value),
                    MaxNumGuests = (int)(UdSeats.Value),
                    CurrentNumGuests = CurrentItemListing.CurrentNumGuests,
                    SupplierID = CurrentItemListing.SupplierID
                };

                var numRows = _productManager.EditItemListing(newListing, CurrentItemListing);

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
        /// Created: 2015/02/16
        /// Validates user input
        /// </summary>
        /// <returns>true if valid</returns>
        private bool Validator()
        {
            if (EventCbox.SelectedIndex.Equals(-1))
            {
                throw new InputValidationException(EventCbox, "Please select an Event to List!");
            }

            if (SupplierCbox.SelectedIndex.Equals(-1))
            {
                throw new InputValidationException(SupplierCbox, "Please select a supplier!");
            }

            if (DateStart.Text == null || DateEnd.Text == null)
            {
                throw new InputValidationException((DateStart.Text == null) ? DateStart : DateEnd, "Please select a date");
            }

            if (TpStartTime.Value == null || TpEndTime.Value == null)
            {
                throw new InputValidationException((TpStartTime.Value == null) ? TpStartTime : TpEndTime, "Please select a time");
            }

            if (UdPrice.Value == 0)
            {
                throw new InputValidationException(UdPrice, "Please indicate a price for tickets");
            }

            if (UdSeats.Value == 0)
            {
                throw new InputValidationException(UdSeats, "Please indicate number of seats for the event");
            }
            return true;
        }
    }
}