using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// This Window allows the administrator to directly add a suplier
    /// </summary>
    public partial class AddEditSupplier
    {
        public static AddEditSupplier Instance;
        private SupplierManager _manager = new SupplierManager();
        private Supplier _UpdatableSupplier;
        private List<CityState> _zips;
        private CityStateManager _cityStateManager = new CityStateManager();

        private SupplierLoginManager _loginManager = new SupplierLoginManager();
        private string _supplierUserName;

        /// <summary>
        /// Constructs the object and will fill the list of suppliers
        /// created by Will Fritz 2/6/15
        /// </summary>
        public AddEditSupplier()
        {
            InitializeComponent();
            btnEdit.IsEnabled = false;
            fillComboBox();
            Instance = this;
        }

        public AddEditSupplier(Supplier supplierToEdit, bool ReadOnly = false)
        {
            InitializeComponent();
            Instance = this;
            this.Title = "Edit Supplier";

            //retrieve the username
            _supplierUserName = _loginManager.retrieveSupplierUserName(supplierToEdit.SupplierID);

            fillComboBox();
            FillUpdateList(supplierToEdit);

            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] { }); }
        }

        //////////////////////Windows Events//////////////////////////////

        /// <summary>
        /// Will fill the list and set error message to nothing
        /// created by Will Fritz 2/6/15
        /// </summary>
        /// /// <remarks>
        /// edited by will fritz 2/19/15
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //btnEdit.IsEnabled = false;
        }

        /// <summary>
        /// Will validate the fields and edit the current supplier
        /// created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/19/15
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
            {
                return;
            }
            else
            {
                EditSupplier();
                ListSuppliers.Instance.FillList();
                //this.Close();
            }
        }

        /// <summary>
        /// Will validate the fields and call add supplier method
        /// created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/19/15
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
            {
                return;
            }
            else
            {
                AddTheSupplier();
                ListSuppliers.Instance.FillList();
                //this.Close();
            }
        }

        /////////////////////////////Custom Methods/////////////////////////////////////

        /// <summary>
        /// checks to see if all the feilds are fill out and formated with the correct data
        /// It returns a false if there is an invalid feilds(s) and will output an error message to the lblError label
        /// Created By Will Fritz 2/4/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/19/15
        /// </remarks>
        /// <returns></returns>
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
        /// This will send a supplier object to the business logic layer
        /// Created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/15/15
        /// Added a conformation message box
        /// </remarks>
        private async void AddTheSupplier()
        {
            try
            {
                bool validUserName = false;

                validUserName = _loginManager.CheckSupplierUserName(txtUserName.Text);

                if (validUserName)
                {
                    Supplier tempSupplier = new Supplier();

                    tempSupplier.CompanyName = txtCompanyName.Text.Trim();
                    tempSupplier.FirstName = txtFirstName.Text.Trim();
                    tempSupplier.LastName = txtLastName.Text.Trim();
                    tempSupplier.Address1 = txtAddress1.Text.Trim();
                    tempSupplier.Address2 = txtAddress2.Text.Trim();
                    tempSupplier.PhoneNumber = txtPhoneNumber.Text;
                    tempSupplier.Zip = cboZip.SelectedValue.ToString();
                    tempSupplier.EmailAddress = txtEmail.Text.Trim();
                    tempSupplier.SupplyCost = (decimal)numSupplyCost.Value;

                    if (_manager.AddANewSupplier(tempSupplier, txtUserName.Text) == SupplierResult.Success)
                    {
                        await DialogBox.ShowMessageDialog(this, "Supplier was added to the database.");
                        this.Close();
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
        /// This will fill the add/edit tab feilds with the data from a selected Supplier from the list view
        /// Created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/15/15
        /// changed zip to a drop down
        /// </remarks>
        /// <param name="supplierUpdate"></param>
        public void FillUpdateList(Supplier supplierUpdate)
        {
            txtCompanyName.Text = supplierUpdate.CompanyName.Trim();
            txtFirstName.Text = supplierUpdate.FirstName.Trim();
            txtLastName.Text = supplierUpdate.LastName.Trim();
            txtAddress1.Text = supplierUpdate.Address1.Trim();
            txtAddress2.Text = supplierUpdate.Address2.Trim();
            txtEmail.Text = supplierUpdate.EmailAddress.Trim();
            //throw new WanderingTurtleException(supplierUpdate.PhoneNumber);
            string phone = supplierUpdate.PhoneNumber.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            txtPhoneNumber.Text = phone;
            txtUserName.Text = _supplierUserName;

            for (int i = 0; i < _zips.Count; i++)
            {
                if (_zips[i].Zip == supplierUpdate.Zip)
                {
                    cboZip.SelectedValue = _zips[i].Zip;
                }
            }
            numSupplyCost.Value = (double)(supplierUpdate.SupplyCost);
            //cboZip.SelectedValue = supplierUpdate.Zip;
            _UpdatableSupplier = supplierUpdate;

            btnSubmit.IsEnabled = false;
            btnEdit.IsEnabled = true;
        }

        /// <summary>
        /// This will send a supplier object to the business logic layer
        /// Created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/15/15
        /// added conformation message box
        /// </remarks>
        private async void EditSupplier()
        {
            try
            {
                //check if user name has changed
                if (!_supplierUserName.Equals(txtUserName.Text))
                {
                    //update user name
                    ResultsEdit result = _loginManager.UpdateSupplierLogin(txtUserName.Text, _supplierUserName, _UpdatableSupplier.SupplierID);
                
                    if (result.Equals(ResultsEdit.Success))
                    {
                        await DialogBox.ShowMessageDialog(this, "Supplier was updated.");
                        this.Close();
                    }
                }

                Supplier tempSupplier = new Supplier();

                tempSupplier.CompanyName = txtCompanyName.Text.Trim();
                tempSupplier.FirstName = txtFirstName.Text.Trim();
                tempSupplier.LastName = txtLastName.Text.Trim();
                tempSupplier.Address1 = txtAddress1.Text.Trim();
                tempSupplier.Address2 = txtAddress2.Text.Trim();
                tempSupplier.PhoneNumber = txtPhoneNumber.Text;
                tempSupplier.Zip = cboZip.SelectedValue.ToString();
                tempSupplier.EmailAddress = txtEmail.Text.Trim();
                tempSupplier.SupplyCost = (decimal)numSupplyCost.Value;

                tempSupplier.SupplierID = _UpdatableSupplier.SupplierID;

                if (_manager.EditSupplier(_UpdatableSupplier, tempSupplier) == SupplierResult.Success)
                {
                    await DialogBox.ShowMessageDialog(this, "The Supplier was successfully edited.");
                    this.Close();
                }
                else
                {
                    throw new WanderingTurtleException(this, "Supplier wasn't added to the database.");
                }




            }

            catch (SqlException)
            {
                // ShowErrorMessage("UserName already used.  Please choose another one.");

                throw new WanderingTurtleException(this, "UserName already used.  Please choose another one.");
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }


        /// <summary>
        /// fills the zip code combo box
        /// created by will fritz 2/19/2015
        /// </summary>
        private void fillComboBox()
        {
            try
            {
                _zips = _cityStateManager.GetCityStateList();
                cboZip.ItemsSource = _zips;
                cboZip.DisplayMemberPath = "GetZipStateCity";
                cboZip.SelectedValuePath = "Zip";
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error Retrieving the list of zip codes");
            }
        }

        /// <summary>
        /// necessary to make the singleton pattern work
        /// Will Fritz 2015/3/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }
    }
}
