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
    /// Interaction logic for AddEditPendingSupplier.xaml
    /// </summary>
    public partial class AddEditPendingSupplier
    {
        public SupplierApplication CurrentSupplierApplication;
        public SupplierApplication UpdatedSupplierApplication = new SupplierApplication();
        public SupplierManager MySupplierManager = new SupplierManager();
        public SupplierLoginManager myLoginManager = new SupplierLoginManager();
        private List<CityState> _zips;
        private CityStateManager _cityStateManager = new CityStateManager();

        /// <summary>
        /// Created:  2015/04/04
        /// Miguel Santana
        ///
        /// Interaction logic for AddEditPendingSupplier.xaml
        /// </summary>
        public AddEditPendingSupplier()
        {
            InitializeComponent();
            Title = "Add a new Pending Supplier";
            SetFields();
            ReloadComboBox();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/09
        /// Handles loading of the screen with data from the list.
        /// </summary>
        /// <param name="currentSupplierApplication"></param>
        /// <param name="ReadOnly"></param>
        /// <exception cref="WanderingTurtleException">Occurs making components readonly.</exception>
        public AddEditPendingSupplier(SupplierApplication currentSupplierApplication, bool ReadOnly = false)
        {
            InitializeComponent();

            _cityStateManager.PopulateCityStateCache();
            CurrentSupplierApplication = currentSupplierApplication;
            Title = "Editing Pending Supplier: " + CurrentSupplierApplication.GetFullName;

            ReloadComboBox();
            fillComboBox();
            SetFields();

            if (ReadOnly) { WindowHelper.MakeReadOnly(Content as Panel, btnCancel); }
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/04
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/04
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/04
        /// 
        /// Handles the results from the logic layer
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated 2015/04/14
        /// 
        /// Added rejected/approved/pending combo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //validates data from form
            if (!Validate()) { return; }

            try
            {
                if (cboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Approved.ToString()))
                {
                    bool validUserName = false;

                    validUserName = myLoginManager.CheckSupplierUserName(txtUserName.Text);

                    if (validUserName)
                    {
                        //get data from form
                        GetFormData();

                        decimal supplyCost = (decimal)(numSupplyCost.Value);

                        SupplierResult result = MySupplierManager.ApproveSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication, txtUserName.Text, supplyCost);

                        if (result == SupplierResult.Success)
                        {
                            await this.ShowMessageDialog("Supplier application approved: Supplier added.");
                            DialogResult = true;
                            Close();
                        }
                        else
                        {
                            throw new WanderingTurtleException(this, "DB Error");
                        }
                    }
                    else
                    {
                        txtUserName.Text = "";
                        throw new WanderingTurtleException(this, "UserName already used.  Please choose another one.");
                    }
                }
                else if (cboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Rejected.ToString()) || cboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Pending.ToString()))
                {
                    //get data from form
                    GetFormData();

                    SupplierResult result = MySupplierManager.EditSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication);

                    if (result == SupplierResult.Success)
                    {
                        await this.ShowMessageDialog("Supplier application updated.");
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        throw new WanderingTurtleException(this, "DB Error");
                    }
                }
                else
                {
                    throw new WanderingTurtleException(this, "DB Error.");
                }
            }
            catch (SqlException ex)
            {
                // ShowErrorMessage("UserName already used.  Please choose another one.");

                throw new WanderingTurtleException(this, "UserName already used.  Please choose another one.", ex);
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2014/04/13
        /// 
        /// Validates user input
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            if (!Validator.ValidateCompanyName(txtCompanyName.Text))
            {
                throw new InputValidationException(txtCompanyName, "Enter a company name.");
            }
            if (!Validator.ValidateAlphaNumeric(txtAddress.Text) || String.IsNullOrEmpty(txtAddress.Text))
            {
                throw new InputValidationException(txtAddress, "Enter an address.");
            }
            if (!Validator.ValidatePhone(txtPhoneNumber.Text))
            {
                throw new InputValidationException(txtPhoneNumber, "Enter a phone number.");
            }
            if (!Validator.ValidateEmail(txtEmailAddress.Text))
            {
                throw new InputValidationException(txtEmailAddress, "Enter an email address.");
            }
            if (!Validator.ValidateString(txtFirstName.Text))
            {
                throw new InputValidationException(txtFirstName, "Enter a first name.");
            }
            if (!Validator.ValidateString(txtLastName.Text))
            {
                throw new InputValidationException(txtLastName, "Enter a last name.");
            }
            if (cboZip.SelectedItem == null)
            {
                throw new InputValidationException(cboZip, "You must select an zip from the drop down");
            }
            if (cboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Approved.ToString()) && (!Validator.ValidateString(txtUserName.Text)))
            {
                throw new InputValidationException(txtUserName, "Enter a user name.");
            }
            if (cboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Approved.ToString()) && !Validator.ValidateDecimal(numSupplyCost.Value.ToString()))
            {
                throw new InputValidationException(numSupplyCost, "Enter a valid supply cost.");
            }
            return true;
        }


        /// <summary>
        /// Pat Banks
        /// Created 2015/04/15
        /// 
        /// Gathers form data to send to the manager for addition to the database
        /// </summary>
        private void GetFormData()
        {
            UpdatedSupplierApplication.CompanyName = txtCompanyName.Text;
            UpdatedSupplierApplication.CompanyDescription = txtCompanyDescription.Text;
            UpdatedSupplierApplication.FirstName = txtFirstName.Text;
            UpdatedSupplierApplication.LastName = txtLastName.Text;
            UpdatedSupplierApplication.Address1 = txtAddress.Text;
            UpdatedSupplierApplication.Address2 = txtAddress2.Text;
            UpdatedSupplierApplication.Zip = cboZip.SelectedValue.ToString();

            UpdatedSupplierApplication.PhoneNumber = txtPhoneNumber.Text;
            UpdatedSupplierApplication.EmailAddress = txtEmailAddress.Text;
            UpdatedSupplierApplication.ApplicationDate = CurrentSupplierApplication.ApplicationDate;
            UpdatedSupplierApplication.ApplicationStatus = cboAppStatus.SelectedValue.ToString();
            UpdatedSupplierApplication.Remarks = txtRemarks.Text;

            //application id from record in variable
            UpdatedSupplierApplication.ApplicationID = CurrentSupplierApplication.ApplicationID;
            UpdatedSupplierApplication.LastStatusDate = DateTime.Now;
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/04
        ///
        /// Initial commit  - reads in data and sets the field for the user.
        /// </summary>
        private void SetFields()
        {
            txtCompanyName.Text = CurrentSupplierApplication.CompanyName;
            txtCompanyDescription.Text = CurrentSupplierApplication.CompanyDescription;
            txtFirstName.Text = CurrentSupplierApplication.FirstName;
            txtLastName.Text = CurrentSupplierApplication.LastName;
            txtAddress.Text = CurrentSupplierApplication.Address1;
            txtAddress2.Text = CurrentSupplierApplication.Address2;
            string phoneNumberMasked = CurrentSupplierApplication.PhoneNumber.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            txtPhoneNumber.Text = phoneNumberMasked;
            txtEmailAddress.Text = CurrentSupplierApplication.EmailAddress;
            dateApplicationDate.Content = CurrentSupplierApplication.ApplicationDate.ToString("D");
            cboAppStatus.Text = CurrentSupplierApplication.ApplicationStatus;
            txtRemarks.Text = CurrentSupplierApplication.Remarks;

            foreach (CityState cityState in _zips.Where(cityState => cityState.Zip == CurrentSupplierApplication.Zip))
            { cboZip.SelectedValue = cityState.Zip; }
        }

        /// <summary>
        /// will fritz 
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

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/10
        ///
        /// Defines application status for the combo box
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated:  2015/04/16
        /// updated format of code to IEnum
        /// </remarks>
        private IEnumerable<ApplicationStatus> GetStatusList { get { return new List<ApplicationStatus>(Enum.GetValues(typeof(ApplicationStatus)) as IEnumerable<ApplicationStatus>); } }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/11
        ///
        /// Reloads the combobox with values
        /// </summary>
        private void ReloadComboBox()
        {
            //creating a list for the dropdown userLevel
            cboAppStatus.ItemsSource = GetStatusList;
        }

        /// <summary>
        /// Ryan Blake
        /// Created:  2015/04/11
        /// 
        /// Turns fields on and off depending on the status of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboAppStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAppStatus.SelectedIndex.Equals(1))
            {
                numSupplyCost.IsEnabled = true;
                txtUserName.IsEnabled = true;
            }
            else
            {
                numSupplyCost.Value = .70;
                txtUserName.Text = "";

                numSupplyCost.IsEnabled = false;
                txtUserName.IsEnabled = false;
            }
        }
    }
}