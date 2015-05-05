using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// This Window allows the administrator to directly add a supplier
    /// </summary>
    public partial class AddEditSupplier
    {
        private readonly SupplierLoginManager _loginManager = new SupplierLoginManager();
        private readonly SupplierManager _manager = new SupplierManager();
        private readonly string _supplierUserName;
        private readonly Supplier _updatableSupplier;
        private List<CityState> _zips;

        /// <summary>
        /// Will Fritz
        /// Created 2015/02/06
        /// Set up of ui screen, combo box fill
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated 2015/04/21
        /// Normalized screen titles
        /// </remarks>
        public AddEditSupplier()
        {
            InitializeComponent();
            Title = "Add a new Supplier";
            FillComboBox();
            FillUpdateList();
        }

        /// <summary>
        /// Will Fritz
        /// Created:  2015/02/06
        /// initializes screen
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated:  2015/04/09
        /// combined add/edit screens
        /// </remarks>
        /// <param name="supplierToEdit"></param>
        /// <param name="readOnly"></param>
        /// <exception cref="WanderingTurtleException">Error setting the fields as read only.</exception>
        public AddEditSupplier(Supplier supplierToEdit, bool readOnly = false)
        {
            InitializeComponent();
            _updatableSupplier = supplierToEdit;
            Title = "Editing Supplier: " + _updatableSupplier.GetFullName;

            //retrieve the username
            _supplierUserName = _loginManager.RetrieveSupplierUserName(supplierToEdit.SupplierID);

            FillComboBox();
            FillUpdateList();

            TxtUserName.IsEnabled = false;

            if (readOnly) { (Content as Panel).MakeReadOnly(); }
        }

        /// <summary>
        /// Will Fritz
        /// Created:  2015/02/06
        /// fills the add/edit tab fields with the data from a selected Supplier from the list view
        /// </summary>
        /// <remarks>
        /// Will Fritz
        /// Updated:  2015/02/15
        /// Changed zip to a drop down
        /// </remarks>
        public void FillUpdateList()
        {
            if (_updatableSupplier == null)
            {
                TxtCompanyName.Text = null;
                TxtFirstName.Text = null;
                TxtLastName.Text = null;
                TxtAddress1.Text = null;
                TxtAddress2.Text = null;
                TxtEmail.Text = null;
                TxtPhoneNumber.Text = null;
                TxtUserName.Text = null;
                //making user name items visible so that they can be changed
                TxtUserName.Visibility = Visibility.Visible;
                LblUserName.Visibility = Visibility.Visible;
                CboZip.SelectedItem = null;
                NumSupplyCost.Value = .70;
            }
            else
            {
                TxtCompanyName.Text = _updatableSupplier.CompanyName.Trim();
                TxtFirstName.Text = _updatableSupplier.FirstName.Trim();
                TxtLastName.Text = _updatableSupplier.LastName.Trim();
                TxtAddress1.Text = _updatableSupplier.Address1.Trim();
                TxtAddress2.Text = _updatableSupplier.Address2.Trim();
                TxtEmail.Text = _updatableSupplier.EmailAddress.Trim();
                TxtPhoneNumber.Text = _updatableSupplier.PhoneNumber.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                TxtUserName.Text = _supplierUserName;
                //making user name items invisible so that they can't be changed
                TxtUserName.Visibility = Visibility.Hidden;
                LblUserName.Visibility = Visibility.Hidden;
                foreach (CityState cityState in _zips.Where(cityState => cityState.Zip == _updatableSupplier.Zip)) { CboZip.SelectedValue = cityState.Zip; }
                NumSupplyCost.Value = (double)(_updatableSupplier.SupplyCost);
                //cboZip.SelectedValue = supplierUpdate.Zip;
            }
        }

        /// <summary>
        /// Will Fritz
        /// Created: 2015/02/04
        /// checks to see if all the fields are fill out and formated with the correct data
        /// </summary>
        /// <returns>false if there is an invalid fields and will output an error message to the lblError label</returns>
        /// <exception cref="InputValidationException">Validation Error Handling.</exception>
        public bool Validate()
        {
            if (!TxtCompanyName.Text.Trim().ValidateCompanyName())
            {
                throw new InputValidationException(TxtCompanyName, "Company Name field must be filled out and not contain special characters");
            }
            if (!TxtEmail.Text.Trim().ValidateEmail())
            {
                throw new InputValidationException(TxtEmail, "Not a valid e-mail address");
            }
            if (!TxtPhoneNumber.Text.ValidatePhone())
            {
                throw new InputValidationException(TxtPhoneNumber, "The phone number cannot start with a 1 and must filled out and be formated correctly (10 numeric digits)");
            }
            if (CboZip.SelectedItem == null)
            {
                throw new InputValidationException(CboZip, "You must select an zip from the drop down");
            }
            if (!TxtAddress1.Text.Trim().ValidateAlphaNumeric())
            {
                throw new InputValidationException(TxtAddress1, "The address must be filled out and not contain special characters (spaces allowed)");
            }
            if (!TxtFirstName.Text.Trim().ValidateString())
            {
                throw new InputValidationException(TxtFirstName, "The first name field filled out and must not contain special characters (No Spaces)");
            }
            if (!TxtLastName.Text.Trim().ValidateString())
            {
                throw new InputValidationException(TxtLastName, "The last name field must be filled out and not contain special characters (No Spaces)");
            }
            if (!TxtUserName.Text.Trim().ValidateAlphaNumeric())
            {
                throw new InputValidationException(TxtUserName, "Enter a valid user name.");
            }
            if (!NumSupplyCost.Value.ToString().ValidateDecimal())
            {
                throw new InputValidationException(NumSupplyCost, "Enter a valid supply cost.");
            }
            return true;
        }

        /// <summary>
        /// Will Fritz
        /// Created: 2015/02/06
        /// This will send a supplier object to the business logic layer
        /// </summary>
        /// <remarks>
        /// Will Fritz
        /// Updated:  2015/02/15
        /// Added a confirmation message box
        /// </remarks>
        private async void AddTheSupplier()
        {
            try
            {
                bool validUserName = _loginManager.CheckSupplierUserName(TxtUserName.Text);

                if (validUserName)
                {
                    Supplier tempSupplier = new Supplier
                    {
                        CompanyName = TxtCompanyName.Text.Trim(),
                        FirstName = TxtFirstName.Text.Trim(),
                        LastName = TxtLastName.Text.Trim(),
                        Address1 = TxtAddress1.Text.Trim(),
                        Address2 = TxtAddress2.Text.Trim(),
                        PhoneNumber = TxtPhoneNumber.Text,
                        Zip = CboZip.SelectedValue.ToString(),
                        EmailAddress = TxtEmail.Text.Trim(),
                        SupplyCost = (decimal)NumSupplyCost.Value
                    };

                    if (_manager.AddANewSupplier(tempSupplier, TxtUserName.Text) == SupplierResult.Success)
                    {
                        await this.ShowMessageDialog("Supplier was added to the database.");
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        throw new WanderingTurtleException(this, "Supplier wasn't added to the database.");
                    }
                }
                else
                {
                    TxtUserName.Text = "";
                    throw new WanderingTurtleException(this, "UserName already used.  Please choose another one.");
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/21
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
        /// Created 2015/04/21
        /// Added button to allow Reset of the form fields.  Combined Edit and Add forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            FillUpdateList();
        }

        /// <summary>
        /// Will Fritz
        /// Created:  2015/02/06
        /// Validates the fields and call add/edit supplier method
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated 2015/04/14
        /// Added UserName field
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) { return; }

            if (_updatableSupplier == null)
            {
                AddTheSupplier();
            }
            else
            {
                EditSupplier();
            }
        }

        /// <summary>
        /// Will Fritz
        /// Created:  2015/02/06
        /// This will send a supplier object to the business logic layer
        /// </summary>
        /// <remarks>
        /// Will Fritz
        /// Updated: 2015/02/15
        /// Added confirmation message box
        /// Rose Steffensmeier
        /// Updated:  2015/04/24
        /// deleted checking supplierUserName
        /// </remarks>
        private async void EditSupplier()
        {
            try
            {
                Supplier tempSupplier = new Supplier
                {
                    CompanyName = TxtCompanyName.Text.Trim(),
                    FirstName = TxtFirstName.Text.Trim(),
                    LastName = TxtLastName.Text.Trim(),
                    Address1 = TxtAddress1.Text.Trim(),
                    Address2 = TxtAddress2.Text.Trim(),
                    PhoneNumber = TxtPhoneNumber.Text,
                    Zip = CboZip.SelectedValue.ToString(),
                    EmailAddress = TxtEmail.Text.Trim(),
                    SupplyCost = (decimal)NumSupplyCost.Value,
                    SupplierID = _updatableSupplier.SupplierID
                };

                if (_manager.EditSupplier(_updatableSupplier, tempSupplier) == SupplierResult.Success)
                {
                    await this.ShowMessageDialog("The Supplier was successfully edited.");
                    DialogResult = true;
                    Close();
                }
                else
                {
                    throw new WanderingTurtleException(this, "Supplier wasn't added to the database.");
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Will Fritz
        /// Created 2/19/2015
        /// fills the zip code combo box
        /// </summary>
        private void FillComboBox()
        {
            try
            {
                _zips = DataCache._currentCityStateList;
                CboZip.ItemsSource = _zips;
                CboZip.DisplayMemberPath = "GetZipStateCity";
                CboZip.SelectedValuePath = "Zip";
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error Retrieving the list of zip codes");
            }
        }
    }
}