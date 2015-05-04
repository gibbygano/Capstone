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
        public SupplierLoginManager MyLoginManager = new SupplierLoginManager();
        private List<CityState> _zips;
        private readonly CityStateManager _cityStateManager = new CityStateManager();

        /// <summary>
        /// Created:  2015/04/04
        /// Miguel Santana
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
        /// <param name="readOnly"></param>
        /// <exception cref="WanderingTurtleException">Occurs making components readonly.</exception>
        public AddEditPendingSupplier(SupplierApplication currentSupplierApplication, bool readOnly = false)
        {
            InitializeComponent();

            _cityStateManager.PopulateCityStateCache();
            CurrentSupplierApplication = currentSupplierApplication;
            Title = "Editing Pending Supplier: " + CurrentSupplierApplication.GetFullName;

            ReloadComboBox();
            FillComboBox();
            SetFields();

            if (readOnly) { (Content as Panel).MakeReadOnly(BtnCancel); }
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
        /// Takes user input to send to business logic layer
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Added rejected/approved/pending combo box
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //validates data from form
            if (!Validate()) { return; }

            try
            {
                if (CboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Approved.ToString()))
                {
                    bool validUserName = MyLoginManager.CheckSupplierUserName(TxtUserName.Text);

                    if (validUserName)
                    {
                        //get data from form
                        GetFormData();

                        decimal supplyCost = (decimal)(NumSupplyCost.Value);

                        SupplierResult result = MySupplierManager.ApproveSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication, TxtUserName.Text, supplyCost);

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
                        TxtUserName.Text = "";
                        throw new WanderingTurtleException(this, "UserName already used.  Please choose another one.");
                    }
                }
                else if (CboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Rejected.ToString()) || CboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Pending.ToString()))
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
        /// Validates user input
        /// </summary>
        /// <returns>true if valid</returns>
        private bool Validate()
        {
            if (!TxtCompanyName.Text.ValidateCompanyName())
            {
                throw new InputValidationException(TxtCompanyName, "Enter a company name.");
            }
            if (!TxtAddress.Text.ValidateAlphaNumeric() || String.IsNullOrEmpty(TxtAddress.Text))
            {
                throw new InputValidationException(TxtAddress, "Enter an address.");
            }
            if (!TxtPhoneNumber.Text.ValidatePhone())
            {
                throw new InputValidationException(TxtPhoneNumber, "Enter a phone number.");
            }
            if (!TxtEmailAddress.Text.ValidateEmail())
            {
                throw new InputValidationException(TxtEmailAddress, "Enter an email address.");
            }
            if (!TxtFirstName.Text.ValidateString())
            {
                throw new InputValidationException(TxtFirstName, "Enter a first name.");
            }
            if (!TxtLastName.Text.ValidateString())
            {
                throw new InputValidationException(TxtLastName, "Enter a last name.");
            }
            if (CboZip.SelectedItem == null)
            {
                throw new InputValidationException(CboZip, "You must select an zip from the drop down");
            }
            if (CboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Approved.ToString()) && (!TxtUserName.Text.ValidateString()))
            {
                throw new InputValidationException(TxtUserName, "Enter a user name.");
            }
            if (CboAppStatus.SelectedValue.ToString().Equals(ApplicationStatus.Approved.ToString()) && !NumSupplyCost.Value.ToString().ValidateDecimal())
            {
                throw new InputValidationException(NumSupplyCost, "Enter a valid supply cost.");
            }
            return true;
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/15
        /// Gathers form data to send to the manager for addition to the database
        /// </summary>
        private void GetFormData()
        {
            UpdatedSupplierApplication.CompanyName = TxtCompanyName.Text;
            UpdatedSupplierApplication.CompanyDescription = TxtCompanyDescription.Text;
            UpdatedSupplierApplication.FirstName = TxtFirstName.Text;
            UpdatedSupplierApplication.LastName = TxtLastName.Text;
            UpdatedSupplierApplication.Address1 = TxtAddress.Text;
            UpdatedSupplierApplication.Address2 = TxtAddress2.Text;
            UpdatedSupplierApplication.Zip = CboZip.SelectedValue.ToString();

            UpdatedSupplierApplication.PhoneNumber = TxtPhoneNumber.Text;
            UpdatedSupplierApplication.EmailAddress = TxtEmailAddress.Text;
            UpdatedSupplierApplication.ApplicationDate = CurrentSupplierApplication.ApplicationDate;
            UpdatedSupplierApplication.ApplicationStatus = CboAppStatus.SelectedValue.ToString();
            UpdatedSupplierApplication.Remarks = TxtRemarks.Text;

            //application id from record in variable
            UpdatedSupplierApplication.ApplicationID = CurrentSupplierApplication.ApplicationID;
            UpdatedSupplierApplication.LastStatusDate = DateTime.Now;
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/04
        /// Initial commit  - reads in data and sets the field for the user.
        /// </summary>
        private void SetFields()
        {
            TxtCompanyName.Text = CurrentSupplierApplication.CompanyName;
            TxtCompanyDescription.Text = CurrentSupplierApplication.CompanyDescription;
            TxtFirstName.Text = CurrentSupplierApplication.FirstName;
            TxtLastName.Text = CurrentSupplierApplication.LastName;
            TxtAddress.Text = CurrentSupplierApplication.Address1;
            TxtAddress2.Text = CurrentSupplierApplication.Address2;
            string phoneNumberMasked = CurrentSupplierApplication.PhoneNumber.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            TxtPhoneNumber.Text = phoneNumberMasked;
            TxtEmailAddress.Text = CurrentSupplierApplication.EmailAddress;
            DateApplicationDate.Content = CurrentSupplierApplication.ApplicationDate.ToString("D");
            CboAppStatus.Text = CurrentSupplierApplication.ApplicationStatus;
            TxtRemarks.Text = CurrentSupplierApplication.Remarks;

            foreach (CityState cityState in _zips.Where(cityState => cityState.Zip == CurrentSupplierApplication.Zip))
            { CboZip.SelectedValue = cityState.Zip; }
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

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/10
        /// Defines application status for the combo box
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated:  2015/04/16
        /// updated format of code to IEnum
        /// </remarks>
        private IEnumerable<ApplicationStatus> GetStatusList { get { return new List<ApplicationStatus>((IEnumerable<ApplicationStatus>)Enum.GetValues(typeof(ApplicationStatus))); } }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/11
        /// Reloads the combobox with values
        /// </summary>
        private void ReloadComboBox()
        {
            //creating a list for the dropdown userLevel
            CboAppStatus.ItemsSource = GetStatusList;
        }

        /// <summary>
        /// Ryan Blake
        /// Created:  2015/04/11
        /// Turns fields on and off depending on the status of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboAppStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CboAppStatus.SelectedIndex.Equals(1))
            {
                NumSupplyCost.IsEnabled = true;
                TxtUserName.IsEnabled = true;
            }
            else
            {
                NumSupplyCost.Value = .70;
                TxtUserName.Text = "";

                NumSupplyCost.IsEnabled = false;
                TxtUserName.IsEnabled = false;
            }
        }
    }
}