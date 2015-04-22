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

            _cityStateManager.PopulateCityStateCache();
            Title = "Add a new Pending Supplier";
            SetFields();
            ReloadComboBox();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/09
        /// Handles loading of the screen with data from the list.
        /// </summary>
        /// <param name="CurrentSupplierApplication"></param>
        /// <param name="ReadOnly"></param>
        /// <exception cref="WanderingTurtleException">Occurrs making components readonly.</exception>
        public AddEditPendingSupplier(SupplierApplication CurrentSupplierApplication, bool ReadOnly = false)
        {
            InitializeComponent();

            _cityStateManager.GetCityStateList();
            CurrentSupplierApplication = currentSupplierApplication;
            Title = "Editing Pending Supplier: " + CurrentSupplierApplication.GetFullName;

            ReloadComboBox();
            fillComboBox();
            SetFields();

            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] { btnCancel }); }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        /// <summary>
        ///
        /// </summary>
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
                            await DialogBox.ShowMessageDialog(this, "Supplier application approved: Supplier added.");
                            DialogResult = true;
                            this.Close();
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
                        await DialogBox.ShowMessageDialog(this, "Supplier application updated.");
                        DialogResult = true;
                        this.Close();
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

        private bool Validate()
        {
            if (!Validator.ValidateCompanyName(txtCompanyName.Text))
            {
                throw new InputValidationException(txtCompanyName, "Enter a company name.");
            }
            if (!Validator.ValidateAddress(txtAddress.Text))
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

        private void GetFormData()
        {
            UpdatedSupplierApplication.CompanyName = this.txtCompanyName.Text;
            UpdatedSupplierApplication.CompanyDescription = this.txtCompanyDescription.Text;
            UpdatedSupplierApplication.FirstName = this.txtFirstName.Text;
            UpdatedSupplierApplication.LastName = this.txtLastName.Text;
            UpdatedSupplierApplication.Address1 = this.txtAddress.Text;
            UpdatedSupplierApplication.Address2 = this.txtAddress2.Text;
            UpdatedSupplierApplication.Zip = cboZip.SelectedValue.ToString();

            UpdatedSupplierApplication.PhoneNumber = this.txtPhoneNumber.Text;
            UpdatedSupplierApplication.EmailAddress = this.txtEmailAddress.Text;
            UpdatedSupplierApplication.ApplicationDate = CurrentSupplierApplication.ApplicationDate;
            UpdatedSupplierApplication.ApplicationStatus = this.cboAppStatus.SelectedValue.ToString();
            UpdatedSupplierApplication.Remarks = this.txtRemarks.Text;

            //application id from record in memory
            UpdatedSupplierApplication.ApplicationID = CurrentSupplierApplication.ApplicationID;
            UpdatedSupplierApplication.LastStatusDate = DateTime.Now;
        }

        /// <summary>
        ///
        /// </summary>
        private void SetFields()
        {
            this.txtCompanyName.Text = CurrentSupplierApplication.CompanyName;
            this.txtCompanyDescription.Text = CurrentSupplierApplication.CompanyDescription;
            this.txtFirstName.Text = CurrentSupplierApplication.FirstName;
            this.txtLastName.Text = CurrentSupplierApplication.LastName;
            this.txtAddress.Text = CurrentSupplierApplication.Address1;
            this.txtAddress2.Text = CurrentSupplierApplication.Address2;
            string phoneNumberMasked = CurrentSupplierApplication.PhoneNumber.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            this.txtPhoneNumber.Text = phoneNumberMasked;
            this.txtEmailAddress.Text = CurrentSupplierApplication.EmailAddress;
            this.dateApplicationDate.Content = CurrentSupplierApplication.ApplicationDate.ToString("D");
            this.cboAppStatus.Text = CurrentSupplierApplication.ApplicationStatus;
            this.txtRemarks.Text = CurrentSupplierApplication.Remarks;

            foreach (CityState cityState in _zips.Where(cityState => cityState.Zip == CurrentSupplierApplication.Zip))
            { cboZip.SelectedValue = cityState.Zip; }
        }

        /// <summary>
        /// fills the zip code combo box
        /// created by will fritz 2/19/2015
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
