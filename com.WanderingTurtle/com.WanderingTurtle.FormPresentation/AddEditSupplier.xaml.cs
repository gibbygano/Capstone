using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private CityStateManager _cityStateManager = new CityStateManager();
        private SupplierLoginManager _loginManager = new SupplierLoginManager();
        private SupplierManager _manager = new SupplierManager();
        private string _supplierUserName;
        private Supplier _UpdatableSupplier;
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
            fillComboBox();
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
        /// <param name="ReadOnly"></param>
        public AddEditSupplier(Supplier supplierToEdit, bool ReadOnly = false)
        {
            InitializeComponent();
            _UpdatableSupplier = supplierToEdit;
            Title = "Editing Supplier: " + _UpdatableSupplier.GetFullName;

            //retrieve the username
            _supplierUserName = _loginManager.retrieveSupplierUserName(supplierToEdit.SupplierID);

            fillComboBox();
            FillUpdateList();

            txtUserName.IsEnabled = false;

            if (ReadOnly) { WindowHelper.MakeReadOnly(Content as Panel); }
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
            if (_UpdatableSupplier == null)
            {
                txtCompanyName.Text = null;
                txtFirstName.Text = null;
                txtLastName.Text = null;
                txtAddress1.Text = null;
                txtAddress2.Text = null;
                txtEmail.Text = null;
                txtPhoneNumber.Text = null;
                txtUserName.Text = null;
                //making user name items visible so that they can be changed
                txtUserName.Visibility = System.Windows.Visibility.Visible;
                lblUserName.Visibility = System.Windows.Visibility.Visible;
                cboZip.SelectedItem = null;
                numSupplyCost.Value = .70;
            }
            else
            {
                txtCompanyName.Text = _UpdatableSupplier.CompanyName.Trim();
                txtFirstName.Text = _UpdatableSupplier.FirstName.Trim();
                txtLastName.Text = _UpdatableSupplier.LastName.Trim();
                txtAddress1.Text = _UpdatableSupplier.Address1.Trim();
                txtAddress2.Text = _UpdatableSupplier.Address2.Trim();
                txtEmail.Text = _UpdatableSupplier.EmailAddress.Trim();
                txtPhoneNumber.Text = _UpdatableSupplier.PhoneNumber.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                txtUserName.Text = _supplierUserName;
                //making user name items invisible so that they can't be changed
                txtUserName.Visibility = System.Windows.Visibility.Hidden;
                lblUserName.Visibility = System.Windows.Visibility.Hidden;
                foreach (CityState cityState in _zips.Where(cityState => cityState.Zip == _UpdatableSupplier.Zip)) { cboZip.SelectedValue = cityState.Zip; }
                numSupplyCost.Value = (double)(_UpdatableSupplier.SupplyCost);
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
            if (!Validator.ValidateCompanyName(txtCompanyName.Text.Trim()))
            {
                throw new InputValidationException(txtCompanyName, "Company Name field must be filled out and not contain special characters");
            }
            if (!Validator.ValidateEmail(txtEmail.Text.Trim()))
            {
                throw new InputValidationException(txtEmail, "Not a valid e-mail address");
            }
            if (!Validator.ValidatePhone(txtPhoneNumber.Text))
            {
                throw new InputValidationException(txtPhoneNumber, "The phone number cannot start with a 1 and must filled out and be formated correctly (10 numeric digits)");
            }
            if (cboZip.SelectedItem == null)
            {
                throw new InputValidationException(cboZip, "You must select an zip from the drop down");
            }
            if (!Validator.ValidateAlphaNumeric(txtAddress1.Text.Trim()))
            {
                throw new InputValidationException(txtAddress1, "The address must be filled out and not contain special characters (spaces allowed)");
            }
            if (!Validator.ValidateString(txtFirstName.Text.Trim()))
            {
                throw new InputValidationException(txtFirstName, "The first name field filled out and must not contain special characters (No Spaces)");
            }
            if (!Validator.ValidateString(txtLastName.Text.Trim()))
            {
                throw new InputValidationException(txtLastName, "The last name field must be filled out and not contain special characters (No Spaces)");
            }
            if (!Validator.ValidateAlphaNumeric(txtUserName.Text.Trim()))
            {
                throw new InputValidationException(txtUserName, "Enter a valid user name.");
            }
            if (!Validator.ValidateDecimal(numSupplyCost.Value.ToString()))
            {
                throw new InputValidationException(numSupplyCost, "Enter a valid supply cost.");
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
                bool validUserName = false;

                validUserName = _loginManager.CheckSupplierUserName(txtUserName.Text);

                if (validUserName)
                {
                    Supplier tempSupplier = new Supplier
                    {
                        CompanyName = txtCompanyName.Text.Trim(),
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Address1 = txtAddress1.Text.Trim(),
                        Address2 = txtAddress2.Text.Trim(),
                        PhoneNumber = txtPhoneNumber.Text,
                        Zip = cboZip.SelectedValue.ToString(),
                        EmailAddress = txtEmail.Text.Trim(),
                        SupplyCost = (decimal)numSupplyCost.Value
                    };

                    if (_manager.AddANewSupplier(tempSupplier, txtUserName.Text) == SupplierResult.Success)
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
                    txtUserName.Text = "";
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

            if (_UpdatableSupplier == null)
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
                    CompanyName = txtCompanyName.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Address1 = txtAddress1.Text.Trim(),
                    Address2 = txtAddress2.Text.Trim(),
                    PhoneNumber = txtPhoneNumber.Text,
                    Zip = cboZip.SelectedValue.ToString(),
                    EmailAddress = txtEmail.Text.Trim(),
                    SupplyCost = (decimal)numSupplyCost.Value,
                    SupplierID = _UpdatableSupplier.SupplierID
                };

                if (_manager.EditSupplier(_UpdatableSupplier, tempSupplier) == SupplierResult.Success)
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
        private void fillComboBox()
        {
            try
            {
                _zips = DataCache._currentCityStateList;
                cboZip.ItemsSource = _zips;
                cboZip.DisplayMemberPath = "GetZipStateCity";
                cboZip.SelectedValuePath = "Zip";
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error Retrieving the list of zip codes");
            }
        }
    }
}